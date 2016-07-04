using System;
using System.IO;

namespace Microsoft.SqlServer.IntegrationServices.Build
{
    internal class AssemblyPath
    {
        internal string GetAssemblyPath
        {
            get
            {
                string path = null;
                string[] versions = { "80", "100", "120", "130" };
                /*
                Microsoft.AnalysisServices.Project.DLL
                Microsoft.DataTransformationServices.VsIntegration.DLL
                Microsoft.DataWarehouse.VsIntegration.DLL
                Microsoft.SqlServer.ManagedDTS.dll
                */
                string dll = "Microsoft.DataWarehouse.VsIntegration.DLL";

                foreach (string version in versions)
                {
                    // For now, we'll assume its in the default location, there is a regkey to 
                    // check if its installed in a non-standard location, but not 100%
                    string dir = Path.Combine(ProgramFilesx86, @"Microsoft SQL Server\" + version + @"\Tools\Binn\ManagementStudio");
                    if (Directory.Exists(dir))
                    {
                        // Directory exists, lets see if it has the right files
                        if (File.Exists(Path.Combine(dir, dll)))
                        {
                            path = dir;
                        }
                    }
                    else
                    {
                        // Directory doesn't exist
                    }
                }
                return path;
            }
        }

        private string ProgramFilesx86
        {
            get
            {
                if (8 == IntPtr.Size
                    || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
                {
                    return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
                }
                return Environment.GetEnvironmentVariable("ProgramFiles");
            }
        }
    }
}
