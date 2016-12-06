//////////////////////////////////////////////////////////////////////
// TOOLS
//////////////////////////////////////////////////////////////////////
#tool "nuget:?package=GitVersion.CommandLine"
#tool "nuget:?package=GitReleaseNotes"
#addin nuget:?package=Cake.Git
#addin "Cake.ExtendedNuGet"
#addin "nuget:?package=NuGet.Core&version=2.8.6"
#addin "MagicChunks"


//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var artifactsDir = "./artifacts";
var projectName = "Dazinator.AspNet.Extensions.FileProviders";
var globalAssemblyFile = "./src/GlobalAssemblyInfo.cs";
var projectToPackage = $"./src/{projectName}";
var repoBranchName = "master";


var isContinuousIntegrationBuild = !BuildSystem.IsLocalBuild;

var gitVersionInfo = GitVersion(new GitVersionSettings {
    OutputType = GitVersionOutput.Json
});

var nugetVersion = isContinuousIntegrationBuild ? gitVersionInfo.NuGetVersion : "0.0.0";

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////
Setup(context =>
{
    Information("Building DotNetCoreBuild v{0}", nugetVersion);    
});

Teardown(context =>
{
    Information("Finished running tasks.");
});

//////////////////////////////////////////////////////////////////////
//  PRIVATE TASKS
//////////////////////////////////////////////////////////////////////

Task("__Default")    
    .IsDependentOn("__SetAppVeyorBuildNumber")
    .IsDependentOn("__Clean")
    .IsDependentOn("__Restore")
    .IsDependentOn("__UpdateAssemblyVersionInformation")
    .IsDependentOn("__Build")
    .IsDependentOn("__Test")
    .IsDependentOn("__UpdateProjectJsonVersion")
    .IsDependentOn("__Pack")
    .IsDependentOn("__GenerateReleaseNotes")
    .IsDependentOn("__PublishNuGetPackages");

Task("__Clean")
    .Does(() =>
{
    CleanDirectory(artifactsDir);
    CleanDirectories("./src/**/bin");
    CleanDirectories("./src/**/obj");
});

Task("__SetAppVeyorBuildNumber")
    .Does(() =>
{
    if (BuildSystem.AppVeyor.IsRunningOnAppVeyor)
    {
        var appVeyorBuildNumber = EnvironmentVariable("APPVEYOR_BUILD_NUMBER");
        var appVeyorBuildVersion = $"{nugetVersion}+{appVeyorBuildNumber}";
        repoBranchName = EnvironmentVariable("APPVEYOR_REPO_BRANCH");
        Information("AppVeyor branch name is " + repoBranchName);
        Information("AppVeyor build version is " + appVeyorBuildVersion);
        BuildSystem.AppVeyor.UpdateBuildVersion(appVeyorBuildVersion);
    }
    else
    {
        Information("Not running on AppVeyor");
    }    
});

Task("__Restore")
    .Does(() => DotNetCoreRestore());

Task("__UpdateAssemblyVersionInformation")
    .WithCriteria(isContinuousIntegrationBuild)
    .Does(() =>
{
     GitVersion(new GitVersionSettings {
        UpdateAssemblyInfo = true,
        UpdateAssemblyInfoFilePath = globalAssemblyFile
    });

    Information("AssemblyVersion -> {0}", gitVersionInfo.AssemblySemVer);
    Information("AssemblyFileVersion -> {0}", $"{gitVersionInfo.MajorMinorPatch}.0");
    Information("AssemblyInformationalVersion -> {0}", gitVersionInfo.InformationalVersion);
});

Task("__Build")
    .Does(() =>
{
    DotNetCoreBuild("**/project.json", new DotNetCoreBuildSettings
    {
        Configuration = configuration
    });
});

Task("__Test")
    .Does(() =>
{
    GetFiles("**/*Tests/project.json")
        .ToList()
        .ForEach(testProjectFile => 
        {           
            var projectDir = testProjectFile.GetDirectory();
            DotNetCoreTest(testProjectFile.ToString(), new DotNetCoreTestSettings
            {
                Configuration = configuration,
                WorkingDirectory = projectDir
            });
        });
});

Task("__UpdateProjectJsonVersion")
    .WithCriteria(isContinuousIntegrationBuild)
    .Does(() =>
{
    var projectToPackagePackageJson = $"{projectToPackage}/project.json";
    Information("Updating {0} version -> {1}", projectToPackagePackageJson, nugetVersion);

    TransformConfig(projectToPackagePackageJson, projectToPackagePackageJson, new TransformationCollection {
        { "version", nugetVersion }
    });
});

Task("__Pack")
    .Does(() =>
{
    var settings = new DotNetCorePackSettings
    {
        Configuration = "Release",
        OutputDirectory = $"{artifactsDir}"        
    };
            
    DotNetCorePack($"{projectToPackage}", settings);
});

Task("__GenerateReleaseNotes")
    .Does(() =>
{
    var settings = new DotNetCorePackSettings
    {
        Configuration = "Release",
        OutputDirectory = $"{artifactsDir}"        
    };    
            
    GitReleaseNotes($"{artifactsDir}/ReleaseNotes.md", new GitReleaseNotesSettings {
    WorkingDirectory         = ".",
    Verbose                  = true,       
    RepoBranch               = repoBranchName,    
    Version                  = nugetVersion,
    AllLabels                = true
});
});


Task("__PublishNuGetPackages")
    .Does(() =>
{              

            if(isContinuousIntegrationBuild)
            {

                var nugetPackageName = $"{artifactsDir}/{projectName}.{nugetVersion}.nupkg";
                var nugetSourcePackageName = $"{artifactsDir}/{projectName}.{nugetVersion}.symbols.nupkg";

                var feed = new
                    {
                    Name = "NuGetOrg",
                    Source = EnvironmentVariable("PUBLIC_NUGET_FEED_SOURCE")
                };
            
                NuGetAddSource(
                    name:feed.Name,
                    source:feed.Source
                );

                var apiKey = EnvironmentVariable("NuGetOrgApiKey");

                 // Push the package. NOTE: this also pushes the symbols package alongside.
                NuGetPush(nugetPackageName, new NuGetPushSettings {
                    Source = feed.Source,
                    ApiKey = apiKey
                });
                    
            }  
});


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
Task("Default")
    .IsDependentOn("__Default");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////
RunTarget(target);