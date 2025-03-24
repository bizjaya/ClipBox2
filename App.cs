using System;
using System.IO;
using System.Reflection;

namespace ClipBox2
{
    public static class App
    {
        public const string CompanyName = "BizJaya";
        public const string ToolName = "ClipBox";
        public const string Version = "2.0.0";
        public const string License = "MIT";

        public static string ExecutablePath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string JsonFilePath => Path.Combine(ExecutablePath, "ClipBox2.json");

        public static void EnsureAppDataFolderExists()
        {
            // Note: This method is no longer necessary with the new JSON file location,
            // but it has been left in the code for potential future use or refactoring.
            // if (!Directory.Exists(AppDataPath))
            // {
            //     Directory.CreateDirectory(AppDataPath);
            // }
        }
    }
}
