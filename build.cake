//////////////////////////////////////////////////////////////////////
// TOOLS
//////////////////////////////////////////////////////////////////////
#tool "nuget:https://ci.appveyor.com/nuget/gitversion-8nigugxjftrw?package=GitVersion.CommandLine&version=4.0.0-pullrequest1269-1534"
#tool "nuget:?package=GitReleaseNotes&version=0.7.0"
#addin "nuget:?package=NuGet.Core&version=2.14.0"


//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var artifactsDir = "./artifacts";
var projectName = "DotNet.Glob";
var projectToPackage = $"./src/{projectName}";
var repoBranchName = "master";
var benchMarksEnabled = EnvironmentVariable("BENCHMARKS") == "on";
var solutionPath = "./src/DotNetGlob.sln";

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
    .IsDependentOn("__Build")
    .IsDependentOn("__Test")
    .IsDependentOn("__Benchmarks")       
    .IsDependentOn("__Pack")    
    .IsDependentOn("__PublishNuGetPackages");

Task("__Clean")
    .Does(() =>
{
    CleanDirectory(artifactsDir);
    CleanDirectories("./src/BDN.Generated");
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
    .Does(() => 
{
	 var settings = new DotNetCoreRestoreSettings
     {      
         ArgumentCustomization = args => args.Append("/p:PackageVersion=" + nugetVersion),
		 DisableParallel = true
     };

	 DotNetCoreRestore(solutionPath, settings);

});	

Task("__Build")
    .Does(() =>
{
    DotNetCoreBuild(solutionPath, new DotNetCoreBuildSettings
    {        
        Configuration = configuration,
		ArgumentCustomization = args => args.Append("--disable-parallel"),
		Verbosity = Cake.Common.Tools.DotNetCore.Detailed
    });   
});

Task("__Test")
    .Does(() =>
{
    GetFiles("**/*Tests/*.csproj")
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

Task("__Benchmarks")
    .Does(() =>
{
    if(benchMarksEnabled)
    {
        GetFiles("**/*Benchmarks/*.csproj")
        .ToList()
        .ForEach(projFile => 
        {           

            DotNetCoreBuild(projFile.ToString(), new DotNetCoreBuildSettings
            {
                Framework = "netcoreapp1.1",
                Configuration = configuration
            });

            var projectDir = projFile.GetDirectory();
            DotNetCoreRun(projFile.ToString(), "--args", new DotNetCoreRunSettings
            {
                Framework = "netcoreapp1.1",
                Configuration = configuration,
                WorkingDirectory = projectDir               
            });
        });
    }    
});

Task("__Pack")
    .Does(() =>
{

    var versionarg = "/p:PackageVersion=" + nugetVersion;
    var settings = new DotNetCorePackSettings
    {
        Configuration = "Release",
        OutputDirectory = $"{artifactsDir}",
		ArgumentCustomization = args=>args.Append(versionarg)
    };
            
    DotNetCorePack($"{projectToPackage}", settings);
});

Task("__GenerateReleaseNotes")
    .Does(() =>
{   
            
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