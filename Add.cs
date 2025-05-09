using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace ClipBox2
{
    public partial class Add : MaterialSkin.Controls.MaterialForm
    {
        private Form1 mainForm;

        public Add()
        {
            // MaterialSkin initialization
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue200, Accent.LightBlue200, TextShade.WHITE);
            InitializeComponent();
            mainForm = Application.OpenForms["Form1"] as Form1;
            
            // Set form properties to ensure it appears on top
            this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // Populate the font size combo box from App.FontSizes
            foreach (var size in App.FontSizes)
            {
                fontSizeComboBox.Items.Add(size.Value);
            }
            
            // Set default font size to 9
            SelectFontSizeInComboBox(9);
            fontSizeComboBox.SelectedIndexChanged += fontSizeComboBox_SelectedIndexChanged;
        }

        private void Add_Load(object sender, EventArgs e)
        {
            // Any additional setup on form load can go here.
        }

        private void btnplus_Click(object sender, EventArgs e)
        {
            // Add a column name to the list
            if (string.IsNullOrEmpty(tbColumn.Text)) return;
            lboColumns.Items.Add(tbColumn.Text);
            tbColumn.Text = "";
        }

        private void btnminus_Click(object sender, EventArgs e)
        {
            // Remove the selected column from the list
            if (lboColumns.SelectedIndex < 0) return;
            lboColumns.Items.RemoveAt(lboColumns.SelectedIndex);
        }
        
        private void btnColumnLeft_Click(object sender, EventArgs e)
        {
            // Move the selected column left in the list
            if (lboColumns.SelectedIndex <= 0) return;
            
            int selectedIndex = lboColumns.SelectedIndex;
            string selectedItem = lboColumns.SelectedItem.ToString();
            
            lboColumns.Items.RemoveAt(selectedIndex);
            lboColumns.Items.Insert(selectedIndex - 1, selectedItem);
            lboColumns.SelectedIndex = selectedIndex - 1;
        }
        
        private void btnColumnRight_Click(object sender, EventArgs e)
        {
            // Move the selected column right in the list
            if (lboColumns.SelectedIndex < 0 || lboColumns.SelectedIndex >= lboColumns.Items.Count - 1) return;
            
            int selectedIndex = lboColumns.SelectedIndex;
            string selectedItem = lboColumns.SelectedItem.ToString();
            
            lboColumns.Items.RemoveAt(selectedIndex);
            lboColumns.Items.Insert(selectedIndex + 1, selectedItem);
            lboColumns.SelectedIndex = selectedIndex + 1;
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

        private void btnAddList_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbListName.Text))
            {
                MessageBox.Show("Please enter a list name.", "Add List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string keyName = tbListName.Text;
            
            // Create a new list with the columns
            MasterData master = SaveJSON.LoadMasterData();
            
            // Check if the list name already exists
            if (master.Lists.ContainsKey(keyName))
            {
                MessageBox.Show($"A list with the name '{keyName}' already exists.", "Add List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Create a new Info object for the list
            Info info = new Info
            {
                cols = new List<string>(),
                strs = new List<List<string>>(),
                // Legacy pswd property is no longer used, but kept for backward compatibility
                pswd = false,
                size = GetSelectedFontSize()
            };
            
            // Initialize column-specific settings lists
            info.colIsPassword = new List<bool>();
            info.colIsMultiLine = new List<bool>();
            
            // Add the columns
            foreach (var item in lboColumns.Items)
            {
                info.cols.Add(item.ToString());
                
                // If this is the first column and the password checkbox is checked,
                // mark it as a password column (migration from legacy behavior)
                if (info.cols.Count == 1 && chkPswd.Checked)
                {
                    info.colIsPassword.Add(true);
                }
                else
                {
                    info.colIsPassword.Add(false);
                }
                
                // Default all columns to non-multiline
                info.colIsMultiLine.Add(false);
            }
            
            // Add the list to the master data
            master.Lists[keyName] = info;
            master.Save();
            
            // Refresh the main form's combo box
            //if (mainForm != null)
            //{
            //    mainForm.populateDGV1(keyName);
            //}
            
            MessageBox.Show($"List '{keyName}' added successfully.", "Add List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}