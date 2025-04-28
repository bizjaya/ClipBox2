using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace ClipBox2
{
  public partial class Edit : MaterialSkin.Controls.MaterialForm
  {
    private MasterData master;
    private Info data;
    private string listName;

    // For reference if needed
    public string xmlFile = Environment.GetEnvironmentVariable("cbFol")
                          + Environment.GetEnvironmentVariable("fName");

    public Edit(MasterData master, string listName)
    {
      // MaterialSkin initialization
      var materialSkinManager = MaterialSkinManager.Instance;
      materialSkinManager.AddFormToManage(this);
      materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
      materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue200, Accent.LightBlue200, TextShade.WHITE);
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

    private void btnColumnUp_Click(object sender, EventArgs e)
    {
        int selectedIndex = lboColumns.SelectedIndex;
        if (selectedIndex <= 0) return; // Can't move up if it's the first item or nothing is selected

        // Get the item to move
        object item = lboColumns.Items[selectedIndex];
        
        // Remove it from the current position
        lboColumns.Items.RemoveAt(selectedIndex);
        
        // Insert it at the new position (one up)
        lboColumns.Items.Insert(selectedIndex - 1, item);
        
        // Keep the item selected
        lboColumns.SelectedIndex = selectedIndex - 1;
    }

    private void btnColumnDown_Click(object sender, EventArgs e)
    {
        int selectedIndex = lboColumns.SelectedIndex;
        if (selectedIndex < 0 || selectedIndex >= lboColumns.Items.Count - 1) return; // Can't move down if it's the last item or nothing is selected

        // Get the item to move
        object item = lboColumns.Items[selectedIndex];
        
        // Remove it from the current position
        lboColumns.Items.RemoveAt(selectedIndex);
        
        // Insert it at the new position (one down)
        lboColumns.Items.Insert(selectedIndex + 1, item);
        
        // Keep the item selected
        lboColumns.SelectedIndex = selectedIndex + 1;
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

    private void fontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Update the font size for the list box to preview the change
        if (fontSizeComboBox.SelectedIndex >= 0)
        {
            int size = GetSelectedFontSize();
            if (size > 0)
            {
                lboColumns.Font = new Font(lboColumns.Font.FontFamily, size);
            }
        }
    }

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
  }
}
