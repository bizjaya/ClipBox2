using System;

namespace ClipBox2
{
    /// <summary>
    /// Generic record for combo box items with display name and value
    /// </summary>
    /// <typeparam name="T">The type of the value</typeparam>
    public record CbxItem<T>(string Name, T Value)
    {
        /// <summary>
        /// Override ToString to return only the Name property for display in combo boxes
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    };
}
