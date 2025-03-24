using System;
using System.Collections.Generic;
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

        // Font size options for the application
        public static readonly Dictionary<int, string> FontSizes = new Dictionary<int, string>
        {
            { 7, "Size 7" },
            { 8, "Size 8" },
            { 9, "Size 9" },
            { 10, "Size 10" },
            { 11, "Size 11" },
            { 12, "Size 12" },
            { 13, "Size 13" },
            { 14, "Size 14" },
            { 15, "Size 15" }
        };

        public static string ExecutablePath
        {
            get
            {
                // For single-file deployments, use AppContext.BaseDirectory instead of Assembly.Location
                string baseDir = AppContext.BaseDirectory;
                
                // Fallback to Assembly.Location if baseDir is null or empty
                if (string.IsNullOrEmpty(baseDir))
                {
                    baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                }
                
                return baseDir;
            }
        }

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
