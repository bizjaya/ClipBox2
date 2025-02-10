using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClipBox2
{
    partial class Edit
    {
        private IContainer components = null;

        private TextBox tbcolname;
        private Button btnminus;
        private Button btnplus;
        private Button btnEdit;
        private Label lblColumns;
        private Label lblAdd;
        private ListBox lboColumns;
        private ComboBox cboList;
        private Button btn1;

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
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(Edit));
            this.tbcolname = new System.Windows.Forms.TextBox();
            this.btnminus = new System.Windows.Forms.Button();
            this.btnplus = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lblColumns = new System.Windows.Forms.Label();
            this.lblAdd = new System.Windows.Forms.Label();
            this.lboColumns = new System.Windows.Forms.ListBox();
            this.cboList = new System.Windows.Forms.ComboBox();
            this.btn1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbcolname
            // 
            this.tbcolname.Location = new System.Drawing.Point(85, 38);
            this.tbcolname.Name = "tbcolname";
            this.tbcolname.Size = new System.Drawing.Size(95, 20);
            this.tbcolname.TabIndex = 15;
            // 
            // btnminus
            // 
            this.btnminus.Location = new System.Drawing.Point(213, 37);
            this.btnminus.Name = "btnminus";
            this.btnminus.Size = new System.Drawing.Size(19, 23);
            this.btnminus.TabIndex = 14;
            this.btnminus.Text = "-";
            this.btnminus.UseVisualStyleBackColor = true;
            this.btnminus.Click += new System.EventHandler(this.btnminus_Click);
            // 
            // btnplus
            // 
            this.btnplus.Location = new System.Drawing.Point(190, 37);
            this.btnplus.Name = "btnplus";
            this.btnplus.Size = new System.Drawing.Size(19, 23);
            this.btnplus.TabIndex = 13;
            this.btnplus.Text = "+";
            this.btnplus.UseVisualStyleBackColor = true;
            this.btnplus.Click += new System.EventHandler(this.btnplus_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(184, 229);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 12;
            this.btnEdit.Text = "Edit List";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lblColumns
            // 
            this.lblColumns.AutoSize = true;
            this.lblColumns.Location = new System.Drawing.Point(25, 41);
            this.lblColumns.Name = "lblColumns";
            this.lblColumns.Size = new System.Drawing.Size(42, 13);
            this.lblColumns.TabIndex = 11;
            this.lblColumns.Text = "Column";
            // 
            // lblAdd
            // 
            this.lblAdd.AutoSize = true;
            this.lblAdd.Location = new System.Drawing.Point(25, 16);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(54, 13);
            this.lblAdd.TabIndex = 10;
            this.lblAdd.Text = "List Name";
            // 
            // lboColumns
            // 
            this.lboColumns.FormattingEnabled = true;
            this.lboColumns.Location = new System.Drawing.Point(25, 73);
            this.lboColumns.Name = "lboColumns";
            this.lboColumns.Size = new System.Drawing.Size(234, 147);
            this.lboColumns.TabIndex = 8;
            this.lboColumns.SelectedIndexChanged += new System.EventHandler(this.lboColumns_SelectedIndexChanged);
            // 
            // cboList
            // 
            this.cboList.FormattingEnabled = true;
            this.cboList.Location = new System.Drawing.Point(85, 11);
            this.cboList.Name = "cboList";
            this.cboList.Size = new System.Drawing.Size(174, 21);
            this.cboList.TabIndex = 16;
            this.cboList.SelectedIndexChanged += new System.EventHandler(this.cboList_SelectedIndexChanged);
            // 
            // btn1
            // 
            this.btn1.Image = ((System.Drawing.Image)(resources.GetObject("btn1.Image")));
            this.btn1.Location = new System.Drawing.Point(237, 37);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(22, 23);
            this.btn1.TabIndex = 17;
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.cboList);
            this.Controls.Add(this.tbcolname);
            this.Controls.Add(this.btnminus);
            this.Controls.Add(this.btnplus);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lblColumns);
            this.Controls.Add(this.lblAdd);
            this.Controls.Add(this.lboColumns);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Edit";
            this.Text = "Edit";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Edit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
