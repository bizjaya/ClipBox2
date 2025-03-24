using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClipBox2
{
    public partial class Form1
    {
        private IContainer components = null;
        public ComboBox cb1;
        private Label listlbl;
        public ComboBox cb2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem addListToolStripMenuItem;
        private ToolStripMenuItem deleteListToolStripMenuItem;
        private ToolStripMenuItem editListToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem passwordGeneratorToolStripMenuItem;
        private ToolStripMenuItem migrateXmlToolStripMenuItem;
        public CheckBox chk1;
        private Button btn1;
        public DataGridView dgv1;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem saveAsEncryptedToolStripMenuItem;
        private ToolStripMenuItem saveAsNormalToolStripMenuItem;
        private Button U;
        private Button d;

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
            this.components = new Container();
            System.Windows.Forms.DataGridViewCellStyle gridViewCellStyle =
                new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager componentResourceManager =
                new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cb1 = new System.Windows.Forms.ComboBox();
            this.listlbl = new System.Windows.Forms.Label();
            this.cb2 = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passwordGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.migrateXmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsEncryptedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.d = new System.Windows.Forms.Button();
            this.U = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.chk1 = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // cb1
            // 
            this.cb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb1.FormattingEnabled = true;
            this.cb1.Location = new System.Drawing.Point(26, 21);
            this.cb1.Name = "cb1";
            this.cb1.Size = new System.Drawing.Size(134, 21);
            this.cb1.TabIndex = 1;
            this.cb1.SelectedIndexChanged += new System.EventHandler(this.cb1_SelectedIndexChanged);
            // 
            // listlbl
            // 
            this.listlbl.AutoSize = true;
            this.listlbl.Location = new System.Drawing.Point(3, 25);
            this.listlbl.Name = "listlbl";
            this.listlbl.Size = new System.Drawing.Size(23, 13);
            this.listlbl.TabIndex = 2;
            this.listlbl.Text = "List";
            // 
            // cb2
            // 
            this.cb2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb2.FormattingEnabled = true;
            this.cb2.Location = new System.Drawing.Point(164, 21);
            this.cb2.Name = "cb2";
            this.cb2.Size = new System.Drawing.Size(49, 21);
            this.cb2.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new ToolStripItem[] {
                this.fileToolStripMenuItem,
                this.toolsToolStripMenuItem,
                this.optionsToolStripMenuItem,
                this.aboutToolStripMenuItem
            });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(286, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.addListToolStripMenuItem,
                this.editListToolStripMenuItem,
                this.deleteListToolStripMenuItem
            });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // addListToolStripMenuItem
            // 
            this.addListToolStripMenuItem.Name = "addListToolStripMenuItem";
            this.addListToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.addListToolStripMenuItem.Text = "Add List";
            this.addListToolStripMenuItem.Click += new System.EventHandler(this.addListToolStripMenuItem_Click);
            // 
            // editListToolStripMenuItem
            // 
            this.editListToolStripMenuItem.Name = "editListToolStripMenuItem";
            this.editListToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.editListToolStripMenuItem.Text = "Edit List";
            this.editListToolStripMenuItem.Click += new System.EventHandler(this.editListToolStripMenuItem_Click);
            // 
            // deleteListToolStripMenuItem
            // 
            this.deleteListToolStripMenuItem.Name = "deleteListToolStripMenuItem";
            this.deleteListToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.deleteListToolStripMenuItem.Text = "Delete List";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.passwordGeneratorToolStripMenuItem,
                this.migrateXmlToolStripMenuItem
            });
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // passwordGeneratorToolStripMenuItem
            // 
            this.passwordGeneratorToolStripMenuItem.Name = "passwordGeneratorToolStripMenuItem";
            this.passwordGeneratorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.passwordGeneratorToolStripMenuItem.Text = "Password Generator";
            this.passwordGeneratorToolStripMenuItem.Click += new System.EventHandler(this.passwordGeneratorToolStripMenuItem_Click);
            // 
            // migrateXmlToolStripMenuItem
            // 
            this.migrateXmlToolStripMenuItem.Name = "migrateXmlToolStripMenuItem";
            this.migrateXmlToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.migrateXmlToolStripMenuItem.Text = "Migrate XML Files";
            this.migrateXmlToolStripMenuItem.Click += new System.EventHandler(this.migrateXmlToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.saveAsEncryptedToolStripMenuItem,
                this.saveAsNormalToolStripMenuItem
            });
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // saveAsEncryptedToolStripMenuItem
            // 
            this.saveAsEncryptedToolStripMenuItem.Checked = true;
            this.saveAsEncryptedToolStripMenuItem.CheckState = CheckState.Checked;
            this.saveAsEncryptedToolStripMenuItem.Name = "saveAsEncryptedToolStripMenuItem";
            this.saveAsEncryptedToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.saveAsEncryptedToolStripMenuItem.Text = "Save as Encrypted";
            this.saveAsEncryptedToolStripMenuItem.Click += new System.EventHandler(this.saveAsEncryptedToolStripMenuItem_Click);
            // 
            // saveAsNormalToolStripMenuItem
            // 
            this.saveAsNormalToolStripMenuItem.Name = "saveAsNormalToolStripMenuItem";
            this.saveAsNormalToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.saveAsNormalToolStripMenuItem.Text = "Save as Normal";
            this.saveAsNormalToolStripMenuItem.Click += new System.EventHandler(this.saveAsNormalToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // dgv1
            // 
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.AllowUserToDeleteRows = false;
            this.dgv1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom
                                | AnchorStyles.Left | AnchorStyles.Right);
            this.dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv1.BackgroundColor = SystemColors.ButtonHighlight;
            this.dgv1.BorderStyle = BorderStyle.None;
            this.dgv1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.dgv1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle.BackColor = SystemColors.Window;
            gridViewCellStyle.Font = new Font("Arial", 8.2F, FontStyle.Regular,
                                              GraphicsUnit.Pixel);
            gridViewCellStyle.ForeColor = SystemColors.ControlText;
            gridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle.WrapMode = DataGridViewTriState.False;
            this.dgv1.DefaultCellStyle = gridViewCellStyle;
            this.dgv1.EditMode = DataGridViewEditMode.EditOnKeystroke;
            this.dgv1.GridColor = SystemColors.ButtonFace;
            this.dgv1.Location = new System.Drawing.Point(0, 67);
            this.dgv1.MultiSelect = false;
            this.dgv1.Name = "dgv1";
            this.dgv1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgv1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dgv1.ShowCellErrors = false;
            this.dgv1.Size = new System.Drawing.Size(286, 389);
            this.dgv1.StandardTab = true;
            this.dgv1.TabIndex = 0;
            this.dgv1.TabStop = false;
            this.dgv1.CellContentClick += new DataGridViewCellEventHandler(this.dgv1_CellClick);
            this.dgv1.LostFocus += new System.EventHandler(this.dgv1_LostFocus);
            // 
            // d
            // 
            this.d.Image = ((System.Drawing.Image)(componentResourceManager.GetObject("d.Image")));
            this.d.ImageAlign = ContentAlignment.BottomCenter;
            this.d.Location = new System.Drawing.Point(258, 46);
            this.d.Name = "d";
            this.d.Size = new System.Drawing.Size(18, 16);
            this.d.TabIndex = 9;
            this.d.TextAlign = ContentAlignment.TopRight;
            this.d.UseVisualStyleBackColor = true;
            this.d.Click += new System.EventHandler(this.d_Click);
            // 
            // U
            // 
            this.U.Image = ((System.Drawing.Image)(componentResourceManager.GetObject("U.Image")));
            this.U.ImageAlign = ContentAlignment.TopCenter;
            this.U.Location = new System.Drawing.Point(258, 31);
            this.U.Name = "U";
            this.U.Size = new System.Drawing.Size(18, 16);
            this.U.TabIndex = 8;
            this.U.UseVisualStyleBackColor = true;
            this.U.Click += new System.EventHandler(this.u_Click);
            // 
            // btn1
            // 
            this.btn1.Image = ((System.Drawing.Image)(componentResourceManager.GetObject("btn1.Image")));
            this.btn1.Location = new System.Drawing.Point(237, 39);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(22, 23);
            this.btn1.TabIndex = 7;
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // chk1
            // 
            this.chk1.Appearance = Appearance.Button;
            this.chk1.AutoSize = true;
            this.chk1.BackColor = SystemColors.ButtonFace;
            this.chk1.BackgroundImageLayout = ImageLayout.Center;
            this.chk1.ForeColor = SystemColors.Desktop;
            this.chk1.Image = ((System.Drawing.Image)(componentResourceManager.GetObject("chk1.Image")));
            this.chk1.Location = new System.Drawing.Point(215, 39);
            this.chk1.Name = "chk1";
            this.chk1.Size = new System.Drawing.Size(22, 22);
            this.chk1.TabIndex = 6;
            this.chk1.UseVisualStyleBackColor = false;
            this.chk1.CheckedChanged += new System.EventHandler(this.chk1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 456);
            this.Controls.Add(this.d);
            this.Controls.Add(this.U);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.chk1);
            this.Controls.Add(this.dgv1);
            this.Controls.Add(this.cb2);
            this.Controls.Add(this.listlbl);
            this.Controls.Add(this.cb1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(componentResourceManager.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.Text = "ClipBox V2.4";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
