using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace ClipBox2
{
    public static class DataMigrator
    {
        /// <summary>
        /// Check if content is likely encrypted by looking for base64 pattern
        /// and absence of XML tags
        /// </summary>
        private static bool IsLikelyEncrypted(string content)
        {
            if (string.IsNullOrWhiteSpace(content)) return false;

            // Check if it starts with <?xml
            if (content.TrimStart().StartsWith("<?xml")) return false;

            // Check if it contains obvious XML tags
            if (content.Contains("</")) return false;

            // Check if it's mostly base64-like characters
            var base64Chars = content.Count(c => char.IsLetterOrDigit(c) || c == '+' || c == '/' || c == '=');
            var totalChars = content.Length;
            
            // If more than 90% of characters are base64-like, consider it encrypted
            return base64Chars > totalChars * 0.9;
        }

        public static void MigrateXmlToJson()
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select folder containing XML files to migrate";
                
                if (folderDialog.ShowDialog() != DialogResult.OK)
                    return;

                string sourceDirectory = folderDialog.SelectedPath;
                if (!Directory.Exists(sourceDirectory))
                {
                    MessageBox.Show("Selected directory does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var master = new MasterData();
                var xmlFiles = Directory.GetFiles(sourceDirectory, "*.xml");

                foreach (var xmlFile in xmlFiles)
                {
                    try
                    {
                        // Read the file content
                        string content = File.ReadAllText(xmlFile);
                        
                        // Check if content is encrypted
                        if (IsLikelyEncrypted(content))
                        {
                            try
                            {
                                // Use the legacy Encrypt class for decryption
                                content = Encrypt.DecryptString(content);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error decrypting {Path.GetFileName(xmlFile)}: {ex.Message}",
                                    "Decryption Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                continue;
                            }
                        }

                        // Load the XML content (either decrypted or original)
                        var doc = XDocument.Parse(content);
                        var info = new Info();

                        // Get the Info element attributes
                        var infoElement = doc.Root;
                        if (infoElement != null)
                        {
                            info.cbmz = infoElement.Attribute("cbmz")?.Value ?? "cbmz";
                            info.cbname = infoElement.Attribute(XName.Get("cbname", "http://bizjaya.com"))?.Value 
                                     ?? Path.GetFileNameWithoutExtension(xmlFile);
                            info.cbdate = DateTime.Now;  // Set current date for migrated files
                        }

                        // Get column names from <cols> element
                        var colsElement = doc.Root?.Element("cols");
                        if (colsElement != null)
                        {
                            info.cols = colsElement.Elements("col")
                                .Select(e => e.Value)
                                .ToList();

                            // Get data rows from <strs> element
                            var strsElement = doc.Root?.Element("strs");
                            if (strsElement != null)
                            {
                                info.strs = strsElement.Elements("str")
                                    .Select(strElement => strElement.Elements("string")
                                        .Select(s => s.Value ?? string.Empty)  // Handle null values
                                        .ToList())
                                    .ToList();
                            }
                            else
                            {
                                info.strs = new List<List<string>>();  // Initialize empty list
                            }

                            master.Lists[info.cbname] = info;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error processing {Path.GetFileName(xmlFile)}: {ex.Message}", 
                            "Migration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                // Save the migrated data to JSON in the same directory
                string jsonPath = Path.Combine(sourceDirectory, "ClipBox2.json");
                master.Save(jsonPath);

                MessageBox.Show($"Migration complete!\nJSON file saved as: {jsonPath}", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
