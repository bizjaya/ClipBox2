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
        // List of column names
        public List<string> cols { get; set; } = new List<string>();

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
