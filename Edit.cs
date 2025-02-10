using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ClipBox2
{
  public partial class Edit : Form
  {
    // For reference if needed
    public string xmlFile = Environment.GetEnvironmentVariable("cbFol")
                          + Environment.GetEnvironmentVariable("fName");

    public Edit()
    {
      InitializeComponent();
    }



    //private void Edit_Load(object sender, EventArgs e)
    //{
    //  // Populate comboBox with existing .xml files
    //  string folder = Environment.GetEnvironmentVariable("cbFol");
    //  foreach (string file in Directory.GetFiles(folder, "*.xml"))
    //  {
    //    cboList.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
    //  }

    //  // If we can safely select index 0
    //  if (cboList.Items.Count > 0)
    //    cboList.SelectedIndex = 0;
    //}

    //private void cboList_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //  string filename = cboList.Text + ".xml";
    //  if (string.IsNullOrEmpty(filename)) return;

    //  Info data = SaveXML.GetData(filename);
    //  lboColumns.Items.Clear();

    //  foreach (var col in data.cols)
    //  {
    //    lboColumns.Items.Add(col);
    //  }
    //}



    private void btnplus_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(tbcolname.Text)) return;

      lboColumns.Items.Add(tbcolname.Text);
      tbcolname.Text = "";
    }

    private void btnminus_Click(object sender, EventArgs e)
    {
      if (lboColumns.SelectedIndex < 0) return;

      lboColumns.Items.RemoveAt(lboColumns.SelectedIndex);
    }

    //private void btnEdit_Click(object sender, EventArgs e)
    //{
    //  // Overwrite the .xml columns with what's in lboColumns
    //  if (string.IsNullOrEmpty(cboList.Text)) return;

    //  string filename = cboList.Text + ".xml";
    //  Info data = SaveXML.GetData(filename);

    //  data.cols.Clear();
    //  string[] colArray = new string[lboColumns.Items.Count];
    //  lboColumns.Items.CopyTo(colArray, 0);
    //  data.cols.AddRange(colArray);

    //  string fullPath = Environment.GetEnvironmentVariable("cbFol") + filename;

    //  try
    //  {
    //    SaveXML.SaveData(data, fullPath);
    //    MessageBox.Show("Saved");
    //    this.Close();

    //    // If Form1 is open, refresh its grid
    //    if (Application.OpenForms["Form1"] is Form1 frm)
    //    {
    //      frm.populate(fullPath);
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    MessageBox.Show(ex.Message);
    //  }
    //}



    private void lboColumns_SelectedIndexChanged(object sender, EventArgs e)
    {
      tbcolname.Text = lboColumns.Text;
    }

    private void btn1_Click(object sender, EventArgs e)
    {
      // Edit the selected column name in the listbox
      int index = lboColumns.SelectedIndex;
      if (index < 0) return;

      lboColumns.Items[index] = tbcolname.Text;
    }



    private void Edit_Load(object sender, EventArgs e)
    {
        // Instead of scanning .xml files, we read the keys from MasterData
        MasterData master = SaveJSON.LoadMasterData();
        foreach (var listName in master.Lists.Keys)
        {
            cboList.Items.Add(listName);
        }
        if (cboList.Items.Count > 0)
            cboList.SelectedIndex = 0;
    }

    private void cboList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboList.Text)) return;
        string listName = cboList.Text;

        MasterData master = SaveJSON.LoadMasterData();
        if (!master.Lists.ContainsKey(listName)) return;

        Info data = master.Lists[listName];
        lboColumns.Items.Clear();
        foreach (var col in data.cols)
        {
            lboColumns.Items.Add(col);
        }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        string listName = cboList.Text;
        if (string.IsNullOrEmpty(listName)) return;

        MasterData master = SaveJSON.LoadMasterData();
        if (!master.Lists.ContainsKey(listName)) return;

        Info data = master.Lists[listName];

        data.cols.Clear();
        string[] colArray = new string[lboColumns.Items.Count];
        lboColumns.Items.CopyTo(colArray, 0);
        data.cols.AddRange(colArray);

        try
        {
            SaveJSON.SaveMasterData(master);
            MessageBox.Show("Saved");
            this.Close();

            // If Form1 is open, refresh
            if (Application.OpenForms["Form1"] is Form1 frm)
            {
                frm.populate(listName);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }




    }
}
