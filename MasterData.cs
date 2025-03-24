using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipBox2
{
    public class MasterData
    {
        public Dictionary<string, Info> Lists { get; set; } = new Dictionary<string, Info>(StringComparer.OrdinalIgnoreCase);
        
        /// <summary>
        /// Saves the MasterData to the default JSON file
        /// </summary>
        public void Save()
        {
            SaveJSON.SaveMasterData(this);
        }
        
        /// <summary>
        /// Saves the MasterData to a specified JSON file path
        /// </summary>
        /// <param name="jsonPath">The path to save the JSON file to</param>
        public void Save(string jsonPath)
        {
            SaveJSON.SaveMasterData(this, jsonPath);
        }
    }
}
