using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ClipBox2
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      // Initialize environment variables
      Environment.SetEnvironmentVariable("cbFol", AppDomain.CurrentDomain.BaseDirectory);
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
                SaveJSON.SaveMasterData(master);
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
            
            rows[rowIndex++] = new DataGridViewRow();
            rows[rowIndex - 1].CreateCells(dgv1, rowData.ToArray());
        }
        
        // Add all rows at once
        dgv1.Rows.AddRange(rows);
        
        dgv1.ResumeLayout();
        this.ResumeLayout();
        
        if (saveChanges)
        {
            master.Lists[listName] = data;
            SaveJSON.SaveMasterData(master);
        }
    }


    private void addListToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (var frmAdd = new Add())
        {
        frmAdd.ShowDialog();
        }
    }

    private void editListToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (var frmEdit = new Edit())
      {
        frmEdit.ShowDialog();
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
      if (chk1.Checked) return;
      if (e.RowIndex < 0) return;

      string textValue = dgv1.SelectedCells[0].Value?.ToString() ?? "";

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
          string textValue = dgv1.SelectedCells[0].Value?.ToString() ?? "";
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
      // Toggle Edit Mode in the DataGridView
      if (chk1.Checked)
      {
        dgv1.ReadOnly = false;
        dgv1.AllowUserToAddRows = true;
        dgv1.AllowUserToDeleteRows = true;
        dgv1.AllowUserToOrderColumns = true;
        dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      }
      else
      {
        // Save changes + lock the grid again
        Save();
        dgv1.ReadOnly = true;
        dgv1.AllowUserToAddRows = false;
        dgv1.AllowUserToDeleteRows = false;
        dgv1.AllowUserToOrderColumns = false;
        dgv1.SelectionMode = DataGridViewSelectionMode.CellSelect;
      }
    }

    private void btn1_Click(object sender, EventArgs e)
    {
        Save();
    }

    private void Save()
    {
        try
        {
            string listName = cb1.Items[cb1.SelectedIndex].ToString();
            if (string.IsNullOrEmpty(listName))
            {
                MessageBox.Show("Please select a list to save.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            SaveJSON.SaveMasterData(master);

            // Reload the grid with saved data
            populate(listName, false);  // Don't save changes when reloading

            MessageBox.Show("Changes saved successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving changes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var about = new About();
        about.Show();
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

    // Decompiled enum from the original code
    private enum GetWindow_Cmd : uint
    {
      GW_HWNDFIRST,
      GW_HWNDLAST,
      GW_HWNDNEXT,
      GW_HWNDPREV,
      GW_OWNER,
      GW_CHILD,
      GW_ENABLEDPOPUP,
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
