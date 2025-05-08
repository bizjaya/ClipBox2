using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.IO;
using System.Windows.Forms;
// No MaterialSkin references needed for standard WinForms controls

namespace ClipBox2;

public partial class Edit : MaterialSkin.Controls.MaterialForm
{
    // Mode: true = Edit, false = Add
    private bool isEditMode = false;
    // Data source for columns
    private BindingList<Info> columnData = new BindingList<Info>();

    // Optionally allow passing mode in constructor
    public Edit(bool editMode = false)
    {
        InitializeComponent();
        isEditMode = editMode;
        SetupMode();
        SetupGrid();
        SetupEventHandlers();
    }

    private void SetupMode()
    {
        cboList.Visible = isEditMode;
        tbxList.Visible = !isEditMode;
        this.Text = isEditMode ? "Edit List" : "Add List";
        btnEdit.Text = isEditMode ? "Edit List" : "Add List";
    }

    private void SetupGrid()
    {
        dgvColumns.AutoGenerateColumns = false;
        dgvColumns.DataSource = columnData;
        // If columns are not already added in designer, add them here:
        if (dgvColumns.Columns.Count == 0)
        {
            var colName = new DataGridViewTextBoxColumn();
            colName.HeaderText = "Column Name";
            colName.DataPropertyName = "colName";
            colName.Name = "colName";
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            var colMulti = new DataGridViewCheckBoxColumn();
            colMulti.HeaderText = "Multi";
            colMulti.DataPropertyName = "multi";
            colMulti.Name = "colMulti";
            colMulti.Width = 50;
            dgvColumns.Columns.AddRange(new DataGridViewColumn[] { colName, colMulti });
        }
    }

    // Add column
    // Add column (renamed from btnplus_Click to avoid ambiguous method definition)
    private void AddColumn(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(this.tbcolname.Text)) return;
        columnData.Add(new Info { cols = new List<string> { this.tbcolname.Text }, multi = false });
        this.tbcolname.Text = "";
    }

    // Remove selected column (renamed from btnminus_Click to avoid ambiguous method definition)
    private void RemoveColumn(object sender, EventArgs e)
    {
        if (this.dgvColumns.CurrentRow == null) return;
        columnData.RemoveAt(this.dgvColumns.CurrentRow.Index);
    }

    // Move selected column left/up (renamed from btnColumnLeft_Click to avoid ambiguous method definition)
    private void MoveColumnLeft(object sender, EventArgs e)
    {
        int idx = this.dgvColumns.CurrentRow?.Index ?? -1;
        if (idx > 0)
        {
            var item = columnData[idx];
            columnData.RemoveAt(idx);
            columnData.Insert(idx - 1, item);
            this.dgvColumns.CurrentCell = this.dgvColumns.Rows[idx - 1].Cells[0];
        }
    }

    // Move selected column right/down (renamed from btnColumnRight_Click to avoid ambiguous method definition)
    private void MoveColumnRight(object sender, EventArgs e)
    {
        int idx = this.dgvColumns.CurrentRow?.Index ?? -1;
        if (idx >= 0 && idx < columnData.Count - 1)
        {
            var item = columnData[idx];
            columnData.RemoveAt(idx);
            columnData.Insert(idx + 1, item);
            this.dgvColumns.CurrentCell = this.dgvColumns.Rows[idx + 1].Cells[0];
        }
    }

    // Font size change
    private void fontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (fontSizeComboBox.SelectedIndex >= 0)
        {
            int size = GetSelectedFontSize();
            if (size > 0)
            {
                dgvColumns.Font = new Font(dgvColumns.Font.FontFamily, size);
            }
        }
    }

    private int GetSelectedFontSize()
    {
        if (fontSizeComboBox.SelectedIndex >= 0)
        {
            string selectedText = fontSizeComboBox.SelectedItem.ToString();
            foreach (var pair in App.FontSizes)
            {
                if (pair.Value == selectedText)
                {
                    return pair.Key;
                }
            }
        }
        return 9; // Default size
    }

    // Save (Add/Edit List)
    private void SaveList(object sender, EventArgs e)
    {
        // Gather columns info
        var colNames = columnData.Select(c => c.cols.FirstOrDefault() ?? "").ToList();
        var multiFlags = columnData.Select(c => c.multi).ToList();
        string listName = isEditMode ? this.cboList.SelectedItem?.ToString() : this.tbxList.Text;

        // Save to data model
        if (master != null && data != null)
        {
            data.cols = colNames;
            data.multi = chkPswd.Checked;
            // Other properties as needed

            // Update master data
            if (isEditMode)
            {
                // Update existing list
                // master.SaveList(listName, data);
            }
            else
            {
                // Add new list
                // master.AddList(listName, data);
            }
        }

        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private MasterData master;
    private Info data;
    private string listName;

    // For reference if needed
    public string xmlFile = Environment.GetEnvironmentVariable("cbFol")
                          + Environment.GetEnvironmentVariable("fName");

    public Edit(MasterData master, string listName)
    {
        // No MaterialSkin initialization - using standard WinForms controls
        // Set the form appearance to match your application style
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.MaximizeBox = false;
        this.MinimizeBox = true;
        InitializeComponent();
        this.master = master;
        this.listName = listName;

        // Set form properties to ensure it appears on top
        this.TopMost = true;
        this.StartPosition = FormStartPosition.CenterScreen;

        // Populate the font size combo box from App.FontSizes
        foreach (var size in App.FontSizes)
        {
            fontSizeComboBox.Items.Add(size.Value);
        }

        // Set default selected index
        fontSizeComboBox.SelectedIndexChanged += fontSizeComboBox_SelectedIndexChanged;

        // Populate the list names combo
        foreach (string name in master.Lists.Keys)
        {
            cboList.Items.Add(name);
        }

        if (cboList.Items.Count > 0)
            cboList.SelectedIndex = 0;
    }

    // Hook up event handlers in the constructor or form load
    private void SetupEventHandlers()
    {
        // Connect the renamed method handlers to their controls
        this.btnplus.Click -= new System.EventHandler(this.btnplus_Click); // Remove existing handler
        this.btnplus.Click += new System.EventHandler(this.AddColumn);

        this.btnminus.Click -= new System.EventHandler(this.btnminus_Click); // Remove existing handler
        this.btnminus.Click += new System.EventHandler(this.RemoveColumn);

        this.btnColumnLeft.Click -= new System.EventHandler(this.btnColumnLeft_Click); // Remove existing handler
        this.btnColumnLeft.Click += new System.EventHandler(this.MoveColumnLeft);

        this.btnColumnRight.Click -= new System.EventHandler(this.btnColumnRight_Click); // Remove existing handler
        this.btnColumnRight.Click += new System.EventHandler(this.MoveColumnRight);

        this.btnEdit.Click -= new System.EventHandler(this.btnEdit_Click); // Remove existing handler
        this.btnEdit.Click += new System.EventHandler(this.SaveList);

        // Make sure buttons have standard appearance
        foreach (var btn in new Button[] { btnplus, btnminus, btnColumnLeft, btnColumnRight, btnEdit })
        {
            btn.UseVisualStyleBackColor = true;
        }

        // Set up DataGridView event handlers
        this.dgvColumns.SelectionChanged += (s, e) =>
        {
            if (this.dgvColumns.CurrentRow != null && this.dgvColumns.CurrentRow.Index >= 0 && this.dgvColumns.CurrentRow.Index < columnData.Count)
            {
                var selectedCol = columnData[this.dgvColumns.CurrentRow.Index];
                // Update UI based on selection if needed
                this.tbcolname.Text = selectedCol.cols.FirstOrDefault() ?? "";
            }
        };

        //lboColumns.SelectedIndex = selectedIndex + 1;
    }

    private void btnplus_Click(object sender, EventArgs e)
    {
        // Add a column name to the list

    }

    private void btnminus_Click(object sender, EventArgs e)
    {
        //// Remove the selected column from the list
        //if (lboColumns.SelectedIndex < 0) return;
        //lboColumns.Items.RemoveAt(lboColumns.SelectedIndex);
    }

    private void btnColumnLeft_Click(object sender, EventArgs e)
    {
        // This is for moving columns in the DataGridView of Form1
        if (string.IsNullOrEmpty(cboList.Text)) return;
        string listName = cboList.Text;

        // Get the selected column index
        int selectedIndex = lboColumns.SelectedIndex;
        if (selectedIndex <= 0) return; // Can't move left if it's the first column or nothing is selected

        // Load the current data
        MasterData master = SaveJSON.LoadMasterData();
        if (!master.Lists.ContainsKey(listName)) return;
        Info data = master.Lists[listName];

        // Swap columns in the cols list
        string currentCol = data.cols[selectedIndex];
        string leftCol = data.cols[selectedIndex - 1];
        data.cols[selectedIndex - 1] = currentCol;
        data.cols[selectedIndex] = leftCol;

        // Swap data in each row of strs
        foreach (var row in data.strs)
        {
            if (row.Count > selectedIndex && row.Count > selectedIndex - 1)
            {
                string currentValue = row[selectedIndex];
                string leftValue = row[selectedIndex - 1];
                row[selectedIndex - 1] = currentValue;
                row[selectedIndex] = leftValue;
            }
        }

        // Save the updated data
        master.Save();

        // Update the UI
        lboColumns.Items.Clear();
        foreach (var col in data.cols)
        {
            lboColumns.Items.Add(col);
        }

        // Keep the moved item selected
        lboColumns.SelectedIndex = selectedIndex - 1;
    }

    private void btnColumnRight_Click(object sender, EventArgs e)
    {
        // This is for moving columns in the DataGridView of Form1
        if (string.IsNullOrEmpty(cboList.Text)) return;
        string listName = cboList.Text;

        // Get the selected column index
        int selectedIndex = lboColumns.SelectedIndex;
        if (selectedIndex < 0 || selectedIndex >= lboColumns.Items.Count - 1) return; // Can't move right if it's the last column or nothing is selected

        // Load the current data
        MasterData master = SaveJSON.LoadMasterData();
        if (!master.Lists.ContainsKey(listName)) return;
        Info data = master.Lists[listName];

        // Swap columns in the cols list
        string currentCol = data.cols[selectedIndex];
        string rightCol = data.cols[selectedIndex + 1];
        data.cols[selectedIndex + 1] = currentCol;
        data.cols[selectedIndex] = rightCol;

        // Swap data in each row of strs
        foreach (var row in data.strs)
        {
            if (row.Count > selectedIndex && row.Count > selectedIndex + 1)
            {
                string currentValue = row[selectedIndex];
                string rightValue = row[selectedIndex + 1];
                row[selectedIndex + 1] = currentValue;
                row[selectedIndex] = rightValue;
            }
        }

        // Save the updated data
        master.Save();

        // Update the UI
        lboColumns.Items.Clear();
        foreach (var col in data.cols)
        {
            lboColumns.Items.Add(col);
        }

        // Keep the moved item selected
        lboColumns.SelectedIndex = selectedIndex + 1;
    }

    private void cboList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboList.SelectedIndex >= 0)
        {
            listName = cboList.SelectedItem.ToString();
            data = master.Lists[listName];

            // Update the UI with the data
            lboColumns.Items.Clear();
            foreach (string s in data.cols)
            {
                lboColumns.Items.Add(s);
            }

            // Set the password checkbox
            chkPswd.Checked = data.pswd;

            // Set the font size combo box
            SelectFontSizeInComboBox(data.size);
        }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        data.cols.Clear();
        foreach (string s in lboColumns.Items)
        {
            data.cols.Add(s);
        }
        data.pswd = chkPswd.Checked;

        // Save the selected font size
        int selectedSize = GetSelectedFontSize();
        if (selectedSize > 0)
        {
            data.size = selectedSize;
        }

        master.Lists[listName] = data;
        master.Save();
        this.Close();
    }

    //private void fontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    // Update the font size for the list box to preview the change
    //    if (fontSizeComboBox.SelectedIndex >= 0)
    //    {
    //        int size = GetSelectedFontSize();
    //        if (size > 0)
    //        {
    //            lboColumns.Font = new Font(lboColumns.Font.FontFamily, size);
    //        }
    //    }
    //}

    private void Edit_Load(object sender, EventArgs e)
    {
        // Load data for the selected list
        if (!string.IsNullOrEmpty(listName) && master.Lists.TryGetValue(listName, out data))
        {
            // Set the list name in the combo box
            cboList.Text = listName;

            // Populate the columns list box
            lboColumns.Items.Clear();
            foreach (var col in data.cols)
            {
                lboColumns.Items.Add(col);
            }

            // Set the password checkbox
            chkPswd.Checked = data.pswd;

            // Set the font size combo box
            SelectFontSizeInComboBox(data.size);
        }
    }

    private void SelectFontSizeInComboBox(int size)
    {
        if (App.FontSizes.TryGetValue(size, out string sizeText))
        {
            fontSizeComboBox.SelectedItem = sizeText;
        }
        else
        {
            // Default to Size 9 if the specified size is not found
            if (App.FontSizes.TryGetValue(9, out string defaultSizeText))
            {
                fontSizeComboBox.SelectedItem = defaultSizeText;
            }
        }
    }


}
