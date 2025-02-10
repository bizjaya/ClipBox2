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
        public Add()
        {
            InitializeComponent();
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

        //private void btnAddList_Click(object sender, EventArgs e)
        //{
        //    // Create the new XML file for the list
        //    if (string.IsNullOrEmpty(tbListName.Text))
        //    {
        //        MessageBox.Show("Please enter a list name.");
        //        return;
        //    }

        //    string newListName = tbListName.Text.Trim();
        //    if (newListName.Length == 0)
        //    {
        //        MessageBox.Show("Invalid List Name.");
        //        return;
        //    }

        //    // Build the Info object
        //    Info data = new Info();
        //    data.cbmz = "cbmz";
        //    data.cbname = newListName;

        //    // Gather columns
        //    var columns = new string[lboColumns.Items.Count];
        //    lboColumns.Items.CopyTo(columns, 0);
        //    data.cols.AddRange(columns);

        //    // No initial rows here; user can add them later in Form1 (Edit mode).
        //    // data.strs remains empty.

        //    // Path to the new XML
        //    string xmlFilename = newListName + ".xml";
        //    string fullPath = Environment.GetEnvironmentVariable("cbFol") + xmlFilename;

        //    // Save and update Form1
        //    try
        //    {
        //        SaveXML.SaveData(data, fullPath);
        //        MessageBox.Show("List created successfully!", "Add List",
        //                        MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        // Close this Add form
        //        this.Close();

        //        // Refresh the main form’s combo + re‐populate
        //        if (Application.OpenForms["Form1"] != null)
        //        {
        //            var frm = (Form1)Application.OpenForms["Form1"];
        //            frm.popcombo();
        //            frm.populate(fullPath);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error creating list: " + ex.Message);
        //    }
        //}

        private void btnAddList_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbListName.Text))
            {
                MessageBox.Show("Please enter a list name.");
                return;
            }

            string newListName = tbListName.Text.Trim();
            if (newListName.Length == 0)
            {
                MessageBox.Show("Invalid List Name.");
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
                cbname = newListName
            };

            // Gather columns
            var columns = new string[lboColumns.Items.Count];
            lboColumns.Items.CopyTo(columns, 0);
            data.cols.AddRange(columns);

            // Insert it into our MasterData
            master.Lists[newListName] = data;

            // Save MasterData back to JSON
            try
            {
                SaveJSON.SaveMasterData(master);
                MessageBox.Show("List created successfully!", "Add List",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close this Add form
                this.Close();

                // Refresh the main form’s combo + re‐populate
                if (Application.OpenForms["Form1"] != null)
                {
                    var frm = (Form1)Application.OpenForms["Form1"];
                    frm.popcombo();
                    frm.populate(newListName); // pass the newly added name
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating list: " + ex.Message);
            }
        }

    }
}