using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace ClipBox2
{
    public static class SaveJSON
    {
        private static readonly string Key = "YourEncryptionKey123"; // Replace with your secure key
        public static bool UseEncryption { get; set; } = true;

        /// <summary>
        /// Load the entire MasterData from one JSON file.
        /// If the file doesn't exist, returns a new, empty MasterData.
        /// </summary>
        public static MasterData LoadMasterData(string jsonPath = null)
        {
            App.EnsureAppDataFolderExists();
            
            try
            {
                jsonPath = jsonPath ?? App.JsonFilePath;
                if (!File.Exists(jsonPath))
                    return new MasterData();

                string rawJson = File.ReadAllText(jsonPath);
                
                if (UseEncryption)
                {
                    rawJson = DecryptString(rawJson);
                }

                // Convert the JSON to a MasterData object
                // var data = SimpleJson.DeserializeObject<MasterData>(rawJson);
                var data = JsonConvert.DeserializeObject<MasterData>(rawJson);
                if (data == null)
                    data = new MasterData();

                // Initialize empty collections if they're null
                if (data.Lists == null)
                    data.Lists = new Dictionary<string, Info>();
                
                // Initialize empty collections if they're null
                foreach (var info in data.Lists.Values)
                {
                    // Initialize collections if they're null
                    if (info.cols == null)
                        info.cols = new List<string>();
                    if (info.strs == null)
                        info.strs = new List<List<string>>();
                    if (info.colIsPassword == null)
                        info.colIsPassword = new List<bool>();
                    if (info.colIsMultiLine == null)
                        info.colIsMultiLine = new List<bool>();
                        
                    // For backward compatibility, if cbname is set but Name is not, use cbname
                    if (string.IsNullOrEmpty(info.Name) && !string.IsNullOrEmpty(info.cbname))
                    {
                        info.Name = info.cbname;
                    }
                }
                
                // Ensure all lists have Name properties set (but don't change keys)
                data.EnsureListsHaveNames();

                return data;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error loading data: {ex.Message}", "Error", 
                    System.Windows.Forms.MessageBoxButtons.OK, 
                    System.Windows.Forms.MessageBoxIcon.Error);
                // On failure, return a blank container
                return new MasterData();
            }
        }

        /// <summary>
        /// Save the entire MasterData to a single JSON file.
        /// Uses encryption if UseEncryption is true.
        /// </summary>
        public static void SaveMasterData(MasterData master, string jsonPath = null)
        {
            App.EnsureAppDataFolderExists();
            
            try
            {
                jsonPath = jsonPath ?? App.JsonFilePath;
                // string rawJson = SimpleJson.SerializeObject(master);
                string rawJson = JsonConvert.SerializeObject(master, Newtonsoft.Json.Formatting.Indented);
                
                if (UseEncryption)
                {
                    rawJson = EncryptString(rawJson);
                }
                
                File.WriteAllText(jsonPath, rawJson);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error saving data: {ex.Message}", "Error", 
                    System.Windows.Forms.MessageBoxButtons.OK, 
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        #region Encryption/Decryption
        // This uses a fixed key/IV for demo purposes.
        // In a real app, you'd do more secure key mgmt.

        public static string EncryptString(string text)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key.PadRight(32));
                aes.IV = new byte[16];  // Using a zero IV for simplicity

                using (var encryptor = aes.CreateEncryptor())
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string DecryptString(string cipherText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key.PadRight(32));
                aes.IV = new byte[16];  // Using a zero IV for simplicity

                using (var decryptor = aes.CreateDecryptor())
                using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
        #endregion
    }
}
