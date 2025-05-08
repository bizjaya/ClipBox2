using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClipBox2
{
    partial class Add
    {
        private IContainer components = null;
        private TextBox tbListName;
        private TextBox tbColumn;
        private ListBox lboColumns;
        private Button btnAddList;
        private Button btnplus;
        private Button btnminus;
        private Label lblListName;
        private Label lblColumn;
        private Button btnColumnLeft;
        private Button btnColumnRight;
        private CheckBox chkPswd;
        private ComboBox fontSizeComboBox;

        private void InitializeComponent()
        {
            this.tbListName = new TextBox();
            this.tbColumn = new TextBox();
            this.lboColumns = new ListBox();
            this.btnAddList = new Button();
            this.btnplus = new Button();
            this.btnminus = new Button();
            this.lblListName = new Label();
            this.lblColumn = new Label();
            this.btnColumnLeft = new Button();
            this.btnColumnRight = new Button();
            this.chkPswd = new CheckBox();
            this.fontSizeComboBox = new ComboBox();
            this.SuspendLayout();

            // lblListName
            this.lblListName.AutoSize = true;
            this.lblListName.Location = new Point(25, 16);
            this.lblListName.Name = "lblListName";
            this.lblListName.Size = new Size(54, 13);
            this.lblListName.Text = "List Name";

            // lblColumn
            this.lblColumn.AutoSize = true;
            this.lblColumn.Location = new Point(25, 41);
            this.lblColumn.Name = "lblColumn";
            this.lblColumn.Size = new Size(42, 13);
            this.lblColumn.Text = "Column";

            // tbListName
            this.tbListName.Location = new Point(85, 11);
            this.tbListName.Name = "tbListName";
            this.tbListName.Size = new Size(130, 20);
            this.tbListName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;



            // tbColumn
            this.tbColumn.Location = new Point(85, 38);
            this.tbColumn.Name = "tbColumn";
            this.tbColumn.Size = new Size(65, 20);
            this.tbColumn.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // btnplus
            this.btnplus.Location = new Point(155, 37);
            this.btnplus.Name = "btnplus";
            this.btnplus.Size = new Size(19, 23);
            this.btnplus.TabIndex = 13;
            this.btnplus.Text = "+";
            this.btnplus.UseVisualStyleBackColor = true;
            this.btnplus.Click += new EventHandler(this.btnplus_Click);
            this.btnplus.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // btnminus
            this.btnminus.Location = new Point(175, 37);
            this.btnminus.Name = "btnminus";
            this.btnminus.Size = new Size(19, 23);
            this.btnminus.TabIndex = 14;
            this.btnminus.Text = "-";
            this.btnminus.UseVisualStyleBackColor = true;
            this.btnminus.Click += new EventHandler(this.btnminus_Click);
            this.btnminus.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // btnColumnLeft
            this.btnColumnLeft.Location = new Point(195, 37);
            this.btnColumnLeft.Name = "btnColumnLeft";
            this.btnColumnLeft.Size = new Size(19, 23);
            this.btnColumnLeft.TabIndex = 20;
            this.btnColumnLeft.Text = "←";
            this.btnColumnLeft.UseVisualStyleBackColor = true;
            this.btnColumnLeft.Click += new EventHandler(this.btnColumnLeft_Click);
            this.btnColumnLeft.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // btnColumnRight
            this.btnColumnRight.Location = new Point(215, 37);
            this.btnColumnRight.Name = "btnColumnRight";
            this.btnColumnRight.Size = new Size(19, 23);
            this.btnColumnRight.TabIndex = 21;
            this.btnColumnRight.Text = "→";
            this.btnColumnRight.UseVisualStyleBackColor = true;
            this.btnColumnRight.Click += new EventHandler(this.btnColumnRight_Click);
            this.btnColumnRight.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // chkPswd
            this.chkPswd.AutoSize = true;
            this.chkPswd.Location = new Point(242, 39);
            this.chkPswd.Name = "chkPswd";
            this.chkPswd.Size = new Size(53, 17);
            this.chkPswd.TabIndex = 22;
            this.chkPswd.Text = "Pswd";
            this.chkPswd.UseVisualStyleBackColor = true;
            this.chkPswd.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // fontSizeComboBox
            this.fontSizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.fontSizeComboBox.FormattingEnabled = true;
            this.fontSizeComboBox.Location = new Point(230, 11);
            this.fontSizeComboBox.Name = "fontSizeComboBox";
            this.fontSizeComboBox.Size = new Size(65, 21);
            this.fontSizeComboBox.TabIndex = 23;
            this.fontSizeComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // lboColumns
            this.lboColumns.FormattingEnabled = true;
            this.lboColumns.Location = new Point(25, 91);
            this.lboColumns.Name = "lboColumns";
            this.lboColumns.Size = new Size(270, 147);
            this.lboColumns.TabIndex = 8;
            this.lboColumns.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));

            // btnAddList
            this.btnAddList.Location = new Point(243, 244);
            this.btnAddList.Name = "btnAddList";
            this.btnAddList.Size = new Size(75, 23);
            this.btnAddList.TabIndex = 9;
            this.btnAddList.Text = "Add List";
            this.btnAddList.UseVisualStyleBackColor = true;
            this.btnAddList.Click += new EventHandler(this.btnAddList_Click);
            this.btnAddList.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            // Add Form
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(320, 280);
            this.Controls.Add(this.fontSizeComboBox);
            this.Controls.Add(this.chkPswd);
            this.Controls.Add(this.btnColumnRight);
            this.Controls.Add(this.btnColumnLeft);
            this.Controls.Add(this.lblListName);
            this.Controls.Add(this.lblColumn);
            this.Controls.Add(this.tbListName);
            this.Controls.Add(this.tbColumn);
            this.Controls.Add(this.lboColumns);
            this.Controls.Add(this.btnAddList);
            this.Controls.Add(this.btnplus);
            this.Controls.Add(this.btnminus);
            this.Text = "Add List";
            this.Padding = new Padding(25);
            this.Load += new EventHandler(this.Add_Load);
            this.MinimumSize = new Size(320, 280);
            this.MaximumSize = new Size(500, 500);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
