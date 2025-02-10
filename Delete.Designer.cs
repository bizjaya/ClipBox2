using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClipBox2
{
    partial class Delete
    {
        private IContainer components = null;
        private ComboBox cboList;
        private Button btnDelete;

        /// <summary>
        /// Clean up resources.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new Container();

            // Create & configure the ComboBox
            this.cboList = new ComboBox();
            this.cboList.Location = new Point(20, 20);
            this.cboList.Size = new Size(180, 21);
            this.cboList.Name = "cboList";
            // this.cboList.DropDownStyle = ComboBoxStyle.DropDownList;  // optional

            // Create & configure the "Delete" button
            this.btnDelete = new Button();
            this.btnDelete.Location = new Point(210, 20);
            this.btnDelete.Size = new Size(60, 23);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // 
            // Delete Form
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(284, 61);
            this.Controls.Add(this.cboList);
            this.Controls.Add(this.btnDelete);
            this.Name = "Delete";
            this.Text = "Delete";

            // Wire up the Load event so we can fill cboList with available lists
            this.Load += new System.EventHandler(this.Delete_Load);

            this.ResumeLayout(false);
        }

        #endregion
    }
}
