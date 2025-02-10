using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClipBox2
{
    public static class SaveJSON
    {
        // Single JSON filename (in the same folder as the .exe).
        private static readonly string JsonPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ClipBox2.json");

        /// <summary>
        /// Load the entire MasterData from one JSON file.
        /// If the file doesn't exist, returns a new, empty MasterData.
        /// </summary>
        public static MasterData LoadMasterData()
        {
            if (!File.Exists(JsonPath))
                return new MasterData();

            // If encrypt=1, then the file is encrypted.
            bool isEncrypted = (Environment.GetEnvironmentVariable("encrypt") == "1");

            try
            {
                string raw = File.ReadAllText(JsonPath);
                if (isEncrypted)
                {
                    raw = DecryptString(raw);
                }

                // Convert the JSON to a MasterData object
                var data = JsonConvert.DeserializeObject<MasterData>(raw);
                if (data == null)
                    data = new MasterData();

                return data;
            }
            catch
            {
                // On failure, return a blank container
                return new MasterData();
            }
        }

        /// <summary>
        /// Save the entire MasterData to a single JSON file.
        /// If encrypt=1, encrypt the JSON text before writing.
        /// </summary>
        public static void SaveMasterData(MasterData master)
        {
            // Convert to JSON
            string rawJson = JsonConvert.SerializeObject(master, Newtonsoft.Json.Formatting.Indented);
            bool isEncrypted = (Environment.GetEnvironmentVariable("encrypt") == "1");
            if (isEncrypted)
            {
                rawJson = EncryptString(rawJson);
            }
            File.WriteAllText(JsonPath, rawJson);
        }

        #region Simple Encryption/Decryption Example
        // This uses a fixed key/IV for demo purposes.
        // In a real app, you'd do more secure key mgmt.

        private static readonly byte[] AesKey = Encoding.UTF8.GetBytes("1234567890123456");
        private static readonly byte[] AesIV = Encoding.UTF8.GetBytes("6543210987654321");

        private static string EncryptString(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = AesKey;
                aes.IV = AesIV;
                aes.Mode = CipherMode.CBC;

                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    return Convert.ToBase64String(cipherBytes);
                }
            }
        }

        private static string DecryptString(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = AesKey;
                aes.IV = AesIV;
                aes.Mode = CipherMode.CBC;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                    return Encoding.UTF8.GetString(plainBytes);
                }
            }
        }
        #endregion
    }
}
