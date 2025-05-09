using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace ClipBox2;

public partial class Form1 : MaterialSkin.Controls.MaterialForm
{
    // Context menu for right-clicking on multiline cells
    private ContextMenuStrip cellContextMenu;
    private ToolStripMenuItem editTextMenuItem;

    // Search functionality
    private System.Windows.Forms.Timer searchTimer;
    private string lastSearchText = string.Empty;
    private List<int> filteredRows = new List<int>();
    private bool isSearchActive = false;

    public Form1()
    {
        // MaterialSkin initialization
        //var materialSkinManager = MaterialSkinManager.Instance;
        //materialSkinManager.AddFormToManage(this);
        //materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        //materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue200, Accent.LightBlue200, TextShade.WHITE);

        //// Set the form title for MaterialSkin
        this.Text = "ClipBox";
        // Add padding so MenuStrip is not hidden behind the MaterialSkin title bar
        this.Padding = new Padding(0, 24, 0, 0); // 24px is the standard MaterialSkin title bar height
                                                 // If MenuStrip is not docked, ensure it is: menuStrip1.Dock = DockStyle.Top;
                                                 // MaterialSkin initialization
        var materialSkinManager = MaterialSkinManager.Instance;
        materialSkinManager.AddFormToManage(this);
        materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue200, Accent.LightBlue200, TextShade.WHITE);


        // Initialize environment variables
        Environment.SetEnvironmentVariable("cbFol", App.ExecutablePath);
        Environment.SetEnvironmentVariable("fName", "default.xml");
        Environment.SetEnvironmentVariable("encrypt", "1");

        // Calls InitializeComponent() from the Designer file
        InitializeComponent();

        // Additional setup after controls are created
        cb2.Items.Add("Copy");
        cb2.Items.Add("Click");
        cb2.Items.Add("Point");
        cb2.Text = cb2.Items[0].ToString();

        dgv1.AllowUserToDeleteRows = false;
        dgv1.AllowUserToOrderColumns = false;

        // Populate font size combo box from App.FontSizes
        fontSizeComboBox.Items.Clear();
        foreach (var size in App.FontSizes)
        {
            fontSizeComboBox.Items.Add(size.Value);
        }

        // Default to Size 9
        SelectFontSizeInComboBox(9);

        // Set up keyboard shortcut handling
        this.KeyPreview = true;
        this.KeyDown += Form1_KeyDown;

        // Set up context menu for multiline cells
        cellContextMenu = new ContextMenuStrip();
        editTextMenuItem = new ToolStripMenuItem("Edit Text...");
        editTextMenuItem.Click += EditTextMenuItem_Click;
        cellContextMenu.Items.Add(editTextMenuItem);

        // Add cell mouse event handlers
        dgv1.CellMouseDown += Dgv1_CellMouseDown;
        dgv1.CellMouseDoubleClick += Dgv1_CellMouseDoubleClick;

        // Set up search functionality
        tbxSrch.KeyUp += TbxSrch_KeyUp;
        clrBtn.Click += ClrBtn_Click;

        // Initialize search timer to reduce UI lag
        searchTimer = new System.Windows.Forms.Timer();
        searchTimer.Interval = 300; // 300ms delay
        searchTimer.Tick += SearchTimer_Tick;
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        // Check for Ctrl+E to toggle edit mode
        if (e.Control && e.KeyCode == Keys.E)
        {
            e.Handled = true; // Mark as handled to prevent further processing
            e.SuppressKeyPress = true; // Suppress the key press to prevent it from being processed elsewhere

            // Toggle the edit mode checkbox
            editChk.Checked = !editChk.Checked;

            // The chk1_CheckedChanged event will handle the actual toggling of edit mode
            return;
        }

        // Check for Ctrl+C to copy cell content
        if (e.Control && e.KeyCode == Keys.C)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;

            // Copy the content of the selected cell
            CopyCellContent();
            return;
        }

        // Allow arrow keys to pass through to the DataGridView
        if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down ||
            e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ||
            e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
        {
            // Don't handle these keys at the form level
            e.Handled = false;
            return;
        }
    }

    private void CopyCellContent()
    {
        // Make sure we have a selected cell
        if (dgv1.SelectedCells.Count == 0) return;

        // Get the value, checking first if it's masked
        string textValue;
        if (dgv1.SelectedCells[0].Value?.ToString() == "****" && dgv1.SelectedCells[0].Tag != null)
        {
            // Use the original value stored in Tag
            textValue = dgv1.SelectedCells[0].Tag.ToString();
        }
        else
        {
            textValue = dgv1.SelectedCells[0].Value?.ToString() ?? "";
        }

        // Copy to clipboard
        if (!string.IsNullOrEmpty(textValue))
        {
            Clipboard.SetText(textValue);
        }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        try
        {
            // Set background colors to white
            tbxSrch.BackColor = Color.White;
            cbxListName.BackColor = Color.White;
            cb2.BackColor = Color.White;
            fontSizeComboBox.BackColor = Color.White;

            // Configure DataGridView appearance
            dgv1.BackgroundColor = Color.White;
            dgv1.DefaultCellStyle.BackColor = Color.White;
            dgv1.GridColor = Color.LightGray;
            dgv1.BorderStyle = BorderStyle.Fixed3D;
            dgv1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv1.EnableHeadersVisualStyles = false;
            dgv1.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgv1.ColumnHeadersDefaultCellStyle.ForeColor = Color.DarkSlateGray;

            // Setup combo box style and autocomplete
            cbxListName.DropDownStyle = ComboBoxStyle.DropDown;
            cbxListName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbxListName.AutoCompleteSource = AutoCompleteSource.ListItems;

            MasterData master = SaveJSON.LoadMasterData();

            // If empty, maybe we create a default list
            if (master.Lists.Count == 0)
            {
                Info info = new Info
                {
                    Name = "TestList",
                    cols = new List<string> { "Col1", "Col2", "Col3" },
                    strs = new List<List<string>>
                    {
                        new List<string> { "Text1", "Text2", "Text3" },
                        new List<string> { "Tezt1", "Tezt2", "Tezt3" }
                    }
                };

                master.Lists["0"] = info;
                master.Save();
            }

            // Now populate the combo & show the first list
            popCbxListName();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading lists: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void cbxListName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cbxListName.SelectedIndex == -1) return;

            // Get the selected item from the combo box
            if (cbxListName.SelectedItem is CbxItem<string> selectedItem)
            {
                // Get the key value directly from the selected item
                string key = selectedItem.Value;

                // Load the master data
                MasterData master = SaveJSON.LoadMasterData();

                // Check if the key exists in the master data
                if (master.Lists.ContainsKey(key))
                {
                    populateDGV1(key);
                    return;
                }

                // If we get here, the key doesn't exist in the master data
                // This shouldn't happen if the combo box is properly populated
                MessageBox.Show($"List not found: {selectedItem.Name} (Key: {key})", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // This shouldn't happen if the combo box is properly populated
                MessageBox.Show("Invalid selection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error selecting list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void lv1_SelectedIndexChanged(object sender, EventArgs e)
    {
        // (Unused in original code)
    }

    // Handle right-click on cell to open text editor
    private void Dgv1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
        {
            // Select the cell that was right-clicked
            dgv1.CurrentCell = dgv1.Rows[e.RowIndex].Cells[e.ColumnIndex];

            // Check if this column is multiline
            string columnName = dgv1.Columns[e.ColumnIndex].Name;
            int columnIndex = GetColumnIndexInData(columnName);

            if (IsMultiLineColumn(columnIndex))
            {
                // Show context menu
                Point cellLocation = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                cellContextMenu.Show(dgv1, new Point(cellLocation.X + 15, cellLocation.Y + 15));
            }
        }
    }

    // Handle double-click on multiline cell to open text editor
    private void Dgv1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
        {
            // Check if this column is multiline
            string columnName = dgv1.Columns[e.ColumnIndex].Name;
            int columnIndex = GetColumnIndexInData(columnName);

            if (IsMultiLineColumn(columnIndex))
            {
                OpenTextEditor(e.RowIndex, e.ColumnIndex);
            }
        }
    }

    // Helper method to get the index of a column in the data
    private int GetColumnIndexInData(string columnName)
    {
        string listName = cbxListName.Text;
        if (string.IsNullOrEmpty(listName)) return -1;

        MasterData master = SaveJSON.LoadMasterData();
        if (!master.Lists.ContainsKey(listName)) return -1;

        Info data = master.Lists[listName];
        if (data.cols == null) return -1;

        return data.cols.IndexOf(columnName);
    }

    // Helper method to check if a column is multiline
    private bool IsMultiLineColumn(int columnIndex)
    {
        if (columnIndex < 0) return false;

        string listName = cbxListName.Text;
        if (string.IsNullOrEmpty(listName)) return false;

        MasterData master = SaveJSON.LoadMasterData();
        if (!master.Lists.ContainsKey(listName)) return false;

        Info data = master.Lists[listName];
        if (data.colIsMultiLine == null || columnIndex >= data.colIsMultiLine.Count) return false;

        return data.colIsMultiLine[columnIndex];
    }

    // Open the text editor for a cell
    private void EditTextMenuItem_Click(object sender, EventArgs e)
    {
        if (dgv1.CurrentCell != null)
        {
            OpenTextEditor(dgv1.CurrentCell.RowIndex, dgv1.CurrentCell.ColumnIndex);
        }
    }

    // Open the text editor for a cell
    private void OpenTextEditor(int rowIndex, int columnIndex)
    {
        if (rowIndex < 0 || columnIndex < 0) return;

        // Get the current value
        string currentValue = dgv1.Rows[rowIndex].Cells[columnIndex].Value?.ToString() ?? "";

        // Open the text editor form
        using (var editor = new TextEditorForm())
        {
            editor.CellValue = currentValue;
            if (editor.ShowDialog() == DialogResult.OK)
            {
                // Update the cell value
                dgv1.Rows[rowIndex].Cells[columnIndex].Value = editor.CellValue;

                // Save the changes to the data
                SaveCellValue(rowIndex, columnIndex, editor.CellValue);
            }
        }
    }

    // Save a cell value to the data
    private void SaveCellValue(int rowIndex, int columnIndex, string value)
    {
        // Get the display name from the combo box
        string displayName = cbxListName.Text;
        if (string.IsNullOrEmpty(displayName)) return;

        MasterData master = SaveJSON.LoadMasterData();
        string actualKey = null;
        Info data = null;

        // First try direct key lookup
        if (master.Lists.ContainsKey(displayName))
        {
            actualKey = displayName;
            data = master.Lists[displayName];
        }
        else
        {
            // Try case-insensitive key lookup
            foreach (var kvp in master.Lists)
            {
                if (string.Equals(kvp.Key, displayName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(kvp.Value.Name, displayName, StringComparison.OrdinalIgnoreCase))
                {
                    actualKey = kvp.Key;
                    data = kvp.Value;
                    break;
                }
            }
        }

        // If we couldn't find the list, return
        if (data == null || actualKey == null) return;

        // Update the data
        if (data.strs == null || rowIndex >= data.strs.Count) return;

        List<string> row = data.strs[rowIndex];
        if (columnIndex >= row.Count) return;

        row[columnIndex] = value;
        master.Lists[actualKey] = data;
        master.Save();
    }

    public void populateDGV1(string listKey, bool saveChanges = false)
    {
        if (string.IsNullOrEmpty(listKey)) return;

        MasterData master = SaveJSON.LoadMasterData();

        // First try direct key lookup
        if (master.Lists.ContainsKey(listKey))
        {
            Info data = master.Lists[listKey];
            PopulateGridWithData(data, listKey, saveChanges);
            return;
        }

        // Try case-insensitive key lookup
        foreach (var kvp in master.Lists)
        {
            if (
                string.Equals(kvp.Key, listKey, StringComparison.OrdinalIgnoreCase)
                //||string.Equals(kvp.Value.Name, listName, StringComparison.OrdinalIgnoreCase))
                )
            {
                Info data = kvp.Value;
                PopulateGridWithData(data, kvp.Key, saveChanges);
                return;
            }
        }

        // If we get here, the list wasn't found
        MessageBox.Show($"List '{listKey}' not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }


    private void PopulateGridWithData(Info data, string listKey, bool saveChanges = false)
    {
        this.SuspendLayout();
        dgv1.SuspendLayout();
        dgv1.Rows.Clear();
        dgv1.Columns.Clear();

        // Add ID column
        DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
        idColumn.Name = "ID";
        idColumn.HeaderText = "ID";
        idColumn.Width = 30; // Make it narrower
        idColumn.ReadOnly = true;
        idColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // Prevent auto-sizing
        dgv1.Columns.Add(idColumn);

        // First make sure we have valid column names
        if (data.cols == null) data.cols = new List<string>();

        var uniqueColumns = new List<string>();
        for (int i = 0; i < data.cols.Count; i++)
        {
            string colName = data.cols[i];
            if (string.IsNullOrEmpty(colName))
            {
                colName = $"Column{i + 1}";
            }

            // Make sure column name is unique
            string uniqueName = colName;
            int suffix = 1;
            while (uniqueColumns.Contains(uniqueName))
            {
                uniqueName = $"{colName}_{suffix++}";
            }
            uniqueColumns.Add(uniqueName);

            // Update the column name in the data
            data.cols[i] = uniqueName;
        }

        // Now set up the grid columns (add columns after the ID column)
        for (int i = 0; i < uniqueColumns.Count; i++)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = uniqueColumns[i];
            column.HeaderText = uniqueColumns[i];
            dgv1.Columns.Add(column);

            // Check if this column is password protected
            bool isPassword = false;
            if (data.colIsPassword != null && i < data.colIsPassword.Count)
            {
                isPassword = data.colIsPassword[i];
            }

            // Check if this column is multiline
            bool isMultiLine = false;
            if (data.colIsMultiLine != null && i < data.colIsMultiLine.Count)
            {
                isMultiLine = data.colIsMultiLine[i];
            }

            // Get the index of the column we just added (ID column + i)
            int columnIndex = i + 1; // +1 for the ID column

            // Configure column for multiline if needed
            if (isMultiLine)
            {
                // Set multiline properties
                DataGridViewCellStyle multilineStyle = new DataGridViewCellStyle();
                multilineStyle.WrapMode = DataGridViewTriState.True;
                dgv1.Columns[columnIndex].DefaultCellStyle = multilineStyle;

                // Set fixed row height instead of auto-sizing
                dgv1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgv1.RowTemplate.Height = 50; // Fixed height for rows with multiline content
            }

            // Configure column for password if needed
            if (isPassword)
            {
                // Tag the column as password protected for reference
                dgv1.Columns[columnIndex].Tag = "password";
            }
        }

        // Add the rows
        if (data.strs == null) data.strs = new List<List<string>>();

        // Track row index for ID column
        int rowIndex = 0;

        foreach (List<string> rowData in data.strs)
        {
            // Make sure row has enough cells
            while (rowData.Count < uniqueColumns.Count)
            {
                rowData.Add(string.Empty);
            }

            // Create a copy of the row data for display
            var displayData = new string[rowData.Count];
            for (int i = 0; i < rowData.Count; i++)
            {
                // Check if this column is password protected
                bool isColumnPassword = false;
                if (data.colIsPassword != null && i < data.colIsPassword.Count)
                {
                    isColumnPassword = data.colIsPassword[i];
                }

                // If this is a password column, mask the data with asterisks
                if (isColumnPassword || (data.pswd && i == 1)) // Check both column-specific and legacy password flag
                {
                    displayData[i] = !string.IsNullOrEmpty(rowData[i]) ? "***" : "";
                }
                else
                {
                    displayData[i] = rowData[i];
                }
            }

            // Create a new row with the right number of cells
            dgv1.Rows.Add();
            int newRowIndex = dgv1.Rows.Count - 1;

            // Set the ID column value (row number)
            dgv1.Rows[newRowIndex].Cells[0].Value = (rowIndex + 1).ToString();

            // Set the data for the other columns (offset by 1 for the ID column)
            for (int i = 0; i < displayData.Length; i++)
            {
                // Make sure we don't go out of bounds
                if (i + 1 < dgv1.Columns.Count)
                {
                    dgv1.Rows[newRowIndex].Cells[i + 1].Value = displayData[i];

                    // Store the original value in the Tag property for password cells
                    if (dgv1.Columns[i + 1].Tag != null && dgv1.Columns[i + 1].Tag.ToString() == "password" && !string.IsNullOrEmpty(rowData[i]))
                    {
                        dgv1.Rows[newRowIndex].Cells[i + 1].Tag = rowData[i];
                    }
                }
            }

            rowIndex++;

            // We've already handled storing original values for password cells above
            // No need to do anything else here
        }

        // Rows have been added directly in the loop above

        dgv1.ResumeLayout();
        this.ResumeLayout();

        // Apply font size from data
        SelectFontSizeInComboBox(data.size);
        ApplyFontSize(data.size);

        if (saveChanges)
        {
            // Load master data to ensure we're working with the latest data
            MasterData master = SaveJSON.LoadMasterData();
            master.Lists[listKey] = data;
            master.Save();
        }
    }

    private void addListToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Use the Edit form with isEditMode set to false instead of the old Add form
        MasterData master = SaveJSON.LoadMasterData();
        Edit addForm = new Edit(false, null, master);

        // Show as dialog to block until user finishes
        DialogResult result = addForm.ShowDialog(this);

        if (result == DialogResult.OK)
        {
            // Refresh the combo box and select the newly added list if possible
            popCbxListName();
        }
    }



    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        // "Copy" or "Point" logic
        if (editChk.Checked) return;
        if (e.RowIndex < 0) return;

        // Get the value, checking first if it's a password field (has Tag property set)
        string textValue;
        if (dgv1.SelectedCells[0].Tag != null)
        {
            // Use the original value stored in Tag for password fields
            textValue = dgv1.SelectedCells[0].Tag.ToString();
        }
        else
        {
            textValue = dgv1.SelectedCells[0].Value?.ToString() ?? "";
        }

        // 0 = Copy, 1 = Click, 2 = Point
        if (cb2.SelectedIndex == 0)
        {
            // Copy to clipboard
            Clipboard.SetText(textValue);
        }
        else if (cb2.SelectedIndex == 2)
        {
            // "Point" = simulate typing
            this.TopMost = false;
            IntPtr hWnd = GetWindow(Process.GetCurrentProcess().MainWindowHandle, 2U);
            while (true)
            {
                IntPtr parent = GetParent(hWnd);
                if (parent != IntPtr.Zero)
                    hWnd = parent;
                else
                    break;
            }
            SetForegroundWindow(hWnd);
            SendKeys.SendWait(textValue);
            this.TopMost = true;
        }
    }

    private void dgv1_LostFocus(object sender, EventArgs e)
    {
        try
        {
            if (cb2.SelectedIndex == 1) // "Click"
            {
                // Possibly send keys on lost focus
                Thread.Sleep(200);

                // Get the value, checking first if it's a password field (has Tag property set)
                string textValue;
                if (dgv1.SelectedCells[0].Tag != null)
                {
                    // Use the original value stored in Tag for password fields
                    textValue = dgv1.SelectedCells[0].Tag.ToString();
                }
                else
                {
                    textValue = dgv1.SelectedCells[0].Value?.ToString() ?? "";
                }

                SendKeys.SendWait(textValue);
            }
        }
        catch
        {
            // ignore
        }
    }

    private void chk1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (editChk.Checked)
            {
                dgv1.ReadOnly = false;
                dgv1.AllowUserToAddRows = true;
                dgv1.EditMode = DataGridViewEditMode.EditOnEnter;
                editModeLabel.Text = "E";
                editModeLabel.Visible = true;
            }
            else
            {
                dgv1.ReadOnly = true;
                dgv1.AllowUserToAddRows = false;
                dgv1.EditMode = DataGridViewEditMode.EditOnKeystroke;
                editModeLabel.Visible = false;

                // Save changes when exiting edit mode
                Save();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error changing edit mode: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btn1_Click(object sender, EventArgs e)
    {
        dgv1.ReadOnly = false;
        dgv1.AllowUserToAddRows = true;
        dgv1.EditMode = DataGridViewEditMode.EditOnEnter;
        editModeLabel.Text = "E";
        editModeLabel.Visible = true;
    }

    private void btn2_Click(object sender, EventArgs e)
    {
        Save();

    }

    private void Save()
    {
        try
        {
            // Show "S" indicator to indicate saving is happening
            editModeLabel.Text = "S";
            editModeLabel.Visible = true;
            // Force UI update to show the "S"
            Application.DoEvents();

            string listName = cbxListName.Items[cbxListName.SelectedIndex].ToString();

            string listKey = null;
            if (cbxListName.SelectedIndex >= 0 && cbxListName.SelectedItem is CbxItem<string> selectedItem)
            {
                listKey = selectedItem.Value;
            }

            if (string.IsNullOrEmpty(listName))
            {
                MessageBox.Show("Please select a list to save.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Hide the indicator if save is canceled
                if (!editChk.Checked) editModeLabel.Visible = false;
                return;
            }

            // Load current master data
            MasterData master = SaveJSON.LoadMasterData();

            // Create info object for current list
            var info = new Info();

            // Gather columns
            info.cols = new List<string>();
            foreach (DataGridViewColumn column in dgv1.Columns)
            {
                info.cols.Add(column.Name);
            }

            // Gather rows
            info.strs = new List<List<string>>();
            foreach (DataGridViewRow row in dgv1.Rows)
            {
                if (row.IsNewRow) continue;

                var rowData = new List<string>();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    rowData.Add(cell.Value?.ToString() ?? "");
                }
                info.strs.Add(rowData);
            }

            // Copy metadata from existing list if it exists
            if (master.Lists.TryGetValue(listKey, out Info existing))
            {
                info.cbmz = existing.cbmz;
                info.cbname = existing.cbname;
                info.pswd = existing.pswd;
                info.size = existing.size;
            }
            else
            {
                info.cbmz = "cbmz";
                info.cbname = listName;
            }
            info.Name = listName;
            info.cbdate = DateTime.Now;

            //  var key = master.Lists.Where(x => x.Value.Name == listName).FirstOrDefault().Key;

            // Update the list
            master.Lists[listKey] = info;

            // Save back to file
            master.Save();

            // Reload the grid with saved data

            //populateDGV1(listKey, false);  // Don't save changes when reloading

            // If we're not in edit mode, hide the indicator
            if (!editChk.Checked)
            {
                editModeLabel.Visible = false;

                dgv1.ReadOnly = true;
                dgv1.AllowUserToAddRows = false;
                dgv1.EditMode = DataGridViewEditMode.EditOnKeystroke;
                editModeLabel.Visible = false;
            }
            else
            {
                // If we're in edit mode, change back to "E"
                editModeLabel.Text = "E";
            }

            MessageBox.Show("Changes saved successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving changes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Reset the indicator
            if (editChk.Checked)
            {
                editModeLabel.Text = "E";
            }
            else
            {
                editModeLabel.Visible = false;
            }
        }
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var about = new About();
        about.ShowDialog(this);
    }

    private void saveAsEncryptedToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SaveJSON.UseEncryption = true;
        saveAsEncryptedToolStripMenuItem.Checked = true;
        saveAsNormalToolStripMenuItem.Checked = false;
        MessageBox.Show("Saving as Encrypted");
    }

    private void saveAsNormalToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SaveJSON.UseEncryption = false;
        saveAsEncryptedToolStripMenuItem.Checked = false;
        saveAsNormalToolStripMenuItem.Checked = true;
        MessageBox.Show("Saving as Normal");
    }

    public void chkEncrypt()
    {
        // Called externally if needed
        SaveJSON.UseEncryption = Environment.GetEnvironmentVariable("encrypt") == "1";
        saveAsEncryptedToolStripMenuItem.Checked = SaveJSON.UseEncryption;
        saveAsNormalToolStripMenuItem.Checked = !SaveJSON.UseEncryption;
    }

    private void u_Click(object sender, EventArgs e)
    {
        // Move row up
        try
        {
            int rowIndex = dgv1.SelectedCells[0].OwningRow.Index;
            if (rowIndex == 0) return;

            int colIndex = dgv1.SelectedCells[0].OwningColumn.Index;
            var row = dgv1.Rows[rowIndex];
            dgv1.Rows.Remove(row);
            dgv1.Rows.Insert(rowIndex - 1, row);
            dgv1.ClearSelection();
            dgv1.Rows[rowIndex - 1].Cells[colIndex].Selected = true;
        }
        catch
        {
            // ignore
        }
    }

    private void d_Click(object sender, EventArgs e)
    {
        // Move row down
        try
        {
            int rowCount = dgv1.Rows.Count;
            int rowIndex = dgv1.SelectedCells[0].OwningRow.Index;
            if (rowIndex == rowCount - 2) return;
            // or rowCount - 1 if your grid has no extra row

            int colIndex = dgv1.SelectedCells[0].OwningColumn.Index;
            var row = dgv1.Rows[rowIndex];
            dgv1.Rows.Remove(row);
            dgv1.Rows.Insert(rowIndex + 1, row);
            dgv1.ClearSelection();
            dgv1.Rows[rowIndex + 1].Cells[colIndex].Selected = true;
        }
        catch
        {
            // ignore
        }
    }

    private void topButton_Click(object sender, EventArgs e)
    {
        // Move row to the top
        try
        {
            int rowIndex = dgv1.SelectedCells[0].OwningRow.Index;
            if (rowIndex == 0) return;

            int colIndex = dgv1.SelectedCells[0].OwningColumn.Index;
            var row = dgv1.Rows[rowIndex];
            dgv1.Rows.Remove(row);
            dgv1.Rows.Insert(0, row);
            dgv1.ClearSelection();
            dgv1.Rows[0].Cells[colIndex].Selected = true;
        }
        catch
        {
            // ignore
        }
    }

    private void bottomButton_Click(object sender, EventArgs e)
    {
        // Move row to the bottom
        try
        {
            int rowCount = dgv1.Rows.Count;
            int rowIndex = dgv1.SelectedCells[0].OwningRow.Index;
            int lastRowIndex = rowCount - 2; // Accounting for the new row at the end
            if (rowIndex == lastRowIndex) return;

            int colIndex = dgv1.SelectedCells[0].OwningColumn.Index;
            var row = dgv1.Rows[rowIndex];
            dgv1.Rows.Remove(row);
            dgv1.Rows.Insert(lastRowIndex, row);
            dgv1.ClearSelection();
            dgv1.Rows[lastRowIndex].Cells[colIndex].Selected = true;
        }
        catch
        {
            // ignore
        }
    }

    private void leftButton_Click(object sender, EventArgs e)
    {
        // Move cell content left (swap with left cell)
        try
        {
            int rowIndex = dgv1.SelectedCells[0].OwningRow.Index;
            int colIndex = dgv1.SelectedCells[0].OwningColumn.Index;

            // Check if we can move left
            if (colIndex <= 0) return;

            // Get current and left cell values
            var currentCell = dgv1.Rows[rowIndex].Cells[colIndex];
            var leftCell = dgv1.Rows[rowIndex].Cells[colIndex - 1];

            // Swap values
            object tempValue = currentCell.Value;
            currentCell.Value = leftCell.Value;
            leftCell.Value = tempValue;

            // Select the left cell
            dgv1.ClearSelection();
            dgv1.Rows[rowIndex].Cells[colIndex - 1].Selected = true;
        }
        catch
        {
            // ignore
        }
    }

    private void rightButton_Click(object sender, EventArgs e)
    {
        // Move cell content right (swap with right cell)
        try
        {
            int rowIndex = dgv1.SelectedCells[0].OwningRow.Index;
            int colIndex = dgv1.SelectedCells[0].OwningColumn.Index;

            // Check if we can move right
            if (colIndex >= dgv1.Columns.Count - 1) return;

            // Get current and right cell values
            var currentCell = dgv1.Rows[rowIndex].Cells[colIndex];
            var rightCell = dgv1.Rows[rowIndex].Cells[colIndex + 1];

            // Swap values
            object tempValue = currentCell.Value;
            currentCell.Value = rightCell.Value;
            rightCell.Value = tempValue;

            // Select the right cell
            dgv1.ClearSelection();
            dgv1.Rows[rowIndex].Cells[colIndex + 1].Selected = true;
        }
        catch
        {
            // ignore
        }
    }

    private void fontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (fontSizeComboBox.SelectedItem == null) return;

            int fontSize = GetSelectedFontSize();
            if (fontSize > 0)
            {
                ApplyFontSize(fontSize);

                // Save the font size setting to the current list
                string listName = cbxListName.Text;
                if (!string.IsNullOrEmpty(listName))
                {
                    MasterData master = SaveJSON.LoadMasterData();
                    if (master.Lists.ContainsKey(listName))
                    {
                        master.Lists[listName].size = fontSize;
                        master.Save();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error changing font size: " + ex.Message);
        }
    }


    private void ApplyFontSize(int fontSize)
    {
        // Create new font with the selected size
        Font newFont = new Font(dgv1.Font.FontFamily, fontSize, dgv1.Font.Style);

        // Update DataGridView font
        dgv1.Font = newFont;

        // Update column headers font
        dgv1.ColumnHeadersDefaultCellStyle.Font = newFont;

        // Update default cell style font for all cells
        dgv1.DefaultCellStyle.Font = newFont;
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

    private void passwordGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (var form = new PasswordGeneratorForm())
        {
            form.ShowDialog(this);
        }
    }

    private void migrateXmlToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(
            text: "This will migrate any XML files found in the application folder to the new JSON format.",
            caption: "Migrate XML Files",
            buttons: MessageBoxButtons.YesNo,
            icon: MessageBoxIcon.Question) == DialogResult.Yes)
        {
            DataMigrator.MigrateXmlToJson();

            // Refresh the current view
            popCbxListName();
        }
    }

    private void openDataFolderToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            // Open the executable directory in Windows Explorer
            Process.Start("explorer.exe", App.ExecutablePath);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error opening data folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void popCbxListName(string selectListVal = null)
    {
        try
        {
            // Load the entire MasterData once
            MasterData master = SaveJSON.LoadMasterData();

            // Ensure all lists have Names (but don't change keys)
            master.EnsureListsHaveNames();

            // Remember current selection
            string currentKey = null;
            if (cbxListName.SelectedIndex >= 0 && cbxListName.SelectedItem is CbxItem<string> selectedItem)
            {
                currentKey = selectedItem.Value;
            }

            cbxListName.BeginUpdate();
            cbxListName.Items.Clear();

            // Add all list names to the combo box using CbxItem<string>
            foreach (var kvp in master.Lists)
            {
                string key = kvp.Key;
                Info info = kvp.Value;

                // For display in the combo box, use the Name property if available, otherwise use the key
                string displayName = string.IsNullOrEmpty(info.Name) ? key : info.Name;

                // Add a CbxItem with display name and key value to the combo box
                cbxListName.Items.Add(new CbxItem<string>(displayName, key));
            }

            // Suspend layout of the grid to prevent flicker
            dgv1.SuspendLayout();

            // If a specific list was requested, select it
            if (!string.IsNullOrEmpty(selectListVal))
            {
                // Find the index of the item with matching key or name
                int index = -1;
                for (int i = 0; i < cbxListName.Items.Count; i++)
                {
                    if (cbxListName.Items[i] is CbxItem<string> item)
                    {
                        // Check if the key matches
                        if (item.Value == selectListVal)
                        {
                            index = i;
                            break;
                        }

                        // Check if the name matches (case-insensitive)
                        if (string.Equals(item.Name, selectListVal, StringComparison.OrdinalIgnoreCase))
                        {
                            index = i;
                            break;
                        }
                    }
                }

                if (index >= 0)
                {
                    cbxListName.SelectedIndex = index;
                    var selectedListItem = (CbxItem<string>)cbxListName.SelectedItem;
                    populateDGV1(selectedListItem.Value, false);  // Don't save changes when just displaying
                }
                else if (master.Lists.ContainsKey(selectListVal))
                {
                    // If we couldn't find the item but the key exists, populate the grid directly
                    populateDGV1(selectListVal, false);
                }
            }
            // Otherwise try to keep the current selection if it exists
            else if (!string.IsNullOrEmpty(currentKey) && master.Lists.ContainsKey(currentKey))
            {
                // Find the index of the item with matching key
                int index = -1;
                for (int i = 0; i < cbxListName.Items.Count; i++)
                {
                    if (cbxListName.Items[i] is CbxItem<string> item && item.Value == currentKey)
                    {
                        index = i;
                        break;
                    }
                }

                if (index >= 0)
                {
                    cbxListName.SelectedIndex = index;
                    populateDGV1(currentKey, false);  // Don't save changes when just displaying
                }
            }
            // Otherwise select the first item
            else if (cbxListName.Items.Count > 0)
            {
                cbxListName.SelectedIndex = 0;
                var firstItem = (CbxItem<string>)cbxListName.Items[0];
                populateDGV1(firstItem.Value, false);  // Don't save changes when just displaying
            }

            cbxListName.EndUpdate();
            dgv1.ResumeLayout();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error updating lists: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Updates the combo box with list names and selects the specified list
    /// </summary>
    /// <param name="listNameToSelect">The name of the list to select after populating</param>
    //public void popCbxListName(string listNameToSelect = null)
    //{
    //    // Save the current selection if no specific selection is requested
    //    string currentSelection = listNameToSelect ?? cbxListName.Text;

    //    // Clear and repopulate the combo box
    //    cbxListName.Items.Clear();

    //    // Load the master data
    //    MasterData master = SaveJSON.LoadMasterData();

    //    // Add each list to the combo box
    //    foreach (var kvp in master.Lists)
    //    {
    //        string key = kvp.Key;
    //        Info info = kvp.Value;

    //        // For display in the combo box, use the Name property if available, otherwise use the key
    //        string displayName = string.IsNullOrEmpty(info.Name) ? key : info.Name;

    //        // Add a CbxItem with display name and key value to the combo box
    //        cbxListName.Items.Add(new CbxItem<string>(displayName, key));
    //    }

    //    // Try to select the requested list
    //    if (!string.IsNullOrEmpty(listNameToSelect))
    //    {
    //        // Try to find the item by name
    //        for (int i = 0; i < cbxListName.Items.Count; i++)
    //        {
    //            if (cbxListName.Items[i] is CbxItem<string> item &&
    //                (string.Equals(item.Name, listNameToSelect, StringComparison.OrdinalIgnoreCase) ||
    //                 string.Equals(item.Value, listNameToSelect, StringComparison.OrdinalIgnoreCase)))
    //            {
    //                cbxListName.SelectedIndex = i;
    //                return;
    //            }
    //        }
    //    }
    //    else if (!string.IsNullOrEmpty(currentSelection))
    //    {
    //        // Try to reselect the previous selection
    //        for (int i = 0; i < cbxListName.Items.Count; i++)
    //        {
    //            if (cbxListName.Items[i] is CbxItem<string> item &&
    //                (string.Equals(item.Name, currentSelection, StringComparison.OrdinalIgnoreCase) ||
    //                 string.Equals(item.Value, currentSelection, StringComparison.OrdinalIgnoreCase)))
    //            {
    //                cbxListName.SelectedIndex = i;
    //                return;
    //            }
    //        }
    //    }

    //    // If no match found and there are items, select the first one
    //    if (cbxListName.Items.Count > 0 && cbxListName.SelectedIndex < 0)
    //    {
    //        cbxListName.SelectedIndex = 0;
    //    }
    //}


    //private void editListToolStripMenuItem_Click(object sender, EventArgs e)
    //{
    //    string listName = cbxListName.Text;
    //    if (string.IsNullOrEmpty(listName)) return;

    //    MasterData master = SaveJSON.LoadMasterData();
    //    if (!master.Lists.ContainsKey(listName)) return;

    //    Edit editForm = new Edit(true, listName, master);
    //    editForm.Show(this);

    //    // Refresh the current list after editing
    //    populateDGV1(listName);
    //}

    // Search functionality methods
    private void TbxSrch_KeyUp(object sender, KeyEventArgs e)
    {
        // Reset and restart the timer on each key press
        searchTimer.Stop();
        searchTimer.Start();
    }

    private void SearchTimer_Tick(object sender, EventArgs e)
    {
        // Stop the timer since we're processing now
        searchTimer.Stop();

        // Get the search text
        string searchText = tbxSrch.Text.Trim().ToLower();

        // If search text hasn't changed, don't reprocess
        if (searchText == lastSearchText) return;

        // Update last search text
        lastSearchText = searchText;

        // If search text is empty, show all rows
        if (string.IsNullOrWhiteSpace(searchText))
        {
            ClearSearch();
            return;
        }

        // Set search active flag
        isSearchActive = true;

        // Filter the DataGridView
        FilterDataGridView(searchText);
    }

    private void FilterDataGridView(string searchText)
    {
        // Clear previous filtered rows
        filteredRows.Clear();

        // Show the processing indicator
        Cursor.Current = Cursors.WaitCursor;

        // Suspend layout to prevent flickering
        dgv1.SuspendLayout();

        try
        {
            // First pass: Find all rows that match the search text
            for (int rowIndex = 0; rowIndex < dgv1.Rows.Count; rowIndex++)
            {
                // Skip the new row at the end
                if (dgv1.Rows[rowIndex].IsNewRow) continue;

                bool rowMatches = false;

                // Check each cell in the row
                foreach (DataGridViewCell cell in dgv1.Rows[rowIndex].Cells)
                {
                    // Skip null values
                    if (cell.Value == null) continue;

                    // Get cell value (handle password cells)
                    string cellValue;
                    if (cell.Tag != null)
                    {
                        // Use the original value stored in Tag for password fields
                        cellValue = cell.Tag.ToString().ToLower();
                    }
                    else
                    {
                        cellValue = cell.Value.ToString().ToLower();
                    }

                    // Check if the cell contains the search text
                    if (cellValue.Contains(searchText))
                    {
                        rowMatches = true;
                        break;
                    }
                }

                // If the row matches, add it to the filtered rows
                if (rowMatches)
                {
                    filteredRows.Add(rowIndex);
                }
            }

            // Second pass: Hide rows that don't match
            for (int rowIndex = 0; rowIndex < dgv1.Rows.Count; rowIndex++)
            {
                // Skip the new row at the end
                if (dgv1.Rows[rowIndex].IsNewRow) continue;

                // Show or hide the row based on whether it's in the filtered rows
                dgv1.Rows[rowIndex].Visible = filteredRows.Contains(rowIndex);
            }

            // If no rows match, show a message
            if (filteredRows.Count == 0 && dgv1.Rows.Count > 1) // > 1 to account for the new row
            {
                //  MessageBox.Show($"No matches found for '{searchText}'.", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        finally
        {
            // Resume layout
            dgv1.ResumeLayout();

            // Reset cursor
            Cursor.Current = Cursors.Default;
        }
    }

    private void ClrBtn_Click(object sender, EventArgs e)
    {
        // Clear the search text box
        tbxSrch.Clear();

        // Clear the search filter
        ClearSearch();

        // Set focus back to the search text box
        tbxSrch.Focus();
    }

    private void ClearSearch()
    {
        // If search is not active, nothing to do
        if (!isSearchActive) return;

        // Reset search state
        isSearchActive = false;
        lastSearchText = string.Empty;
        filteredRows.Clear();

        // Show all rows
        dgv1.SuspendLayout();

        try
        {
            foreach (DataGridViewRow row in dgv1.Rows)
            {
                row.Visible = true;
            }
        }
        finally
        {
            dgv1.ResumeLayout();
        }
    }

    private void editListToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Make sure a list is selected
        if (cbxListName.SelectedIndex < 0)
        {
            MessageBox.Show("Please select a list to edit.", "Edit List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // Get the selected item from the combo box
        if (!(cbxListName.SelectedItem is CbxItem<string> selectedItem))
        {
            MessageBox.Show("Invalid selection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Get the key value directly from the selected item
        string key = selectedItem.Value;

        // Load the master data
        MasterData master = SaveJSON.LoadMasterData();

        // Check if the key exists in the master data
        if (!master.Lists.ContainsKey(key))
        {
            MessageBox.Show($"List not found: {selectedItem.Name} (Key: {key})", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Open the Edit form with the selected list
        var form = new Edit(true, key, master);
        form.Owner = this; // Set the Owner property so Edit.cs can call back to Form1
        form.Show();

        // Refresh the current list
        //populateDGV1(key);

        // Note: We're not calling popCbxListName here because the Edit form will handle refreshing the list
        // If you want to refresh the combo box after editing, you can uncomment the line below
        // popCbxListName(key);
    }


}
