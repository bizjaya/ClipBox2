using System;
using System.Collections.Generic;

namespace ClipBox2
{
    /// <summary>
    /// Holds column definitions and row data for a single list,
    /// plus some extra metadata (e.g. cbmz, cbname).
    /// </summary>
    [Serializable]  // Optional for JSON, but doesn't hurt.
    public class Info
    {
        // Unique identifier for the list (used as dictionary key when saving)
        public int Id { get; set; }
        
        // Name of the list (displayed to the user)
        public string Name { get; set; }
        
        // List of column names
        public List<string> cols { get; set; } = new List<string>();

        // New: Per-column boolean flags
        public List<bool> colIsPassword { get; set; } = new List<bool>();
        public List<bool> colIsMultiLine { get; set; } = new List<bool>();

        // Rows of data; each row is itself a list of strings
        public List<List<string>> strs { get; set; } = new List<List<string>>();

        // Misc. string properties
        public string cbmz { get; set; }
        public string cbname { get; set; }
        public DateTime cbdate { get; set; }
        public bool pswd { get; set; } = false;
        public bool multi { get; set; } = false;
        public int size { get; set; } = 9;
    }
}
