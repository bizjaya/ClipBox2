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

        private void InitializeComponent()
        {
            this.tbListName = new TextBox();
            this.tbColumn = new TextBox();
            this.lboColumns = new ListBox();
            this.btnAddList = new Button();
            this.btnplus = new Button();
            this.btnminus = new Button();
            this.SuspendLayout();

            // tbListName
            this.tbListName.Location = new Point(85, 11);
            this.tbListName.Name = "tbListName";
            this.tbListName.Size = new Size(174, 20);

            // tbColumn
            this.tbColumn.Location = new Point(85, 38);
            this.tbColumn.Name = "tbColumn";
            this.tbColumn.Size = new Size(95, 20);

            // lboColumns
            this.lboColumns.FormattingEnabled = true;
            this.lboColumns.Location = new Point(25, 73);
            this.lboColumns.Name = "lboColumns";
            this.lboColumns.Size = new Size(234, 147);

            // btnAddList
            this.btnAddList.Location = new Point(184, 229);
            this.btnAddList.Name = "btnAddList";
            this.btnAddList.Size = new Size(75, 23);
            this.btnAddList.Text = "Add List";
            this.btnAddList.UseVisualStyleBackColor = true;
            this.btnAddList.Click += new EventHandler(this.btnAddList_Click);

            // btnplus
            this.btnplus.Location = new Point(190, 37);
            this.btnplus.Name = "btnplus";
            this.btnplus.Size = new Size(19, 23);
            this.btnplus.Text = "+";
            this.btnplus.UseVisualStyleBackColor = true;
            this.btnplus.Click += new EventHandler(this.btnplus_Click);

            // btnminus
            this.btnminus.Location = new Point(213, 37);
            this.btnminus.Name = "btnminus";
            this.btnminus.Size = new Size(19, 23);
            this.btnminus.Text = "-";
            this.btnminus.UseVisualStyleBackColor = true;
            this.btnminus.Click += new EventHandler(this.btnminus_Click);

            // Add Form
            this.ClientSize = new Size(284, 262);
            this.Controls.Add(this.tbListName);
            this.Controls.Add(this.tbColumn);
            this.Controls.Add(this.lboColumns);
            this.Controls.Add(this.btnAddList);
            this.Controls.Add(this.btnplus);
            this.Controls.Add(this.btnminus);
            this.Text = "Add List";
            this.Load += new EventHandler(this.Add_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
