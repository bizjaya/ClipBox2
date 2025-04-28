using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace ClipBox2
{
    public partial class Delete : MaterialSkin.Controls.MaterialForm
    {
        public Delete()
        {
            // MaterialSkin initialization
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue200, Accent.LightBlue200, TextShade.WHITE);
            InitializeComponent();
            this.Load += new EventHandler(Delete_Load);

            // Set form properties to ensure it appears on top
            this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // Example code inside a "Delete" button
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 'cboList' now exists thanks to the Designer code
            string listName = cboList.Text;

            MasterData master = SaveJSON.LoadMasterData();
            if (master.Lists.Remove(listName))
            {
                master.Save();
                MessageBox.Show($"Deleted list {listName}");
            }
        }

        private void Delete_Load(object sender, EventArgs e)
        {
            // e.g. load the MasterData and fill cboList
            MasterData master = SaveJSON.LoadMasterData();
            cboList.Items.Clear();
            foreach (string listName in master.Lists.Keys)
            {
                cboList.Items.Add(listName);
            }
            if (cboList.Items.Count > 0)
                cboList.SelectedIndex = 0;
        }

    }
}
