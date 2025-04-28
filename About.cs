using System;
using System.Diagnostics;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace ClipBox2
{
  public partial class About : MaterialSkin.Controls.MaterialForm
  {
    public About()
    {
      // MaterialSkin initialization
      var materialSkinManager = MaterialSkinManager.Instance;
      materialSkinManager.AddFormToManage(this);
      materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
      materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue200, Accent.LightBlue200, TextShade.WHITE);
      InitializeComponent();
      
      // Set form properties to ensure it appears on top
      this.TopMost = true;
      this.StartPosition = FormStartPosition.CenterScreen;
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      // Launch the website
      Process.Start("http://www.bizjaya.com");
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
      // Optional: handle picture box click here
    }
  }
}
