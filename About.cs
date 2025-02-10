using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ClipBox2
{
  public partial class About : Form
  {
    public About()
    {
      InitializeComponent();
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
