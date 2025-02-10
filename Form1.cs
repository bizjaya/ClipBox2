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


        //private void Form1_Load(object sender, EventArgs e)
        //{
        //  string xmlPath = Environment.GetEnvironmentVariable("cbFol") +
        //                   Environment.GetEnvironmentVariable("fName");

        //  if (File.Exists(xmlPath))
        //  {
        //    popcombo();
        //    populate(xmlPath);
        //  }
        //  else
        //  {
        //    // Create a default XML file if none is found
        //    Info info = new Info
        //    {
        //      cbmz = "cbmz",
        //      cbname = "TestList"
        //    };
        //    info.strs.Add(new List<string> { "Text1", "Text2", "Text3" });
        //    info.strs.Add(new List<string> { "Tezt1", "Tezt2", "Tezt3" });
        //    info.cols.AddRange(new[] { "Col1", "Col2", "Col3" });

        //    // Show first row in the DataGridView
        //    string[] array = info.strs.FirstOrDefault()?.ToArray();
        //    dgv1.ColumnCount = 3;
        //    dgv1.Columns[0].Name = "Col1";
        //    dgv1.Columns[1].Name = "Col2";
        //    dgv1.Columns[2].Name = "Col3";
        //    dgv1.Rows.Add(array);

        //    // Save it
        //    SaveXML.SaveData(info, xmlPath);
        //  }
        //}


    private void Form1_Load(object sender, EventArgs e)
    {
        MasterData master = SaveJSON.LoadMasterData();
        // If empty, maybe we create a default list
        if (master.Lists.Count == 0)
        {
            Info info = new Info
            {
                cbmz = "cbmz",
                cbname = "TestList"
            };
            info.cols.AddRange(new[] { "Col1", "Col2", "Col3" });
            info.strs.Add(new List<string> { "Text1", "Text2", "Text3" });
            info.strs.Add(new List<string> { "Tezt1", "Tezt2", "Tezt3" });

            master.Lists["TestList"] = info;
            SaveJSON.SaveMasterData(master);
        }

        // Now populate the combo & show the first list
        popcombo();
        if (cb1.Items.Count > 0)
        {
            cb1.SelectedIndex = 0;
            string listName = cb1.Text;
            populate(listName);
        }
    }


    //private void cb1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //  // Switch to the newly selected list
    //  Environment.SetEnvironmentVariable("fName", cb1.Text + ".xml");
    //  string fullPath = Environment.GetEnvironmentVariable("cbFol") +
    //                    Environment.GetEnvironmentVariable("fName");
    //  populate(fullPath);
    //}

    private void cb1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string listName = cb1.Text;
        populate(listName);
    }


    private void lv1_SelectedIndexChanged(object sender, EventArgs e)
{
    // (Unused in original code)
}

    //public void populate(string fn)
    //{
    //  Info data = SaveXML.GetData(fn);
    //  dgv1.Rows.Clear();

    //  int colCount = data.cols.Count;
    //  dgv1.ColumnCount = colCount;

    //  for (int i = 0; i < colCount; i++)
    //  {
    //    dgv1.Columns[i].Name = data.cols[i];
    //  }

    //  foreach (List<string> rowData in data.strs)
    //  {
    //    dgv1.Rows.Add(rowData.ToArray());
    //  }
    //}





    //public void popcombo()
    //{
    //  string folder = Environment.GetEnvironmentVariable("cbFol");
    //  string currentFile = Environment.GetEnvironmentVariable("fName");

    //  cb1.Items.Clear();
    //  foreach (string file in Directory.GetFiles(folder, "*.xml"))
    //  {
    //    string baseName = Path.GetFileNameWithoutExtension(file);
    //    cb1.Items.Add(baseName);

    //    if (baseName == Path.GetFileNameWithoutExtension(currentFile))
    //      cb1.Text = baseName;
    //  }
    //}


    public void populate(string listName)
    {
        MasterData master = SaveJSON.LoadMasterData();
        if (!master.Lists.ContainsKey(listName)) return;

        Info data = master.Lists[listName];
        dgv1.Rows.Clear();

        int colCount = data.cols.Count;
        dgv1.ColumnCount = colCount;

        for (int i = 0; i < colCount; i++)
        {
            dgv1.Columns[i].Name = data.cols[i];
        }

        foreach (List<string> rowData in data.strs)
        {
            dgv1.Rows.Add(rowData.ToArray());
        }
    }


    public void popcombo()
    {
        // Load the entire MasterData once
        MasterData master = SaveJSON.LoadMasterData();

        cb1.Items.Clear();
        foreach (string listName in master.Lists.Keys)
        {
            cb1.Items.Add(listName);
        }

        // If there's a "current list" in an env var, we might set that
        // or just pick the first
        string currentList = Environment.GetEnvironmentVariable("fName");
        if (!string.IsNullOrEmpty(currentList))
        {
            // old approach was "fName" = "TestList.xml" so let's just store the base name
            string baseName = Path.GetFileNameWithoutExtension(currentList);
            if (master.Lists.ContainsKey(baseName))
                cb1.Text = baseName;
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
      MessageBox.Show("Saved!");
    }

        //private void Save()
        //{
        //  Info info = new Info
        //  {
        //    cbmz = "cbmz",
        //    cbname = cb1.Text
        //  };

        //  // Gather columns
        //  foreach (DataGridViewColumn column in dgv1.Columns)
        //  {
        //    info.cols.Add(column.Name);
        //  }

        //  // Gather each row
        //  foreach (DataGridViewRow row in dgv1.Rows)
        //  {
        //    string[] rowCells = new string[row.Cells.Count];
        //    for (int i = 0; i < row.Cells.Count; i++)
        //    {
        //      rowCells[i] = row.Cells[i].Value?.ToString() ?? "";
        //    }
        //    info.strs.Add(new List<string>(rowCells));
        //  }

        //  // Save to file
        //  string filename = Environment.GetEnvironmentVariable("cbFol") + cb1.Text + ".xml";
        //  SaveXML.SaveData(info, filename);
        //}



    private void Save()
    {
        string listName = cb1.Text;
        if (string.IsNullOrEmpty(listName)) return;

        MasterData master = SaveJSON.LoadMasterData();

        // If the user typed a name not in the dictionary, we might add a new Info:
        if (!master.Lists.ContainsKey(listName))
        {
            master.Lists[listName] = new Info
            {
                cbmz = "cbmz",
                cbname = listName
            };
        }

        Info info = master.Lists[listName];
        info.cols.Clear();
        info.strs.Clear();

        // Gather columns
        foreach (DataGridViewColumn column in dgv1.Columns)
        {
            info.cols.Add(column.Name);
        }

        // Gather each row
        foreach (DataGridViewRow row in dgv1.Rows)
        {
            var rowCells = new string[row.Cells.Count];
            for (int i = 0; i < row.Cells.Count; i++)
            {
                rowCells[i] = row.Cells[i].Value?.ToString() ?? "";
            }
            info.strs.Add(new List<string>(rowCells));
        }

        // Save entire JSON
        SaveJSON.SaveMasterData(master);
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var about = new About();
        about.Show();
    }

    private void saveAsEncryptedToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Environment.SetEnvironmentVariable("encrypt", "1");
      saveAsEncryptedToolStripMenuItem.Checked = true;
      saveAsNormalToolStripMenuItem.Checked = false;
      MessageBox.Show("Saving as encrypted");
    }

    private void saveAsNormalToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Environment.SetEnvironmentVariable("encrypt", "0");
      saveAsEncryptedToolStripMenuItem.Checked = false;
      saveAsNormalToolStripMenuItem.Checked = true;
      MessageBox.Show("Saving as Normal");
    }

    public void chkEncrypt()
    {
      // Called externally if needed
      if (Environment.GetEnvironmentVariable("encrypt") == "1")
      {
        saveAsEncryptedToolStripMenuItem.Checked = true;
        saveAsNormalToolStripMenuItem.Checked = false;
      }
      else
      {
        saveAsEncryptedToolStripMenuItem.Checked = false;
        saveAsNormalToolStripMenuItem.Checked = true;
      }
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
  }
}
