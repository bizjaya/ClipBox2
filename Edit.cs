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
            cbxListName.Visible = isEditMode;
            tbxListName.Visible = !isEditMode;
            this.Text = isEditMode ? "Edit List" : "Add List";
            btnEdit.Text = isEditMode ? "Edit List" : "Add List";
        }

        private void Edit_Load(object sender, EventArgs e)
        {
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
            dgvColumns.AutoGenerateColumns = false;
            dgvColumns.Columns.Clear();

            DataGridViewTextBoxColumn nameCol = new DataGridViewTextBoxColumn();
            nameCol.DataPropertyName = "Name";
            nameCol.HeaderText = "Column Name";
            nameCol.Name = "colName";
            nameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvColumns.Columns.Add(nameCol);

            DataGridViewCheckBoxColumn pswdCol = new DataGridViewCheckBoxColumn();
            pswdCol.DataPropertyName = "IsPassword";
            pswdCol.HeaderText = "Pswd";
            pswdCol.Name = "colIsPassword";
            pswdCol.Width = 50;
            dgvColumns.Columns.Add(pswdCol);

            DataGridViewCheckBoxColumn multiLineCol = new DataGridViewCheckBoxColumn();
            multiLineCol.DataPropertyName = "IsMultiLine";
            multiLineCol.HeaderText = "MultiLine";
            multiLineCol.Name = "colIsMultiLine";
            multiLineCol.Width = 70;
            dgvColumns.Columns.Add(multiLineCol);

            dgvColumns.DataSource = columnData; // Bind to the BindingList<ColumnDisplayItem>
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
            // Populate list combo box
            cbxListName.Items.Clear();
            if (master != null && master.Lists != null)
            {
                foreach (var listNameKey in master.Lists)
                {
                    cbxListName.Items.Add(listNameKey);
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
            
            this.listName = cbxListName.SelectedItem.ToString();
            if (master.Lists.TryGetValue(this.listName, out Info data))
            {
                // Update the column data for the DataGridView
                columnData.Clear();
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
            }
            else
            {
                columnData.Clear();
                SelectFontSizeInComboBox(9);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Get the list name from the appropriate control based on mode
            string listNameToSave = isEditMode 
                ? (cbxListName.SelectedItem?.ToString() ?? this.listName) 
                : tbxListName.Text.Trim();

            // Validation
            if (string.IsNullOrWhiteSpace(listNameToSave))
            {
                MessageBox.Show("List name cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (columnData.Count == 0)
            {
                MessageBox.Show("A list must have at least one column.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check for duplicate list name
            if ((!isEditMode && master.Lists.ContainsKey(listNameToSave)) ||
                (isEditMode && this.listName != listNameToSave && master.Lists.ContainsKey(listNameToSave)))
            {
                MessageBox.Show($"A list with the name '{listNameToSave}' already exists.", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create or get the Info object
            Info infoToSave;
            bool isRenaming = isEditMode && this.listName != null && this.listName != listNameToSave;

            if (isEditMode && !isRenaming)
            {
                infoToSave = master.Lists.ContainsKey(listNameToSave) ? master.Lists[listNameToSave] : new Info();
            }
            else
            {
                infoToSave = new Info();
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
            infoToSave.size = GetSelectedFontSize();
            infoToSave.cbname = listNameToSave;
            infoToSave.cbdate = DateTime.Now;

            // Handle renaming
            if (isRenaming)
            {
                master.Lists.Remove(this.listName);
            }

            // Save the list
            master.Lists[listNameToSave] = infoToSave;
            master.Save();
            
            // Call the Save() method in Form1 if the owner is Form1
            if (this.Owner is Form1 form1)
            {
                // Use reflection to call the private Save() method in Form1
                System.Reflection.MethodInfo saveMethod = typeof(Form1).GetMethod("Save", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                
                if (saveMethod != null)
                {
                    saveMethod.Invoke(form1, null);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
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
            //if (string.IsNullOrWhiteSpace(tbcolname.Text))
            //{
            //    MessageBox.Show("Column name cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            string newColName = "Col Name";// tbcolname.Text.Trim();
            if (columnData.Any(cd => cd.Name.Equals(newColName, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Column name already exists.", "Duplicate Column", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            columnData.Add(new ColumnDisplayItem(newColName));
           // tbcolname.Clear();
           // tbcolname.Focus();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (dgvColumns.CurrentRow != null && dgvColumns.CurrentRow.Index >= 0)
            {
                columnData.RemoveAt(dgvColumns.CurrentRow.Index);
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
    }
}
