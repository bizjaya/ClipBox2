// Decompiled with JetBrains decompiler
// Type: ClipBox2.Form1
// Assembly: ClipBox2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34961685-4A96-42BF-84C2-2E889F2EA09D
// Assembly location: D:\Sync\LOGX\CB2\ClipBox2.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ClipBox2
{
  public class Form1 : Form
  {
    private IContainer components;
    private ComboBox cb1;
    private Label listlbl;
    private ComboBox cb2;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private ToolStripMenuItem addListToolStripMenuItem;
    private ToolStripMenuItem deleteListToolStripMenuItem;
    private ToolStripMenuItem editListToolStripMenuItem;
    private CheckBox chk1;
    private Button btn1;
    private DataGridView dgv1;
    private ToolStripMenuItem optionsToolStripMenuItem;
    private ToolStripMenuItem saveAsEncryptedToolStripMenuItem;
    private ToolStripMenuItem saveAsNormalToolStripMenuItem;
    private Button U;
    private Button d;

    public Form1()
    {
      Environment.SetEnvironmentVariable("cbFol", AppDomain.CurrentDomain.BaseDirectory);
      Environment.SetEnvironmentVariable("fName", "default.xml");
      Environment.SetEnvironmentVariable("encrypt", "1");
      this.InitializeComponent();
      this.cb2.Items.Add((object) "Copy");
      this.cb2.Items.Add((object) "Click");
      this.cb2.Items.Add((object) "Point");
      this.cb2.Text = this.cb2.Items[0].ToString();
      this.dgv1.AllowUserToAddRows = false;
      this.dgv1.AllowUserToDeleteRows = false;
      this.dgv1.AllowUserToOrderColumns = false;
      this.dgv1.ReadOnly = true;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      string str = Environment.GetEnvironmentVariable("cbFol") + Environment.GetEnvironmentVariable("fName");
      if (File.Exists(str))
      {
        this.popcombo();
        this.populate(str);
      }
      else
      {
        Info info = new Info();
        info.cbmz = "cbmz";
        info.cbname = "TestList";
        info.strs.Add(new List<string>()
        {
          "Text1",
          "Text2",
          "Text3"
        });
        info.strs.Add(new List<string>()
        {
          "Tezt1",
          "Tezt2",
          "Tezt3"
        });
        info.cols.AddRange((IEnumerable<string>) new string[3]
        {
          "Col1",
          "Col2",
          "Col3"
        });
        string[] array = info.strs.FirstOrDefault<List<string>>().ToArray();
        this.dgv1.ColumnCount = 3;
        this.dgv1.Columns[0].Name = "Col1";
        this.dgv1.Columns[1].Name = "Col2";
        this.dgv1.Columns[2].Name = "Col3";
        this.dgv1.Rows.Add((object[]) array);
        SaveXML.SaveData((object) info, str);
      }
    }

    private void cb1_SelectedIndexChanged(object sender, EventArgs e)
    {
      Environment.SetEnvironmentVariable("fName", this.cb1.Text + ".xml");
      this.populate(Environment.GetEnvironmentVariable("cbFol") + Environment.GetEnvironmentVariable("fName"));
    }

    private void lv1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    public void populate(string fn)
    {
      Info data = SaveXML.GetData(fn);
      this.dgv1.Rows.Clear();
      int num = data.cols.Count<string>();
      this.dgv1.ColumnCount = num;
      for (int index = 0; index < num; ++index)
        this.dgv1.Columns[index].Name = data.cols[index];
      foreach (List<string> str in data.strs)
        this.dgv1.Rows.Add((object[]) str.ToArray());
    }

    public void popcombo()
    {
      string environmentVariable1 = Environment.GetEnvironmentVariable("cbFol");
      string environmentVariable2 = Environment.GetEnvironmentVariable("fName");
      this.cb1.Items.Clear();
      foreach (string file in Directory.GetFiles(environmentVariable1, "*.xml"))
      {
        string withoutExtension1 = Path.GetFileNameWithoutExtension(file);
        string withoutExtension2 = Path.GetFileNameWithoutExtension(environmentVariable2);
        this.cb1.Items.Add((object) withoutExtension1);
        if (withoutExtension1 == withoutExtension2)
          this.cb1.Text = withoutExtension2;
      }
    }

    private void addListToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int num = (int) new Add().ShowDialog();
    }

    private void editListToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int num = (int) new Edit().ShowDialog();
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.chk1.Checked)
        return;
      int rowIndex = e.RowIndex;
      int columnIndex = e.ColumnIndex;
      if (rowIndex < 0)
        return;
      string str = this.dgv1.SelectedCells[0].Value.ToString();
      if (this.cb2.SelectedIndex == 0)
      {
        Clipboard.SetText(str);
      }
      else
      {
        if (this.cb2.SelectedIndex != 2)
          return;
        this.TopMost = false;
        IntPtr hWnd = Form1.GetWindow(Process.GetCurrentProcess().MainWindowHandle, 2U);
        while (true)
        {
          IntPtr parent = Form1.GetParent(hWnd);
          if (!parent.Equals((object) IntPtr.Zero))
            hWnd = parent;
          else
            break;
        }
        Form1.SetForegroundWindow(hWnd);
        SendKeys.SendWait(str);
        this.TopMost = true;
      }
    }

    private void dgv1_LostFocus(object sender, EventArgs e)
    {
      try
      {
        string keys = this.dgv1.SelectedCells[0].Value.ToString();
        if (this.cb2.SelectedIndex != 1)
          return;
        Thread.Sleep(200);
        SendKeys.SendWait(keys);
      }
      catch (Exception ex)
      {
      }
    }

    private void chk1_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chk1.Checked)
      {
        this.dgv1.ReadOnly = false;
        this.dgv1.AllowUserToAddRows = true;
        this.dgv1.AllowUserToDeleteRows = true;
        this.dgv1.AllowUserToOrderColumns = true;
        this.dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      }
      else
      {
        this.Save();
        this.dgv1.ReadOnly = true;
        this.dgv1.AllowUserToAddRows = false;
        this.dgv1.AllowUserToDeleteRows = false;
        this.dgv1.AllowUserToOrderColumns = false;
        this.dgv1.SelectionMode = DataGridViewSelectionMode.CellSelect;
      }
    }

    private void btn1_Click(object sender, EventArgs e)
    {
      this.Save();
      int num = (int) MessageBox.Show("Saved!");
    }

    private void Save()
    {
      Info info = new Info();
      info.cbmz = "cbmz";
      string str = this.cb1.Text + ".xml";
      info.cbname = this.cb1.Text;
      foreach (DataGridViewColumn column in (BaseCollection) this.dgv1.Columns)
        info.cols.Add(column.Name);
      foreach (DataGridViewRow row in (IEnumerable) this.dgv1.Rows)
      {
        int count = row.Cells.Count;
        string[] collection = new string[row.Cells.Count];
        int index = 0;
        foreach (DataGridViewCell cell in (BaseCollection) row.Cells)
        {
          if (cell.Value == null)
            cell.Value = (object) "";
          collection[index] = cell.Value.ToString();
          ++index;
        }
        List<string> stringList = new List<string>();
        stringList.AddRange((IEnumerable<string>) collection);
        info.strs.Add(stringList);
      }
      string filename = Environment.GetEnvironmentVariable("cbFol") + str;
      SaveXML.SaveData((object) info, filename);
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e) => new About().Show();

    private void saveAsEncryptedToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Environment.SetEnvironmentVariable("encrypt", "1");
      this.saveAsEncryptedToolStripMenuItem.Checked = true;
      this.saveAsNormalToolStripMenuItem.Checked = false;
      int num = (int) MessageBox.Show("Saving as encrypted");
    }

    private void saveAsNormalToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Environment.SetEnvironmentVariable("encrypt", "0");
      this.saveAsEncryptedToolStripMenuItem.Checked = false;
      this.saveAsNormalToolStripMenuItem.Checked = true;
      int num = (int) MessageBox.Show("Saving as Normal");
    }

    public void chkEncrypt()
    {
      if (Environment.GetEnvironmentVariable("encrypt") == "1")
      {
        this.saveAsEncryptedToolStripMenuItem.Checked = true;
        this.saveAsNormalToolStripMenuItem.Checked = false;
      }
      else
      {
        this.saveAsEncryptedToolStripMenuItem.Checked = false;
        this.saveAsNormalToolStripMenuItem.Checked = true;
      }
    }

    private void u_Click(object sender, EventArgs e)
    {
      DataGridView dgv1 = this.dgv1;
      try
      {
        int count = dgv1.Rows.Count;
        int index1 = dgv1.SelectedCells[0].OwningRow.Index;
        if (index1 == 0)
          return;
        int index2 = dgv1.SelectedCells[0].OwningColumn.Index;
        DataGridViewRowCollection rows = dgv1.Rows;
        DataGridViewRow dataGridViewRow = rows[index1];
        rows.Remove(dataGridViewRow);
        rows.Insert(index1 - 1, dataGridViewRow);
        dgv1.ClearSelection();
        dgv1.Rows[index1 - 1].Cells[index2].Selected = true;
      }
      catch
      {
      }
    }

    private void d_Click(object sender, EventArgs e)
    {
      DataGridView dgv1 = this.dgv1;
      try
      {
        int count = dgv1.Rows.Count;
        int index1 = dgv1.SelectedCells[0].OwningRow.Index;
        if (index1 == count - 2)
          return;
        int index2 = dgv1.SelectedCells[0].OwningColumn.Index;
        DataGridViewRowCollection rows = dgv1.Rows;
        DataGridViewRow dataGridViewRow = rows[index1];
        rows.Remove(dataGridViewRow);
        rows.Insert(index1 + 1, dataGridViewRow);
        dgv1.ClearSelection();
        dgv1.Rows[index1 + 1].Cells[index2].Selected = true;
      }
      catch
      {
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      DataGridViewCellStyle gridViewCellStyle = new DataGridViewCellStyle();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Form1));
      this.cb1 = new ComboBox();
      this.listlbl = new Label();
      this.cb2 = new ComboBox();
      this.menuStrip1 = new MenuStrip();
      this.fileToolStripMenuItem = new ToolStripMenuItem();
      this.addListToolStripMenuItem = new ToolStripMenuItem();
      this.editListToolStripMenuItem = new ToolStripMenuItem();
      this.deleteListToolStripMenuItem = new ToolStripMenuItem();
      this.optionsToolStripMenuItem = new ToolStripMenuItem();
      this.saveAsEncryptedToolStripMenuItem = new ToolStripMenuItem();
      this.saveAsNormalToolStripMenuItem = new ToolStripMenuItem();
      this.aboutToolStripMenuItem = new ToolStripMenuItem();
      this.dgv1 = new DataGridView();
      this.d = new Button();
      this.U = new Button();
      this.btn1 = new Button();
      this.chk1 = new CheckBox();
      this.menuStrip1.SuspendLayout();
      ((ISupportInitialize) this.dgv1).BeginInit();
      this.SuspendLayout();
      this.cb1.FormattingEnabled = true;
      this.cb1.Location = new Point(26, 21);
      this.cb1.Name = "cb1";
      this.cb1.Size = new Size(134, 21);
      this.cb1.TabIndex = 1;
      this.cb1.SelectedIndexChanged += new EventHandler(this.cb1_SelectedIndexChanged);
      this.listlbl.AutoSize = true;
      this.listlbl.Location = new Point(3, 25);
      this.listlbl.Name = "listlbl";
      this.listlbl.Size = new Size(23, 13);
      this.listlbl.TabIndex = 2;
      this.listlbl.Text = "List";
      this.cb2.FormattingEnabled = true;
      this.cb2.Location = new Point(164, 21);
      this.cb2.Name = "cb2";
      this.cb2.Size = new Size(49, 21);
      this.cb2.TabIndex = 3;
      this.menuStrip1.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.fileToolStripMenuItem,
        (ToolStripItem) this.optionsToolStripMenuItem,
        (ToolStripItem) this.aboutToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(286, 24);
      this.menuStrip1.TabIndex = 4;
      this.menuStrip1.Text = "menuStrip1";
      this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.addListToolStripMenuItem,
        (ToolStripItem) this.editListToolStripMenuItem,
        (ToolStripItem) this.deleteListToolStripMenuItem
      });
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      this.addListToolStripMenuItem.Name = "addListToolStripMenuItem";
      this.addListToolStripMenuItem.Size = new Size(128, 22);
      this.addListToolStripMenuItem.Text = "Add List";
      this.addListToolStripMenuItem.Click += new EventHandler(this.addListToolStripMenuItem_Click);
      this.editListToolStripMenuItem.Name = "editListToolStripMenuItem";
      this.editListToolStripMenuItem.Size = new Size(128, 22);
      this.editListToolStripMenuItem.Text = "Edit List";
      this.editListToolStripMenuItem.Click += new EventHandler(this.editListToolStripMenuItem_Click);
      this.deleteListToolStripMenuItem.Name = "deleteListToolStripMenuItem";
      this.deleteListToolStripMenuItem.Size = new Size(128, 22);
      this.deleteListToolStripMenuItem.Text = "Delete List";
      this.optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.saveAsEncryptedToolStripMenuItem,
        (ToolStripItem) this.saveAsNormalToolStripMenuItem
      });
      this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
      this.optionsToolStripMenuItem.Size = new Size(61, 20);
      this.optionsToolStripMenuItem.Text = "Options";
      this.saveAsEncryptedToolStripMenuItem.Checked = true;
      this.saveAsEncryptedToolStripMenuItem.CheckState = CheckState.Checked;
      this.saveAsEncryptedToolStripMenuItem.Name = "saveAsEncryptedToolStripMenuItem";
      this.saveAsEncryptedToolStripMenuItem.Size = new Size(168, 22);
      this.saveAsEncryptedToolStripMenuItem.Text = "Save as Encrypted";
      this.saveAsEncryptedToolStripMenuItem.Click += new EventHandler(this.saveAsEncryptedToolStripMenuItem_Click);
      this.saveAsNormalToolStripMenuItem.Name = "saveAsNormalToolStripMenuItem";
      this.saveAsNormalToolStripMenuItem.Size = new Size(168, 22);
      this.saveAsNormalToolStripMenuItem.Text = "Save as Normal";
      this.saveAsNormalToolStripMenuItem.Click += new EventHandler(this.saveAsNormalToolStripMenuItem_Click);
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new Size(52, 20);
      this.aboutToolStripMenuItem.Text = "About";
      this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);
      this.dgv1.AllowUserToAddRows = false;
      this.dgv1.AllowUserToDeleteRows = false;
      this.dgv1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      this.dgv1.BackgroundColor = SystemColors.ButtonHighlight;
      this.dgv1.BorderStyle = BorderStyle.None;
      this.dgv1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
      this.dgv1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      gridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle.BackColor = SystemColors.Window;
      gridViewCellStyle.Font = new Font("Arial", 8.2f, FontStyle.Regular, GraphicsUnit.Pixel);
      gridViewCellStyle.ForeColor = SystemColors.ControlText;
      gridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle.WrapMode = DataGridViewTriState.False;
      this.dgv1.DefaultCellStyle = gridViewCellStyle;
      this.dgv1.EditMode = DataGridViewEditMode.EditOnKeystroke;
      this.dgv1.GridColor = SystemColors.ButtonFace;
      this.dgv1.Location = new Point(-7, 48);
      this.dgv1.MultiSelect = false;
      this.dgv1.Name = "dgv1";
      this.dgv1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv1.SelectionMode = DataGridViewSelectionMode.CellSelect;
      this.dgv1.ShowCellErrors = false;
      this.dgv1.Size = new Size(286, 407);
      this.dgv1.StandardTab = true;
      this.dgv1.TabIndex = 0;
      this.dgv1.TabStop = false;
      this.dgv1.CellContentClick += new DataGridViewCellEventHandler(this.dgv1_CellClick);
      this.dgv1.LostFocus += new EventHandler(this.dgv1_LostFocus);
      this.d.Image = (Image) componentResourceManager.GetObject("d.Image");
      this.d.ImageAlign = ContentAlignment.BottomCenter;
      this.d.Location = new Point(258, 27);
      this.d.Name = "d";
      this.d.Size = new Size(18, 16);
      this.d.TabIndex = 9;
      this.d.TextAlign = ContentAlignment.TopRight;
      this.d.UseVisualStyleBackColor = true;
      this.d.Click += new EventHandler(this.d_Click);
      this.U.Image = (Image) componentResourceManager.GetObject("U.Image");
      this.U.ImageAlign = ContentAlignment.TopCenter;
      this.U.Location = new Point(258, 12);
      this.U.Name = "U";
      this.U.Size = new Size(18, 16);
      this.U.TabIndex = 8;
      this.U.UseVisualStyleBackColor = true;
      this.U.Click += new EventHandler(this.u_Click);
      this.btn1.Image = (Image) componentResourceManager.GetObject("btn1.Image");
      this.btn1.Location = new Point(237, 20);
      this.btn1.Name = "btn1";
      this.btn1.Size = new Size(22, 23);
      this.btn1.TabIndex = 7;
      this.btn1.UseVisualStyleBackColor = true;
      this.btn1.Click += new EventHandler(this.btn1_Click);
      this.chk1.Appearance = Appearance.Button;
      this.chk1.AutoSize = true;
      this.chk1.BackColor = SystemColors.ButtonFace;
      this.chk1.BackgroundImageLayout = ImageLayout.Center;
      this.chk1.ForeColor = SystemColors.Desktop;
      this.chk1.Image = (Image) componentResourceManager.GetObject("chk1.Image");
      this.chk1.Location = new Point(215, 20);
      this.chk1.Name = "chk1";
      this.chk1.Size = new Size(22, 22);
      this.chk1.TabIndex = 6;
      this.chk1.UseVisualStyleBackColor = false;
      this.chk1.CheckedChanged += new EventHandler(this.chk1_CheckedChanged);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(286, 456);
      this.Controls.Add((Control) this.d);
      this.Controls.Add((Control) this.U);
      this.Controls.Add((Control) this.btn1);
      this.Controls.Add((Control) this.chk1);
      this.Controls.Add((Control) this.dgv1);
      this.Controls.Add((Control) this.cb2);
      this.Controls.Add((Control) this.listlbl);
      this.Controls.Add((Control) this.cb1);
      this.Controls.Add((Control) this.menuStrip1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (Form1);
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.Text = "ClipBox V2.4";
      this.TopMost = true;
      this.Load += new EventHandler(this.Form1_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((ISupportInitialize) this.dgv1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

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
