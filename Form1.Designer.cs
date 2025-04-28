using ClipBox2.Properties;
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
        private ToolStripMenuItem openDataFolderToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem saveAsEncryptedToolStripMenuItem;
        private ToolStripMenuItem saveAsNormalToolStripMenuItem;
        public CheckBox chk1;
        private Button btn1;
        private Button btn2;

        public DataGridView dgv1;
        private Button U;
        private Button d;
        private Button topButton;
        private Button bottomButton;
        private Button leftButton;
        private Button rightButton;
        private ComboBox fontSizeComboBox;
        private Label editModeLabel;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel toolbarPanel;

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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            cb1 = new ComboBox();
            listlbl = new Label();
            cb2 = new ComboBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            addListToolStripMenuItem = new ToolStripMenuItem();
            editListToolStripMenuItem = new ToolStripMenuItem();
            deleteListToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            passwordGeneratorToolStripMenuItem = new ToolStripMenuItem();
            migrateXmlToolStripMenuItem = new ToolStripMenuItem();
            openDataFolderToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            saveAsEncryptedToolStripMenuItem = new ToolStripMenuItem();
            saveAsNormalToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            titleLabel = new ToolStripLabel();
            dgv1 = new DataGridView();
            d = new Button();
            U = new Button();
            btn1 = new Button();
            btn2 = new Button();
            chk1 = new CheckBox();
            topButton = new Button();
            bottomButton = new Button();
            leftButton = new Button();
            rightButton = new Button();
            fontSizeComboBox = new ComboBox();
            editModeLabel = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            toolbarPanel = new FlowLayoutPanel();
            menuStrip1.SuspendLayout();
            ((ISupportInitialize)dgv1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            toolbarPanel.SuspendLayout();
            SuspendLayout();
            // 
            // cb1
            // 
            cb1.DropDownStyle = ComboBoxStyle.DropDownList;
            cb1.FormattingEnabled = true;
            cb1.Location = new Point(8, 4);
            cb1.Margin = new Padding(4, 3, 4, 3);
            cb1.Name = "cb1";
            cb1.Size = new Size(156, 23);
            cb1.TabIndex = 1;
            cb1.SelectedIndexChanged += cb1_SelectedIndexChanged;
            // 
            // listlbl
            // 
            listlbl.AutoSize = true;
            listlbl.Location = new Point(8, 1);
            listlbl.Margin = new Padding(4, 0, 4, 0);
            listlbl.Name = "listlbl";
            listlbl.Size = new Size(25, 15);
            listlbl.TabIndex = 2;
            // 
            // cb2
            // 
            cb2.DropDownStyle = ComboBoxStyle.DropDownList;
            cb2.FormattingEnabled = true;
            cb2.Location = new Point(172, 4);
            cb2.Margin = new Padding(4, 3, 4, 3);
            cb2.Name = "cb2";
            cb2.Size = new Size(56, 23);
            cb2.TabIndex = 3;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, toolsToolStripMenuItem, optionsToolStripMenuItem, aboutToolStripMenuItem, titleLabel });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 2, 0, 2);
            menuStrip1.Size = new Size(619, 24);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addListToolStripMenuItem, editListToolStripMenuItem, deleteListToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // addListToolStripMenuItem
            // 
            addListToolStripMenuItem.Name = "addListToolStripMenuItem";
            addListToolStripMenuItem.Size = new Size(128, 22);
            addListToolStripMenuItem.Text = "Add List";
            addListToolStripMenuItem.Click += addListToolStripMenuItem_Click;
            // 
            // editListToolStripMenuItem
            // 
            editListToolStripMenuItem.Name = "editListToolStripMenuItem";
            editListToolStripMenuItem.Size = new Size(128, 22);
            editListToolStripMenuItem.Text = "Edit List";
            editListToolStripMenuItem.Click += editListToolStripMenuItem_Click;
            // 
            // deleteListToolStripMenuItem
            // 
            deleteListToolStripMenuItem.Name = "deleteListToolStripMenuItem";
            deleteListToolStripMenuItem.Size = new Size(128, 22);
            deleteListToolStripMenuItem.Text = "Delete List";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { passwordGeneratorToolStripMenuItem, migrateXmlToolStripMenuItem, openDataFolderToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(47, 20);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // passwordGeneratorToolStripMenuItem
            // 
            passwordGeneratorToolStripMenuItem.Name = "passwordGeneratorToolStripMenuItem";
            passwordGeneratorToolStripMenuItem.Size = new Size(179, 22);
            passwordGeneratorToolStripMenuItem.Text = "Password Generator";
            passwordGeneratorToolStripMenuItem.Click += passwordGeneratorToolStripMenuItem_Click;
            // 
            // migrateXmlToolStripMenuItem
            // 
            migrateXmlToolStripMenuItem.Name = "migrateXmlToolStripMenuItem";
            migrateXmlToolStripMenuItem.Size = new Size(179, 22);
            migrateXmlToolStripMenuItem.Text = "Migrate XML Files";
            migrateXmlToolStripMenuItem.Click += migrateXmlToolStripMenuItem_Click;
            // 
            // openDataFolderToolStripMenuItem
            // 
            openDataFolderToolStripMenuItem.Name = "openDataFolderToolStripMenuItem";
            openDataFolderToolStripMenuItem.Size = new Size(179, 22);
            openDataFolderToolStripMenuItem.Text = "Open Data Folder";
            openDataFolderToolStripMenuItem.Click += openDataFolderToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveAsEncryptedToolStripMenuItem, saveAsNormalToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // saveAsEncryptedToolStripMenuItem
            // 
            saveAsEncryptedToolStripMenuItem.CheckState = CheckState.Checked;
            saveAsEncryptedToolStripMenuItem.Checked = true;
            saveAsEncryptedToolStripMenuItem.Name = "saveAsEncryptedToolStripMenuItem";
            saveAsEncryptedToolStripMenuItem.Size = new Size(168, 22);
            saveAsEncryptedToolStripMenuItem.Text = "Save as Encrypted";
            saveAsEncryptedToolStripMenuItem.Click += saveAsEncryptedToolStripMenuItem_Click;
            // 
            // saveAsNormalToolStripMenuItem
            // 
            saveAsNormalToolStripMenuItem.Name = "saveAsNormalToolStripMenuItem";
            saveAsNormalToolStripMenuItem.Size = new Size(168, 22);
            saveAsNormalToolStripMenuItem.Text = "Save as Normal";
            saveAsNormalToolStripMenuItem.Click += saveAsNormalToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // titleLabel
            // 
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(130, 17);
            titleLabel.Font = new Font("Georgia", titleLabel.Font.Size, titleLabel.Font.Style);

            titleLabel.Text = "ClipBox by BIZJAYA.COM";
            titleLabel.Alignment = ToolStripItemAlignment.Right;

            // 
            // dgv1
            // 
            dgv1.AllowUserToAddRows = false;
            dgv1.AllowUserToDeleteRows = false;
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv1.BackgroundColor = SystemColors.ButtonHighlight;
            dgv1.BorderStyle = BorderStyle.None;
            dgv1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Arial", 8.2F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgv1.DefaultCellStyle = dataGridViewCellStyle1;
            dgv1.Dock = DockStyle.Fill;
            dgv1.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgv1.GridColor = SystemColors.ButtonHighlight;
            dgv1.Location = new Point(4, 59);
            dgv1.Margin = new Padding(4, 3, 4, 3);
            dgv1.MultiSelect = false;
            dgv1.Name = "dgv1";
            dgv1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv1.RowHeadersVisible = false;
            dgv1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv1.ShowCellErrors = false;
            dgv1.Size = new Size(611, 419);
            dgv1.StandardTab = true;
            dgv1.TabIndex = 0;
            dgv1.TabStop = false;
            dgv1.CellContentClick += dgv1_CellClick;
            dgv1.LostFocus += dgv1_LostFocus;
            // 
            // d
            // 
            d.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            d.Location = new Point(438, 4);
            d.Margin = new Padding(4, 3, 4, 3);
            d.Name = "d";
            d.Size = new Size(21, 24);
            d.TabIndex = 9;
            d.Text = "↓";
            d.UseVisualStyleBackColor = true;
            d.Click += d_Click;
            // 
            // U
            // 
            U.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            U.Location = new Point(409, 4);
            U.Margin = new Padding(4, 3, 4, 3);
            U.Name = "U";
            U.Size = new Size(21, 24);
            U.TabIndex = 8;
            U.Text = "↑";
            U.UseVisualStyleBackColor = true;
            U.Click += u_Click;
            // 
            // btn1
            // 
            btn1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn1.FlatAppearance.BorderSize = 0;
            btn1.FlatStyle = FlatStyle.Flat;
            btn1.Image = (Image)resources.GetObject("btn1.Image");
            btn1.Location = new Point(499, 4);
            btn1.Margin = new Padding(4, 3, 4, 3);
            btn1.Name = "btn1";
            btn1.Size = new Size(24, 24);
            btn1.TabIndex = 7;
            btn1.UseVisualStyleBackColor = true;
            btn1.Click += btn1_Click;
            // 
            // btn2
            // 
            btn2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn2.FlatAppearance.BorderSize = 0;
            btn2.FlatStyle = FlatStyle.Flat;
            btn2.Image = (Image)resources.GetObject("btn2.Image");
            btn2.Location = new Point(531, 4);
            btn2.Margin = new Padding(4, 3, 4, 3);
            btn2.Name = "btn2";
            btn2.Size = new Size(24, 24);
            btn2.TabIndex = 7;
            btn2.UseVisualStyleBackColor = true;
            btn2.Click += btn2_Click;
            // 
            // chk1
            // 
            chk1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chk1.Appearance = Appearance.Button;
            chk1.AutoSize = true;
            chk1.BackColor = SystemColors.ButtonFace;
            chk1.BackgroundImageLayout = ImageLayout.Center;
            chk1.ForeColor = SystemColors.Desktop;
            chk1.Location = new Point(563, 4);
            chk1.Margin = new Padding(4, 3, 4, 3);
            chk1.Name = "chk1";
            chk1.Size = new Size(6, 6);
            chk1.TabIndex = 6;
            chk1.UseVisualStyleBackColor = false;
            chk1.CheckedChanged += chk1_CheckedChanged;
            // 
            // topButton
            // 
            topButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            topButton.Location = new Point(377, 4);
            topButton.Margin = new Padding(4, 3, 4, 3);
            topButton.Name = "topButton";
            topButton.Size = new Size(24, 24);
            topButton.TabIndex = 10;
            topButton.Text = "↥";
            topButton.UseVisualStyleBackColor = true;
            topButton.Click += topButton_Click;
            // 
            // bottomButton
            // 
            bottomButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bottomButton.Location = new Point(467, 4);
            bottomButton.Margin = new Padding(4, 3, 4, 3);
            bottomButton.Name = "bottomButton";
            bottomButton.Size = new Size(24, 24);
            bottomButton.TabIndex = 11;
            bottomButton.Text = "↧";
            bottomButton.UseVisualStyleBackColor = true;
            bottomButton.Click += bottomButton_Click;
            // 
            // leftButton
            // 
            leftButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            leftButton.Location = new Point(313, 4);
            leftButton.Margin = new Padding(4, 3, 4, 3);
            leftButton.Name = "leftButton";
            leftButton.Size = new Size(24, 24);
            leftButton.TabIndex = 12;
            leftButton.Text = "←";
            leftButton.UseVisualStyleBackColor = true;
            leftButton.Click += leftButton_Click;
            // 
            // rightButton
            // 
            rightButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rightButton.Location = new Point(345, 4);
            rightButton.Margin = new Padding(4, 3, 4, 3);
            rightButton.Name = "rightButton";
            rightButton.Size = new Size(24, 24);
            rightButton.TabIndex = 13;
            rightButton.Text = "→";
            rightButton.UseVisualStyleBackColor = true;
            rightButton.Click += rightButton_Click;
            // 
            // fontSizeComboBox
            // 
            fontSizeComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            fontSizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            fontSizeComboBox.FormattingEnabled = true;
            fontSizeComboBox.Items.AddRange(new object[] { "Size 7", "Size 8", "Size 9", "Size 10", "Size 11", "Size 12", "Size 13", "Size 14", "Size 15" });
            fontSizeComboBox.Location = new Point(236, 4);
            fontSizeComboBox.Margin = new Padding(4, 3, 4, 3);
            fontSizeComboBox.Name = "fontSizeComboBox";
            fontSizeComboBox.Size = new Size(69, 23);
            fontSizeComboBox.TabIndex = 14;
            fontSizeComboBox.SelectedIndexChanged += fontSizeComboBox_SelectedIndexChanged;
            // 
            // editModeLabel
            // 
            editModeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            editModeLabel.AutoSize = true;
            editModeLabel.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            editModeLabel.ForeColor = Color.Red;
            editModeLabel.Location = new Point(550, 9);
            editModeLabel.Margin = new Padding(0, 6, 4, 3);
            editModeLabel.Name = "editModeLabel";
            editModeLabel.Size = new Size(13, 13);
            editModeLabel.TabIndex = 16;
            editModeLabel.Visible = false;



            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(menuStrip1, 0, 0);
            tableLayoutPanel1.Controls.Add(toolbarPanel, 0, 1);
            tableLayoutPanel1.Controls.Add(dgv1, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(4, 20);
            tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(619, 481);
            tableLayoutPanel1.TabIndex = 0;
            tableLayoutPanel1.AutoSize = false;

            // 
            // toolbarPanel
            // 
            toolbarPanel.AutoScroll = true;
            toolbarPanel.AutoSize = false;
            toolbarPanel.Controls.Add(cb1);
            toolbarPanel.Controls.Add(cb2);
            toolbarPanel.Controls.Add(fontSizeComboBox);
            toolbarPanel.Controls.Add(leftButton);
            toolbarPanel.Controls.Add(rightButton);
            toolbarPanel.Controls.Add(topButton);
            toolbarPanel.Controls.Add(U);
            toolbarPanel.Controls.Add(d);
            toolbarPanel.Controls.Add(bottomButton);
            toolbarPanel.Controls.Add(btn1);
            toolbarPanel.Controls.Add(btn2);
            toolbarPanel.Controls.Add(chk1);
            toolbarPanel.Controls.Add(editModeLabel);
            toolbarPanel.Dock = DockStyle.Fill;
            toolbarPanel.Location = new Point(0, 24);
            toolbarPanel.Margin = new Padding(0);
            toolbarPanel.Name = "toolbarPanel";
            toolbarPanel.Padding = new Padding(4, 1, 4, 1);
            toolbarPanel.Size = new Size(619, 32);
            toolbarPanel.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(627, 504);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            //MinimumSize = new Size(464, 340);
            Name = "Form1";
            MinimumSize = new Size(200, this.MinimumSize.Height);

            Padding = new Padding(4, 20, 4, 3);
            TopMost = true;
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((ISupportInitialize)dgv1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            toolbarPanel.ResumeLayout(false);
            toolbarPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ToolStripLabel titleLabel;
    }
}
