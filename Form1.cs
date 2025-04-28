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

namespace ClipBox2
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
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

            dgv1.AllowUserToAddRows = false;
            dgv1.AllowUserToDeleteRows = false;
            dgv1.AllowUserToOrderColumns = false;
            dgv1.ReadOnly = true;

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
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Check for Ctrl+E to toggle edit mode
            if (e.Control && e.KeyCode == Keys.E)
            {
                e.Handled = true; // Mark as handled to prevent further processing
                e.SuppressKeyPress = true; // Suppress the key press to prevent it from being processed elsewhere

                // Toggle the edit mode checkbox
                chk1.Checked = !chk1.Checked;

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
                MasterData master = SaveJSON.LoadMasterData();

                // If empty, maybe we create a default list
                if (master.Lists.Count == 0)
                {
                    Info info = new Info
                    {
                        cols = new List<string> { "Col1", "Col2", "Col3" },
                        strs = new List<List<string>>
                    {
                        new List<string> { "Text1", "Text2", "Text3" },
                        new List<string> { "Tezt1", "Tezt2", "Tezt3" }
                    }
                    };

                    master.Lists["TestList"] = info;
                    master.Save();
                }

                // Now populate the combo & show the first list
                popcombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading lists: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cb1.SelectedIndex == -1) return;
                string listName = cb1.Items[cb1.SelectedIndex].ToString();
                populate(listName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // (Unused in original code)
        }

        public void populate(string listName, bool saveChanges = false)
        {
            if (string.IsNullOrEmpty(listName)) return;

            MasterData master = SaveJSON.LoadMasterData();
            if (!master.Lists.ContainsKey(listName))
            {
                MessageBox.Show($"List '{listName}' not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Info data = master.Lists[listName];

            this.SuspendLayout();
            dgv1.SuspendLayout();
            dgv1.Rows.Clear();
            dgv1.Columns.Clear();

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

            // Now set up the grid columns
            dgv1.ColumnCount = uniqueColumns.Count;
            for (int i = 0; i < uniqueColumns.Count; i++)
            {
                dgv1.Columns[i].Name = uniqueColumns[i];
                dgv1.Columns[i].HeaderText = uniqueColumns[i];
            }

            // Add the rows
            if (data.strs == null) data.strs = new List<List<string>>();

            // Pre-allocate rows array
            var rows = new DataGridViewRow[data.strs.Count];
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
                    // If password protection is enabled, mask the data with asterisks
                    if (data.pswd && i == 1) // Col2 is at index 1
                    {
                        displayData[i] = !string.IsNullOrEmpty(rowData[i]) ? "****" : "";
                    }
                    else
                    {
                        displayData[i] = rowData[i];
                    }
                }

                rows[rowIndex++] = new DataGridViewRow();
                rows[rowIndex - 1].CreateCells(dgv1, displayData);

                // Store the original values in the Tag property for copy operations
                for (int i = 0; i < rowData.Count && i < uniqueColumns.Count; i++)
                {
                    if (data.pswd && i == 1) // Col2 is at index 1
                    {
                        rows[rowIndex - 1].Cells[i].Tag = rowData[i]; // Store original value
                    }
                }
            }

            // Add all rows at once
            dgv1.Rows.AddRange(rows);

            dgv1.ResumeLayout();
            this.ResumeLayout();

            // Apply font size from data
            SelectFontSizeInComboBox(data.size);
            ApplyFontSize(data.size);

            if (saveChanges)
            {
                master.Lists[listName] = data;
                master.Save();
            }
        }

        private void addListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add addForm = new Add();
            addForm.ShowDialog(this);

            // Refresh the combo box
            cb1.Items.Clear();
            MasterData master = SaveJSON.LoadMasterData();
            foreach (string listName in master.Lists.Keys)
            {
                cb1.Items.Add(listName);
            }
        }

        private void editListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string listName = cb1.Text;
            if (string.IsNullOrEmpty(listName)) return;

            MasterData master = SaveJSON.LoadMasterData();
            if (!master.Lists.ContainsKey(listName)) return;

            Edit editForm = new Edit(master, listName);
            editForm.ShowDialog(this);

            // Refresh the current list after editing
            populate(listName);
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
            if (chk1.Checked) return;
            if (e.RowIndex < 0) return;

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
                if (chk1.Checked)
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

                string listName = cb1.Items[cb1.SelectedIndex].ToString();
                if (string.IsNullOrEmpty(listName))
                {
                    MessageBox.Show("Please select a list to save.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // Hide the indicator if save is canceled
                    if (!chk1.Checked) editModeLabel.Visible = false;
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
                if (master.Lists.TryGetValue(listName, out Info existing))
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
                info.cbdate = DateTime.Now;

                // Update the list
                master.Lists[listName] = info;

                // Save back to file
                master.Save();

                // Reload the grid with saved data
                populate(listName, false);  // Don't save changes when reloading

                // If we're not in edit mode, hide the indicator
                if (!chk1.Checked)
                {
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
                if (chk1.Checked)
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
                    string listName = cb1.Text;
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
                popcombo();
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

        public void popcombo(string selectList = null)
        {
            try
            {
                // Load the entire MasterData once
                MasterData master = SaveJSON.LoadMasterData();

                // Remember current selection
                string currentSelection = cb1.SelectedIndex >= 0 ? cb1.Items[cb1.SelectedIndex].ToString() : null;

                cb1.BeginUpdate();
                cb1.Items.Clear();
                foreach (string listName in master.Lists.Keys)
                {
                    cb1.Items.Add(listName);
                }

                // Suspend layout of the grid to prevent flicker
                dgv1.SuspendLayout();

                // If a specific list was requested, select it
                if (!string.IsNullOrEmpty(selectList) && master.Lists.ContainsKey(selectList))
                {
                    cb1.SelectedIndex = cb1.Items.IndexOf(selectList);
                    populate(selectList, false);  // Don't save changes when just displaying
                }
                // Otherwise try to keep the current selection if it exists
                else if (!string.IsNullOrEmpty(currentSelection) && master.Lists.ContainsKey(currentSelection))
                {
                    cb1.SelectedIndex = cb1.Items.IndexOf(currentSelection);
                    populate(currentSelection, false);  // Don't save changes when just displaying
                }
                // Otherwise select the first item
                else if (cb1.Items.Count > 0)
                {
                    cb1.SelectedIndex = 0;
                    populate(cb1.Items[0].ToString(), false);  // Don't save changes when just displaying
                }

                cb1.EndUpdate();
                dgv1.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating lists: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
    }
}
