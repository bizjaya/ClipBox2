// Decompiled with JetBrains decompiler
// Type: ClipBox2.Edit
// Assembly: ClipBox2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34961685-4A96-42BF-84C2-2E889F2EA09D
// Assembly location: D:\Sync\LOGX\CB2\ClipBox2.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ClipBox2
{
  public class Edit : Form
  {
    public string xmlFile = Environment.GetEnvironmentVariable("cbFol") + Environment.GetEnvironmentVariable("fName");
    private IContainer components;
    private TextBox tbcolname;
    private Button btnminus;
    private Button btnplus;
    private Button btnEdit;
    private Label lblColumns;
    private Label lblAdd;
    private ListBox lboColumns;
    private ComboBox cboList;
    private Button btn1;

    public Edit() => this.InitializeComponent();

    private void Edit_Load(object sender, EventArgs e)
    {
      foreach (string file in Directory.GetFiles(Environment.GetEnvironmentVariable("cbFol"), "*.xml"))
        this.cboList.Items.Add((object) Path.GetFileNameWithoutExtension(file));
      this.cboList.SelectedIndex = 0;
    }

    private void cboList_SelectedIndexChanged(object sender, EventArgs e)
    {
      int selectedIndex = this.cboList.SelectedIndex;
      string filename = this.cboList.Text + ".xml";
      if (!(filename != ""))
        return;
      Info data = SaveXML.GetData(filename);
      this.lboColumns.Items.Clear();
      foreach (object col in data.cols)
        this.lboColumns.Items.Add(col);
    }

    private void poplist()
    {
    }

    private void btnplus_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.tbcolname.Text))
        return;
      this.lboColumns.Items.Add((object) this.tbcolname.Text);
      this.tbcolname.Text = "";
    }

    private void btnminus_Click(object sender, EventArgs e)
    {
      if (this.lboColumns.SelectedIndices.Count < 0)
        return;
      this.lboColumns.Items.RemoveAt(this.lboColumns.SelectedIndices[0]);
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
      int selectedIndex = this.cboList.SelectedIndex;
      if (string.IsNullOrEmpty(this.cboList.Text))
        return;
      string filename = this.cboList.Text + ".xml";
      Info data = SaveXML.GetData(filename);
      data.cols.Clear();
      string[] strArray = new string[this.lboColumns.Items.Count];
      this.lboColumns.Items.CopyTo((object[]) strArray, 0);
      data.cols.AddRange((IEnumerable<string>) strArray);
      string str = Environment.GetEnvironmentVariable("cbFol") + filename;
      try
      {
        SaveXML.SaveData((object) data, str);
        int num = (int) MessageBox.Show("Saved");
        this.Close();
        if (Application.OpenForms["Form1"] == null)
          return;
        (Application.OpenForms["Form1"] as Form1).populate(str);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message);
      }
    }

    private void lboColumns_SelectedIndexChanged(object sender, EventArgs e) => this.tbcolname.Text = this.lboColumns.Text;

    private void btn1_Click(object sender, EventArgs e)
    {
      int selectedIndex = this.lboColumns.SelectedIndex;
      string text = this.tbcolname.Text;
      if (selectedIndex < 0)
        return;
      this.lboColumns.Items[selectedIndex] = (object) text;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Edit));
      this.tbcolname = new TextBox();
      this.btnminus = new Button();
      this.btnplus = new Button();
      this.btnEdit = new Button();
      this.lblColumns = new Label();
      this.lblAdd = new Label();
      this.lboColumns = new ListBox();
      this.cboList = new ComboBox();
      this.btn1 = new Button();
      this.SuspendLayout();
      this.tbcolname.Location = new Point(85, 38);
      this.tbcolname.Name = "tbcolname";
      this.tbcolname.Size = new Size(95, 20);
      this.tbcolname.TabIndex = 15;
      this.btnminus.Location = new Point(213, 37);
      this.btnminus.Name = "btnminus";
      this.btnminus.Size = new Size(19, 23);
      this.btnminus.TabIndex = 14;
      this.btnminus.Text = "-";
      this.btnminus.UseVisualStyleBackColor = true;
      this.btnminus.Click += new EventHandler(this.btnminus_Click);
      this.btnplus.Location = new Point(190, 37);
      this.btnplus.Name = "btnplus";
      this.btnplus.Size = new Size(19, 23);
      this.btnplus.TabIndex = 13;
      this.btnplus.Text = "+";
      this.btnplus.UseVisualStyleBackColor = true;
      this.btnplus.Click += new EventHandler(this.btnplus_Click);
      this.btnEdit.Location = new Point(184, 229);
      this.btnEdit.Name = "btnEdit";
      this.btnEdit.Size = new Size(75, 23);
      this.btnEdit.TabIndex = 12;
      this.btnEdit.Text = "Edit List";
      this.btnEdit.UseVisualStyleBackColor = true;
      this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
      this.lblColumns.AutoSize = true;
      this.lblColumns.Location = new Point(25, 41);
      this.lblColumns.Name = "lblColumns";
      this.lblColumns.Size = new Size(42, 13);
      this.lblColumns.TabIndex = 11;
      this.lblColumns.Text = "Column";
      this.lblAdd.AutoSize = true;
      this.lblAdd.Location = new Point(25, 16);
      this.lblAdd.Name = "lblAdd";
      this.lblAdd.Size = new Size(54, 13);
      this.lblAdd.TabIndex = 10;
      this.lblAdd.Text = "List Name";
      this.lboColumns.FormattingEnabled = true;
      this.lboColumns.Location = new Point(25, 73);
      this.lboColumns.Name = "lboColumns";
      this.lboColumns.Size = new Size(234, 147);
      this.lboColumns.TabIndex = 8;
      this.lboColumns.SelectedIndexChanged += new EventHandler(this.lboColumns_SelectedIndexChanged);
      this.cboList.FormattingEnabled = true;
      this.cboList.Location = new Point(85, 11);
      this.cboList.Name = "cboList";
      this.cboList.Size = new Size(174, 21);
      this.cboList.TabIndex = 16;
      this.cboList.SelectedIndexChanged += new EventHandler(this.cboList_SelectedIndexChanged);
      this.btn1.Image = (Image) componentResourceManager.GetObject("btn1.Image");
      this.btn1.Location = new Point(237, 37);
      this.btn1.Name = "btn1";
      this.btn1.Size = new Size(22, 23);
      this.btn1.TabIndex = 17;
      this.btn1.UseVisualStyleBackColor = true;
      this.btn1.Click += new EventHandler(this.btn1_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(284, 262);
      this.Controls.Add((Control) this.btn1);
      this.Controls.Add((Control) this.cboList);
      this.Controls.Add((Control) this.tbcolname);
      this.Controls.Add((Control) this.btnminus);
      this.Controls.Add((Control) this.btnplus);
      this.Controls.Add((Control) this.btnEdit);
      this.Controls.Add((Control) this.lblColumns);
      this.Controls.Add((Control) this.lblAdd);
      this.Controls.Add((Control) this.lboColumns);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (Edit);
      this.Text = nameof (Edit);
      this.TopMost = true;
      this.Load += new EventHandler(this.Edit_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
