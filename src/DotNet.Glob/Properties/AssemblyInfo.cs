using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

#if DEBUG
[assembly: InternalsVisibleTo("DotNet.Glob.Tests")]
#endif

#if RELEASE
[assembly: InternalsVisibleTo("DotNet.Glob.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100e9758208a4534e9808dc497a5642ee3f976b0565635b718294966b8b6d3235c3ded8f55a332b17c3d284333e9a41589c6e71b67d6c4b8c8304dda07d29aa60b38b4f3f85a9cbb5bea68fddecc5f08fcb9a3182a40610347ebcdd0ac63baa69b4cb7b85d819016c6afb8facb87c75a51d5ba2a0ac5b15174793a3b883e54be4d4")]
#endif



// The following GUID is for the ID of the typelib if this project is exposed to COM

//[assembly: Guid("287e9a8c-c9ab-4590-8202-8d02d09ab45a")]
