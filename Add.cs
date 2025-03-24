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

namespace ClipBox2
{
    public partial class Add : Form
    {
        private Form1 mainForm;

        public Add()
        {
            InitializeComponent();
            mainForm = Application.OpenForms["Form1"] as Form1;
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

        private void btnAddList_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbListName.Text))
            {
                MessageBox.Show("Please enter a list name.", "Add List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newListName = tbListName.Text.Trim();
            if (newListName.Length == 0)
            {
                MessageBox.Show("Invalid List Name.");
                return;
            }

            // Validate that we have at least one column
            if (lboColumns.Items.Count == 0)
            {
                MessageBox.Show("Please add at least one column to the list.", "Add List", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Load the entire MasterData from the JSON
            MasterData master = SaveJSON.LoadMasterData();

            if (master.Lists.ContainsKey(newListName))
            {
                MessageBox.Show("That list name already exists. Choose another.");
                return;
            }

            // Build a new Info object
            Info data = new Info
            {
                cbmz = "cbmz",
                cbname = newListName,
                cbdate = DateTime.Now
            };

            // Initialize cols list with column names
            foreach (var item in lboColumns.Items)
            {
                data.cols.Add(item.ToString());
            }

            // Add an empty row with empty strings
            var emptyRow = new List<string>();
            for (int i = 0; i < data.cols.Count; i++)
            {
                emptyRow.Add("");
            }
            data.strs.Add(emptyRow);

            // Insert it into our MasterData
            master.Lists[newListName] = data;

            // Save MasterData back to JSON
            try
            {
                // Add to master data and save
                SaveJSON.SaveMasterData(master);

                MessageBox.Show("List created successfully!", "Add List", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the main form
                if (mainForm != null)
                {
                    mainForm.popcombo(newListName);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating list: " + ex.Message);
            }
        }
    }
}