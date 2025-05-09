using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipBox2;


public class MasterData
{
    // Track the next available ID for new lists
  //  public int NextListId { get; set; } = 1;
    
    public Dictionary<string, Info> Lists { get; set; } = new Dictionary<string, Info>(StringComparer.OrdinalIgnoreCase);
    
    /// <summary>
    /// Ensures all lists have valid Name properties but does NOT change dictionary keys
    /// Only used during loading to ensure backward compatibility
    /// </summary>
    public void EnsureListsHaveNames()
    {
        // Process each list to ensure it has a Name
        foreach (var kvp in Lists)
        {
            string key = kvp.Key;
            Info info = kvp.Value;
            
            // If Name is not set, use the dictionary key
            if (string.IsNullOrEmpty(info.Name))
            {
                info.Name = key;
            }
        }
    }
    
    /// <summary>
    /// Prepares lists for saving by assigning IDs and using them as keys
    /// Only called during save operations
    /// </summary>
    public void PrepareForSave()
    {
        // Create a temporary dictionary to hold the updated lists
        var updatedLists = new Dictionary<string, Info>(StringComparer.OrdinalIgnoreCase);

        // Process each list
        int keyIdx = 0;

        foreach (var kvp in Lists)
        {
            string key = kvp.Key;
            Info info = kvp.Value;
            
            
            // backwardcomp -previouslift
            if (string.IsNullOrEmpty(info.Name))
            {
                info.Name = key;
            }

            info.Id = keyIdx;

            // If ID is not set (0 is default value), assign a new ID
            //if (info.Id == 0)
            //{
            //    info.Id = NextListId++;
            //}
            
            // Use the ID as the new key
            string newKey = info.Id.ToString();

            updatedLists[newKey] = info;
            keyIdx++;
        }
        
        // Replace the old dictionary with the updated one
        Lists = updatedLists;
    }
    
    /// <summary>
    /// Gets a list by its name
    /// </summary>
    public Info GetListByName(string name)
    {
        return Lists.Values.FirstOrDefault(info => info.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
    
    /// <summary>
    /// Adds a new list or updates an existing one
    /// </summary>
    public void AddOrUpdateList(Info info)
    {
        // Ensure the list has an ID


        //if (info.Id == 0)
        //{
        //    info.Id = NextListId++;
        //}

        var last = Lists.LastOrDefault();

        var lastKey= last.Key.RgxInt(0);
        lastKey++;
        info.Id = lastKey;

        // Use the ID as the key
        Lists[info.Id.ToString()] = info;
    }
    
    /// <summary>
    /// Removes a list by its name
    /// </summary>
    public bool RemoveListByName(string name)
    {
        var list = GetListByName(name);
        if (list != null)
        {
            return Lists.Remove(list.Id.ToString());
        }
        return false;
    }
    
    /// <summary>
    /// Saves the MasterData to the default JSON file
    /// </summary>
    public void Save()
    {
        // Prepare lists for saving by assigning IDs
        PrepareForSave();
        SaveJSON.SaveMasterData(this);
    }
    
    /// <summary>
    /// Saves the MasterData to a specified JSON file path
    /// </summary>
    /// <param name="jsonPath">The path to save the JSON file to</param>
    public void Save(string jsonPath)
    {
        // Prepare lists for saving by assigning IDs
        PrepareForSave();
        SaveJSON.SaveMasterData(this, jsonPath);
    }
}
