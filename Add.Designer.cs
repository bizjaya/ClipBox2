namespace ClipBox2
{
    partial class Add
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblListName;
        private System.Windows.Forms.TextBox tbListName;
        private System.Windows.Forms.Label lblColumn;
        private System.Windows.Forms.TextBox tbColumn;
        private System.Windows.Forms.Button btnplus;
        private System.Windows.Forms.Button btnminus;
        private System.Windows.Forms.ListBox lboColumns;
        private System.Windows.Forms.Button btnAddList;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.lblListName = new System.Windows.Forms.Label();
            this.tbListName = new System.Windows.Forms.TextBox();
            this.lblColumn = new System.Windows.Forms.Label();
            this.tbColumn = new System.Windows.Forms.TextBox();
            this.btnplus = new System.Windows.Forms.Button();
            this.btnminus = new System.Windows.Forms.Button();
            this.lboColumns = new System.Windows.Forms.ListBox();
            this.btnAddList = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblListName
            // 
            this.lblListName.AutoSize = true;
            this.lblListName.Location = new System.Drawing.Point(12, 15);
            this.lblListName.Name = "lblListName";
            this.lblListName.Size = new System.Drawing.Size(54, 13);
            this.lblListName.TabIndex = 0;
            this.lblListName.Text = "List Name";
            // 
            // tbListName
            // 
            this.tbListName.Location = new System.Drawing.Point(80, 12);
            this.tbListName.Name = "tbListName";
            this.tbListName.Size = new System.Drawing.Size(165, 20);
            this.tbListName.TabIndex = 1;
            // 
            // lblColumn
            // 
            this.lblColumn.AutoSize = true;
            this.lblColumn.Location = new System.Drawing.Point(12, 43);
            this.lblColumn.Name = "lblColumn";
            this.lblColumn.Size = new System.Drawing.Size(42, 13);
            this.lblColumn.TabIndex = 2;
            this.lblColumn.Text = "Column";
            // 
            // tbColumn
            // 
            this.tbColumn.Location = new System.Drawing.Point(80, 40);
            this.tbColumn.Name = "tbColumn";
            this.tbColumn.Size = new System.Drawing.Size(108, 20);
            this.tbColumn.TabIndex = 3;
            // 
            // btnplus
            // 
            this.btnplus.Location = new System.Drawing.Point(194, 38);
            this.btnplus.Name = "btnplus";
            this.btnplus.Size = new System.Drawing.Size(23, 23);
            this.btnplus.TabIndex = 4;
            this.btnplus.Text = "+";
            this.btnplus.UseVisualStyleBackColor = true;
            this.btnplus.Click += new System.EventHandler(this.btnplus_Click);
            // 
            // btnminus
            // 
            this.btnminus.Location = new System.Drawing.Point(222, 38);
            this.btnminus.Name = "btnminus";
            this.btnminus.Size = new System.Drawing.Size(23, 23);
            this.btnminus.TabIndex = 5;
            this.btnminus.Text = "-";
            this.btnminus.UseVisualStyleBackColor = true;
            this.btnminus.Click += new System.EventHandler(this.btnminus_Click);
            // 
            // lboColumns
            // 
            this.lboColumns.FormattingEnabled = true;
            this.lboColumns.Location = new System.Drawing.Point(15, 68);
            this.lboColumns.Name = "lboColumns";
            this.lboColumns.Size = new System.Drawing.Size(230, 121);
            this.lboColumns.TabIndex = 6;
            // 
            // btnAddList
            // 
            this.btnAddList.Location = new System.Drawing.Point(170, 201);
            this.btnAddList.Name = "btnAddList";
            this.btnAddList.Size = new System.Drawing.Size(75, 23);
            this.btnAddList.TabIndex = 7;
            this.btnAddList.Text = "Add List";
            this.btnAddList.UseVisualStyleBackColor = true;
            this.btnAddList.Click += new System.EventHandler(this.btnAddList_Click);
            // 
            // Add
            // 
            this.ClientSize = new System.Drawing.Size(264, 241);
            this.Controls.Add(this.btnAddList);
            this.Controls.Add(this.lboColumns);
            this.Controls.Add(this.btnminus);
            this.Controls.Add(this.btnplus);
            this.Controls.Add(this.tbColumn);
            this.Controls.Add(this.lblColumn);
            this.Controls.Add(this.tbListName);
            this.Controls.Add(this.lblListName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Add_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
