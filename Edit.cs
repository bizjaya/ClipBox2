using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.IO;
using System.Windows.Forms;

namespace ClipBox2
{
    public partial class Edit : MaterialSkin.Controls.MaterialForm
    {
        // Mode: true = Edit, false = Add
        private bool isEditMode = false;
        private BindingList<ColumnDisplayItem> columnData = new BindingList<ColumnDisplayItem>();
        private MasterData master;
        private string listName;

        public Edit(bool editMode = false, string initialListName = null, MasterData masterData = null)
        {
            InitializeComponent();
            this.isEditMode = editMode;
            this.listName = initialListName;
            this.master = masterData;
        }

        private void SetupMode()
        {
            // In Edit mode, show both the combo box and text box
            cbxListName.Visible = isEditMode;
            //lblListName.Visible = false; // isEditMode;
            tbxListName.Visible = true;

            this.Text = isEditMode ? "Edit List" : "Add List";
            btnEdit.Text = isEditMode ? "Edit List" : "Add List";
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            // Set background colors to white
            tbxListName.BackColor = System.Drawing.Color.White;
            cbxListName.BackColor = System.Drawing.Color.White;
            dgvColumns.BackgroundColor = System.Drawing.Color.White;
            dgvColumns.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            
            if (this.master == null)
            {
                this.master = SaveJSON.LoadMasterData();
                if (this.master == null)
                {
                    MessageBox.Show("Failed to load master data. Form may not work correctly.", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.master = new MasterData();
                }
            }
            SetupMode();
            SetupGrid();
            SetupEventHandlers();
            PopulateComboBoxes();
        }

        private void SetupGrid()
        {
            // Clear all existing columns
            dgvColumns.Columns.Clear();
            
            // Configure the DataGridView
            dgvColumns.AutoGenerateColumns = false;
            dgvColumns.AllowUserToAddRows = false;
            dgvColumns.AllowUserToDeleteRows = false;
            dgvColumns.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Add ID column for column numbers
            DataGridViewTextBoxColumn idCol = new DataGridViewTextBoxColumn();
            idCol.HeaderText = "ID";
            idCol.Name = "colId";
            idCol.Width = 30; // Make it narrower
            idCol.ReadOnly = true;
            idCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // Prevent auto-sizing
            dgvColumns.Columns.Add(idCol);

            // Add Column Name column
            DataGridViewTextBoxColumn nameCol = new DataGridViewTextBoxColumn();
            nameCol.HeaderText = "Column Name";
            nameCol.Name = "colName";
            nameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvColumns.Columns.Add(nameCol);

            // Add Password checkbox column
            DataGridViewCheckBoxColumn pswdCol = new DataGridViewCheckBoxColumn();
            pswdCol.HeaderText = "Pswd";
            pswdCol.Name = "colPswd";
            pswdCol.Width = 50;
            dgvColumns.Columns.Add(pswdCol);

            // Add MultiLine checkbox column
            DataGridViewCheckBoxColumn multiLineCol = new DataGridViewCheckBoxColumn();
            multiLineCol.HeaderText = "MultiLine";
            multiLineCol.Name = "colMulti";
            multiLineCol.Width = 70;
            dgvColumns.Columns.Add(multiLineCol);
        }

        private void SetupEventHandlers()
        {
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            this.btnLeft.Click += new System.EventHandler(this.btnColumnLeft_Click);
            this.btnRight.Click += new System.EventHandler(this.btnColumnRight_Click);
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            this.cbxListName.SelectedIndexChanged += new System.EventHandler(this.cbxListName_SelectedIndexChanged);
            this.fontSizeComboBox.SelectedIndexChanged += new System.EventHandler(this.fontSizeComboBox_SelectedIndexChanged);

            this.dgvColumns.SelectionChanged += (s, e) =>
            {
                if (this.dgvColumns.CurrentRow != null && this.dgvColumns.CurrentRow.DataBoundItem is ColumnDisplayItem selectedDisplayItem)
                {
                    // tbcolname.Text = selectedDisplayItem.Name;
                }
                else
                {
                    // tbcolname.Text = "";
                }
            };
        }

        private void PopulateComboBoxes()
        {
            // Populate list combo box with CbxItem<string> objects
            cbxListName.Items.Clear();
            if (master != null && master.Lists != null)
            {
                foreach (var kvp in master.Lists)
                {
                    string key = kvp.Key;
                    Info info = kvp.Value;

                    // For display in the combo box, use the Name property if available, otherwise use the key
                    string displayName = string.IsNullOrEmpty(info.Name) ? key : info.Name;

                    // Add a CbxItem with display name and key value to the combo box
                    cbxListName.Items.Add(new CbxItem<string>(displayName, key));
                }
            }

            // Populate font size combo box using App.FontSizes dictionary
            fontSizeComboBox.Items.Clear();
            if (App.FontSizes != null)
            {
                // Sort by key (font size) to display in ascending order
                foreach (var pair in App.FontSizes.OrderBy(p => p.Key))
                {
                    fontSizeComboBox.Items.Add(pair.Value);
                }

                // Select default size 9 if available
                if (App.FontSizes.ContainsKey(9))
                {
                    fontSizeComboBox.SelectedItem = App.FontSizes[9];
                }
                else if (fontSizeComboBox.Items.Count > 0)
                {
                    fontSizeComboBox.SelectedIndex = 0;
                }
            }

            // Select initial list if in edit mode
            if (isEditMode && !string.IsNullOrEmpty(listName) && cbxListName.Items.Contains(listName))
            {
                cbxListName.SelectedItem = listName;
            }
            else if (isEditMode && cbxListName.Items.Count > 0)
            {
                cbxListName.SelectedIndex = 0;
            }
        }

        //private void LoadInitialData()
        //{
        //    // This method is no longer needed as it's replaced by PopulateComboBoxes
        //    // Keeping it for backward compatibility
        //    PopulateComboBoxes();
        //}

        private void cbxListName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxListName.SelectedItem == null) return;

            // Get the selected item from the combo box
            if (!(cbxListName.SelectedItem is CbxItem<string> selectedItem))
            {
                MessageBox.Show("Invalid selection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the key value directly from the selected item
            this.listName = selectedItem.Value;

            if (master.Lists.TryGetValue(this.listName, out Info data))
            {
                // Update the text box with the list name
                tbxListName.Text = selectedItem.Name;

                // Update the column data for the DataGridView
                columnData.Clear();
                
                // First make sure we have the grid set up correctly
                if (dgvColumns.Columns.Count < 4)
                {
                    SetupGrid();
                }
                
                // Load the column data from the Info object
                if (data.cols != null)
                {
                    for (int i = 0; i < data.cols.Count; i++)
                    {
                        string name = data.cols[i];
                        // Get password and multiline flags with safe defaults
                        bool isPswd = false;
                        bool isMulti = false;

                        // Handle colIsPassword safely
                        if (data.colIsPassword != null && i < data.colIsPassword.Count)
                            isPswd = data.colIsPassword[i];

                        // Handle colIsMultiLine safely
                        if (data.colIsMultiLine != null && i < data.colIsMultiLine.Count)
                            isMulti = data.colIsMultiLine[i];

                        columnData.Add(new ColumnDisplayItem(name, isPswd, isMulti));
                    }
                }

                // Update UI elements
                SelectFontSizeInComboBox(data.size);

                // Refresh the DataGridView
                RefreshDataGridView();
            }
            else
            {
                MessageBox.Show($"List not found: {selectedItem.Name} (Key: {this.listName})", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                columnData.Clear();
                SelectFontSizeInComboBox(9);
            }
        }

        /// <summary>
        /// Refreshes the DataGridView with the current column data and adds ID numbers
        /// </summary>
        private void RefreshDataGridView()
        {
            // Always ensure the grid is properly set up
            SetupGrid();
            
            // Clear existing rows
            dgvColumns.Rows.Clear();
            
            // Debug info
            Console.WriteLine($"Column data count: {columnData.Count}");
            
            // Only add rows if we have column data
            if (columnData.Count == 0)
            {
                return; // No data to display
            }
            
            try
            {
                // Add rows for each column in the data
                for (int i = 0; i < columnData.Count; i++)
                {
                    var item = columnData[i];
                    
                    // Create a new row
                    dgvColumns.Rows.Add();
                    int rowIndex = dgvColumns.Rows.Count - 1;
                    
                    // Set the values for each cell
                    dgvColumns.Rows[rowIndex].Cells["colId"].Value = (i + 1).ToString();
                    dgvColumns.Rows[rowIndex].Cells["colName"].Value = item.Name;
                    dgvColumns.Rows[rowIndex].Cells["colPswd"].Value = item.IsPassword;
                    dgvColumns.Rows[rowIndex].Cells["colMulti"].Value = item.IsMultiLine;
                    
                    // Debug info
                    Console.WriteLine($"Added row {i+1}: {item.Name}, Pswd: {item.IsPassword}, Multi: {item.IsMultiLine}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void PopulateDataGridViewForSelectedList(string listNameKey)
        //{
        //    columnData.Clear(); // Clear existing items
        //    if (master.Lists.TryGetValue(listNameKey, out Info selectedInfo))
        //    {
        //        this.tbxListName.Text = listNameKey; // Update list name TextBox if it's separate
        //        // Populate font size (assuming fontSizeComboBox is for font size)
        //        fontSizeComboBox.SelectedItem = selectedInfo.size.ToString();

        //        for (int i = 0; i < selectedInfo.cols.Count; i++)
        //        {
        //            string name = selectedInfo.cols[i];
        //            bool isPswd = selectedInfo.colIsPassword.ElementAtOrDefault(i);
        //            bool isMulti = selectedInfo.colIsMultiLine.ElementAtOrDefault(i);
        //            columnData.Add(new ColumnDisplayItem(name, isPswd, isMulti));
        //        }
        //        // List-level pswd/multi flags from selectedInfo.pswd and selectedInfo.multi are not directly tied to UI controls anymore.
        //    }
        //    /* FIX: Replace SaveJSON.SaveMasterData with your actual method to save master data */

        //    // SaveJSON.SaveMasterData(master,  App.xmlFile); // Assuming this is the correct save call

        //    master.Save();

        //    this.DialogResult = DialogResult.OK;
        //    this.Close();
        //}

        private void btnColumnLeft_Click(object sender, EventArgs e)
        {
            // Move selected column up
            if (dgvColumns.CurrentRow == null || dgvColumns.CurrentRow.Index < 1) return;
            int idx = dgvColumns.CurrentRow.Index;
            ColumnDisplayItem item = columnData[idx];
            columnData.RemoveAt(idx);
            columnData.Insert(idx - 1, item);
            dgvColumns.CurrentCell = dgvColumns.Rows[idx - 1].Cells[0];
        }

        private void btnColumnRight_Click(object sender, EventArgs e)
        {
            // Move selected column down
            if (dgvColumns.CurrentRow == null || dgvColumns.CurrentRow.Index >= columnData.Count - 1) return;
            int idx = dgvColumns.CurrentRow.Index;
            ColumnDisplayItem item = columnData[idx];
            columnData.RemoveAt(idx);
            columnData.Insert(idx + 1, item);
            dgvColumns.CurrentCell = dgvColumns.Rows[idx + 1].Cells[0];
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            // Auto-generate a column name
            string newColName = "Col" + (columnData.Count + 1);
            
            // Make sure the name is unique
            int counter = 1;
            while (columnData.Any(cd => cd.Name.Equals(newColName, StringComparison.OrdinalIgnoreCase)))
            {
                newColName = "Col" + (columnData.Count + counter);
                counter++;
            }
            
            // Add the new column
            columnData.Add(new ColumnDisplayItem(newColName));
            
            // Refresh the DataGridView to show the new column
            RefreshDataGridView();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (dgvColumns.CurrentRow != null && dgvColumns.CurrentRow.Index >= 0)
            {
                // Get the index of the selected row
                int index = dgvColumns.CurrentRow.Index;
                
                // Remove the column data
                columnData.RemoveAt(index);
                
                // Refresh the DataGridView to show the changes
                RefreshDataGridView();
            }
            else
            {
                MessageBox.Show("Please select a column to remove.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void fontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optionally, update DataGridView font live:
            // if (fontSizeComboBox.SelectedItem != null && int.TryParse(fontSizeComboBox.SelectedItem.ToString(), out int selectedFontSize))
            //     dgvColumns.Font = new Font(dgvColumns.Font.FontFamily, selectedFontSize);
        }

        private void SelectFontSizeInComboBox(int size)
        {
            // Try to find the display text for the given size in App.FontSizes
            if (App.FontSizes != null && App.FontSizes.TryGetValue(size, out string sizeText))
            {
                fontSizeComboBox.SelectedItem = sizeText;
                return;
            }

            // If the exact size isn't found, try to find the closest size
            if (App.FontSizes != null && App.FontSizes.Count > 0)
            {
                // Find closest size
                int closestSize = App.FontSizes.Keys.OrderBy(k => Math.Abs(k - size)).First();
                fontSizeComboBox.SelectedItem = App.FontSizes[closestSize];
                return;
            }

            // Default to first item if nothing else works
            if (fontSizeComboBox.Items.Count > 0)
                fontSizeComboBox.SelectedIndex = 0;
        }

        private int GetSelectedFontSize()
        {
            if (fontSizeComboBox.SelectedItem != null)
            {
                string selectedText = fontSizeComboBox.SelectedItem.ToString();

                // Look up the key (font size) for the selected display text in App.FontSizes
                if (App.FontSizes != null)
                {
                    var pair = App.FontSizes.FirstOrDefault(p => p.Value == selectedText);
                    if (pair.Key != 0) // If a valid key was found
                        return pair.Key;
                }

                // Try to extract a number from the text (e.g., "Size 9" -> 9)
                string numberPart = new string(selectedText.Where(char.IsDigit).ToArray());
                if (!string.IsNullOrEmpty(numberPart) && int.TryParse(numberPart, out int size))
                    return size;
            }

            return 9; // Default font size
        }

        public class ColumnDisplayItem
        {
            public string Name { get; set; }
            public bool IsPassword { get; set; }
            public bool IsMultiLine { get; set; }

            public ColumnDisplayItem(string name = "", bool isPassword = false, bool isMultiLine = false)
            {
                Name = name;
                IsPassword = isPassword;
                IsMultiLine = isMultiLine;
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            // Move the selected list up in order
            if (cbxListName.SelectedIndex <= 0) return; // Already at the top or nothing selected
            
            // Get the current index and item
            int currentIndex = cbxListName.SelectedIndex;
            object currentItem = cbxListName.SelectedItem;
            
            // Create a new ordered dictionary to hold the reordered lists
            var reorderedLists = new Dictionary<string, Info>();
            var keys = master.Lists.Keys.ToList();
            
            // Swap the current item with the one above it
            var temp = keys[currentIndex];
            keys[currentIndex] = keys[currentIndex - 1];
            keys[currentIndex - 1] = temp;
            
            // Rebuild the dictionary in the new order
            foreach (var key in keys)
            {
                reorderedLists[key] = master.Lists[key];
            }
            
            // Replace the master lists with the reordered lists
            master.Lists.Clear();
            foreach (var kvp in reorderedLists)
            {
                master.Lists[kvp.Key] = kvp.Value;
            }
            
            // Save the changes
            master.Save();
            
            // Refresh the combo box
            PopulateComboBoxes();
            
            // Reselect the item that was moved
            cbxListName.SelectedItem = currentItem;
        }

        private void btnBot_Click(object sender, EventArgs e)
        {
            // Move the selected list down in order
            if (cbxListName.SelectedIndex < 0 || cbxListName.SelectedIndex >= cbxListName.Items.Count - 1) return; // Already at the bottom or nothing selected
            
            // Get the current index and item
            int currentIndex = cbxListName.SelectedIndex;
            object currentItem = cbxListName.SelectedItem;
            
            // Create a new ordered dictionary to hold the reordered lists
            var reorderedLists = new Dictionary<string, Info>();
            var keys = master.Lists.Keys.ToList();
            
            // Swap the current item with the one below it
            var temp = keys[currentIndex];
            keys[currentIndex] = keys[currentIndex + 1];
            keys[currentIndex + 1] = temp;
            
            // Rebuild the dictionary in the new order
            foreach (var key in keys)
            {
                reorderedLists[key] = master.Lists[key];
            }
            
            // Replace the master lists with the reordered lists
            master.Lists.Clear();
            foreach (var kvp in reorderedLists)
            {
                master.Lists[kvp.Key] = kvp.Value;
            }
            
            // Save the changes
            master.Save();
            
            // Refresh the combo box
            PopulateComboBoxes();
            
            // Reselect the item that was moved
            cbxListName.SelectedItem = currentItem;
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Get the selected item and the new name from the text box
            string newListName = tbxListName.Text.Trim();

            // In edit mode, we need to get the key from the selected item
            string keyToUse = this.listName;
            string originalName = null;

            if (isEditMode && cbxListName.SelectedItem is CbxItem<string> selectedItem)
            {
                keyToUse = selectedItem.Value;
                originalName = selectedItem.Name;
            }

            // Validation
            if (string.IsNullOrWhiteSpace(newListName))
            {
                MessageBox.Show("List name cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (columnData.Count == 0)
            {
                MessageBox.Show("A list must have at least one column.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if we're trying to use a name that already exists (but not our own list)
            bool nameExists = false;
            //foreach (var kvp in master.Lists)
            //{
            //    // Skip our own list
            //    if (kvp.Key == keyToUse) continue;

            //    // Check if the new name matches any existing list name
            //    if (string.Equals(kvp.Value.Name, newListName, StringComparison.OrdinalIgnoreCase))
            //    {
            //        nameExists = true;
            //        break;
            //    }
            //}

            //if (nameExists)
            //{
            //    MessageBox.Show($"A list with the name '{newListName}' already exists.", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            // Get the Info object for our list
            Info infoToSave;

            if (isEditMode && master.Lists.ContainsKey(keyToUse))
            {
                // We're editing an existing list
                infoToSave = master.Lists[keyToUse];

                // Update the Name property with the new name from the text box
                infoToSave.Name = newListName;
            }
            else
            {
                // We're creating a new list
                infoToSave = new Info
                {
                    Name = newListName
                };
            }

            // Update the Info object with current data
            infoToSave.cols = new List<string>();
            infoToSave.colIsPassword = new List<bool>();
            infoToSave.colIsMultiLine = new List<bool>();

            foreach (ColumnDisplayItem item in columnData)
            {
                infoToSave.cols.Add(item.Name);
                infoToSave.colIsPassword.Add(item.IsPassword);
                infoToSave.colIsMultiLine.Add(item.IsMultiLine);
            }

            // Set other properties
            // Set the font size from the combo box
            if (fontSizeComboBox.SelectedItem != null)
            {
                infoToSave.size = GetSelectedFontSize();
            }

            // Save the updated list
            if (isEditMode)
            {
                // First remove the old list entry
                if (master.Lists.ContainsKey(keyToUse))
                {
                    // Store the ID from the original list if it exists
                    if (master.Lists[keyToUse].Id != 0)
                    {
                        infoToSave.Id = master.Lists[keyToUse].Id;
                    }

                    // Remove the old entry
                    master.Lists.Remove(keyToUse);
                }

                // Add the updated list using the AddOrUpdateList method
                // This ensures it's properly added with the correct key
                master.AddOrUpdateList(infoToSave);
            }
            else
            {
                // Add the new list
                master.AddOrUpdateList(infoToSave);
            }

            // Save the master data
            master.Save();
            
            // Update the parent form's combo box with the new list name
            if (this.Owner is Form1 parentForm)
            {
                parentForm.popCbxListName(newListName);
            }
            
            // Close the form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
