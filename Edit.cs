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


                /* FIX: Replace SaveJSON.LoadMasterData with your actual method to load master data */
              //  SaveJSON.LoadMasterData(/* FIX: Replace App.xmlFile with the correct path or static property for your XML file, e.g. Config.XmlFile or similar */ App.xmlFile);


                if (this.master == null)
                {
                    MessageBox.Show("Failed to load master data. Form may not work correctly.", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.master = new MasterData();
                }
            }
            SetupMode();
            SetupGrid();
            SetupEventHandlers();
            LoadInitialData();
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

            //this.cbxListName.SelectedIndexChanged += new System.EventHandler(this.cbxListName_SelectedIndexChanged);
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

        private void LoadInitialData()
        {
            // Populate cbxListName with list names from master.Lists
            this.cbxListName.Items.Clear();
            if (master != null && master.Lists != null)
            {
                foreach (var listNameKey in master.Lists.Keys)
                {
                    this.cbxListName.Items.Add(listNameKey);
                }
            }

            // If in edit mode and a list is selected (e.g. first one or a passed one), populate controls
            if (isEditMode && this.cbxListName.Items.Count > 0)
            {
                // this.cbxListName.SelectedIndex = 0; // Or select a specific list if its name was passed
                // This will trigger cbxListName_SelectedIndexChanged, which should load dgvColumns
            }
            else if (!isEditMode)
            {
                // Setup for adding a new list
                this.Text = "Add New List";
                this.tbxListName.Enabled = true;
                this.cbxListName.Enabled = false;
                columnData.Clear(); // Ensure grid is empty for new list
                // Set default font size, etc.
            }
            // The original Edit_Load from user might have more specific logic for initial state.
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(tbListName.Text))
            //{
            //    MessageBox.Show("Please enter a list name.", "Add List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //string listName = tbListName.Text;

            //// Create a new list with the columns
            //MasterData master = SaveJSON.LoadMasterData();

            //// Check if the list name already exists
            //if (master.Lists.ContainsKey(listName))
            //{
            //    MessageBox.Show($"A list with the name '{listName}' already exists.", "Add List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //// Create a new Info object for the list
            //Info info = new Info
            //{
            //    cols = new List<string>(),
            //    strs = new List<List<string>>(),
            //    pswd = chkPswd.Checked,
            //    size = GetSelectedFontSize()
            //};

            //// Add the columns
            //foreach (var item in lboColumns.Items)
            //{
            //    info.cols.Add(item.ToString());
            //}

            //// Add the list to the master data
            //master.Lists[listName] = info;
            //master.Save();

            //// Refresh the main form's combo box
            //if (mainForm != null)
            //{
            //    mainForm.populate(listName);
            //}

            //MessageBox.Show($"List '{listName}' added successfully.", "Add List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //this.Close();


        }
        private void PopulateDataGridViewForSelectedList(string listNameKey)
        {
            columnData.Clear(); // Clear existing items
            if (master.Lists.TryGetValue(listNameKey, out Info selectedInfo))
            {
                this.tbxListName.Text = listNameKey; // Update list name TextBox if it's separate
                // Populate font size (assuming fontSizeComboBox is for font size)
                fontSizeComboBox.SelectedItem = selectedInfo.size.ToString();

                for (int i = 0; i < selectedInfo.cols.Count; i++)
                {
                    string name = selectedInfo.cols[i];
                    bool isPswd = selectedInfo.colIsPassword.ElementAtOrDefault(i);
                    bool isMulti = selectedInfo.colIsMultiLine.ElementAtOrDefault(i);
                    columnData.Add(new ColumnDisplayItem(name, isPswd, isMulti));
                }
                // List-level pswd/multi flags from selectedInfo.pswd and selectedInfo.multi are not directly tied to UI controls anymore.
            }
            /* FIX: Replace SaveJSON.SaveMasterData with your actual method to save master data */

            // SaveJSON.SaveMasterData(master,  App.xmlFile); // Assuming this is the correct save call

            master.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

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
