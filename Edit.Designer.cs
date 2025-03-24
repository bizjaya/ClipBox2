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
        private Button btnColumnLeft;
        private Button btnColumnRight;
        private CheckBox chkPswd;
        private ComboBox fontSizeComboBox;

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
            this.btnColumnLeft = new System.Windows.Forms.Button();
            this.btnColumnRight = new System.Windows.Forms.Button();
            this.chkPswd = new System.Windows.Forms.CheckBox();
            this.fontSizeComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tbcolname
            // 
            this.tbcolname.Location = new System.Drawing.Point(85, 38);
            this.tbcolname.Name = "tbcolname";
            this.tbcolname.Size = new System.Drawing.Size(65, 20);
            this.tbcolname.TabIndex = 11;
            this.tbcolname.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            // 
            // btnminus
            // 
            this.btnminus.Location = new System.Drawing.Point(175, 37);
            this.btnminus.Name = "btnminus";
            this.btnminus.Size = new System.Drawing.Size(19, 23);
            this.btnminus.TabIndex = 14;
            this.btnminus.Text = "-";
            this.btnminus.UseVisualStyleBackColor = true;
            this.btnminus.Click += new System.EventHandler(this.btnminus_Click);
            this.btnminus.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            // 
            // btnColumnLeft
            // 
            this.btnColumnLeft.Location = new System.Drawing.Point(195, 37);
            this.btnColumnLeft.Name = "btnColumnLeft";
            this.btnColumnLeft.Size = new System.Drawing.Size(19, 23);
            this.btnColumnLeft.TabIndex = 20;
            this.btnColumnLeft.Text = "←";
            this.btnColumnLeft.UseVisualStyleBackColor = true;
            this.btnColumnLeft.Click += new System.EventHandler(this.btnColumnLeft_Click);
            this.btnColumnLeft.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            // 
            // btnColumnRight
            // 
            this.btnColumnRight.Location = new System.Drawing.Point(215, 37);
            this.btnColumnRight.Name = "btnColumnRight";
            this.btnColumnRight.Size = new System.Drawing.Size(19, 23);
            this.btnColumnRight.TabIndex = 21;
            this.btnColumnRight.Text = "→";
            this.btnColumnRight.UseVisualStyleBackColor = true;
            this.btnColumnRight.Click += new System.EventHandler(this.btnColumnRight_Click);
            this.btnColumnRight.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            // 
            // chkPswd
            // 
            this.chkPswd.AutoSize = true;
            this.chkPswd.Location = new System.Drawing.Point(242, 39);
            this.chkPswd.Name = "chkPswd";
            this.chkPswd.Size = new System.Drawing.Size(53, 17);
            this.chkPswd.TabIndex = 22;
            this.chkPswd.Text = "Pswd";
            this.chkPswd.UseVisualStyleBackColor = true;
            this.chkPswd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // 
            // fontSizeComboBox
            // 
            this.fontSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fontSizeComboBox.FormattingEnabled = true;
            this.fontSizeComboBox.Location = new System.Drawing.Point(230, 11);
            this.fontSizeComboBox.Name = "fontSizeComboBox";
            this.fontSizeComboBox.Size = new System.Drawing.Size(65, 21);
            this.fontSizeComboBox.TabIndex = 23;
            this.fontSizeComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // 
            // btnplus
            // 
            this.btnplus.Location = new System.Drawing.Point(155, 37);
            this.btnplus.Name = "btnplus";
            this.btnplus.Size = new System.Drawing.Size(19, 23);
            this.btnplus.TabIndex = 13;
            this.btnplus.Text = "+";
            this.btnplus.UseVisualStyleBackColor = true;
            this.btnplus.Click += new System.EventHandler(this.btnplus_Click);
            this.btnplus.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(184, 235);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 12;
            this.btnEdit.Text = "Edit List";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            this.btnEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
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
            this.lboColumns.Location = new System.Drawing.Point(25, 91);
            this.lboColumns.Name = "lboColumns";
            this.lboColumns.Size = new System.Drawing.Size(270, 134);
            this.lboColumns.TabIndex = 8;
            this.lboColumns.SelectedIndexChanged += new System.EventHandler(this.lboColumns_SelectedIndexChanged);
            this.lboColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // cboList
            // 
            this.cboList.FormattingEnabled = true;
            this.cboList.Location = new System.Drawing.Point(85, 11);
            this.cboList.Name = "cboList";
            this.cboList.Size = new System.Drawing.Size(130, 21);
            this.cboList.TabIndex = 16;
            this.cboList.SelectedIndexChanged += new System.EventHandler(this.cboList_SelectedIndexChanged);
            this.cboList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            // 
            // Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 270);
            this.Controls.Add(this.fontSizeComboBox);
            this.Controls.Add(this.chkPswd);
            this.Controls.Add(this.btnColumnRight);
            this.Controls.Add(this.btnColumnLeft);
            this.Controls.Add(this.cboList);
            this.Controls.Add(this.tbcolname);
            this.Controls.Add(this.btnminus);
            this.Controls.Add(this.btnplus);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lblColumns);
            this.Controls.Add(this.lblAdd);
            this.Controls.Add(this.lboColumns);
            this.Name = "Edit";
            this.Text = "Edit";
            this.Padding = new System.Windows.Forms.Padding(25);
            this.Load += new System.EventHandler(this.Edit_Load);
            this.MinimumSize = new System.Drawing.Size(320, 280);
            this.MaximumSize = new System.Drawing.Size(500, 500);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
