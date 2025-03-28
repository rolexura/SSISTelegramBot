﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Telegram Bot Connection Manager UI")]
[assembly: AssemblyDescription("Telegram Bot Connection Manager UI")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("XBase")]
[assembly: AssemblyProduct("Telegram Bot for Microsoft SSIS")]
[assembly: AssemblyCopyright("Rostislav Uralskyi '2025")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("4da6a11a-6a98-41d0-8a29-ceebf37ce8ac")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
#if SQL2017
[assembly: AssemblyVersion("1.0.0.2017")]
[assembly: AssemblyFileVersion("1.0.0.2017")]
#elif SQL2019
[assembly: AssemblyVersion("1.0.0.2019")]
[assembly: AssemblyFileVersion("1.0.0.2019")]
#elif SQL2022
[assembly: AssemblyVersion("1.0.0.2022")]
[assembly: AssemblyFileVersion("1.0.0.2022")]
#else
#error "This code must be compiled with SQL2017, SQL2019 or SQL2022 defined."
#endif
