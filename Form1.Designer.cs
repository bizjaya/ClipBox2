using ClipBox2.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClipBox2
{
    public partial class Form1
    {
        private IContainer components = null;
        public ComboBox cbxListName;
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
        public CheckBox editChk;
        private Button editBtn;
        private Button saveBtn;

        public DataGridView dgv1;
        private Button upBtn;
        private Button dnBtn;
        private Button topBtn;
        private Button botBtn;
        private Button lefBtn;
        private Button rigBtn;
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
            cbxListName = new ComboBox();
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
            dnBtn = new Button();
            upBtn = new Button();
            editBtn = new Button();
            saveBtn = new Button();
            editChk = new CheckBox();
            topBtn = new Button();
            botBtn = new Button();
            lefBtn = new Button();
            rigBtn = new Button();
            fontSizeComboBox = new ComboBox();
            editModeLabel = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            toolbarPanel = new FlowLayoutPanel();
            toolbarPanel2 = new FlowLayoutPanel();
            tbxSrch = new TextBox();
            menuStrip1.SuspendLayout();
            ((ISupportInitialize)dgv1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            toolbarPanel.SuspendLayout();
            toolbarPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // cbxListName
            // 
            cbxListName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxListName.FormattingEnabled = true;
            cbxListName.Location = new Point(8, 4);
            cbxListName.Margin = new Padding(4, 3, 4, 3);
            cbxListName.Name = "cbxListName";
            cbxListName.Size = new Size(156, 23);
            cbxListName.TabIndex = 1;
            cbxListName.SelectedIndexChanged += cbxListName_SelectedIndexChanged;
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
            menuStrip1.Size = new Size(297, 24);
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
            titleLabel.Alignment = ToolStripItemAlignment.Right;
            titleLabel.Font = new Font("Georgia", 9F);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(156, 17);
            titleLabel.Text = "ClipBox by BIZJAYA.COM";
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
            tableLayoutPanel1.SetColumnSpan(dgv1, 2);
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
            dgv1.Location = new Point(4, 73);
            dgv1.Margin = new Padding(4, 3, 4, 3);
            dgv1.MultiSelect = false;
            dgv1.Name = "dgv1";
            dgv1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv1.RowHeadersVisible = false;
            dgv1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv1.ShowCellErrors = false;
            dgv1.Size = new Size(586, 352);
            dgv1.StandardTab = true;
            dgv1.TabIndex = 0;
            dgv1.TabStop = false;
            dgv1.CellContentClick += dgv1_CellClick;
            dgv1.LostFocus += dgv1_LostFocus;
            // 
            // dnBtn
            // 
            dnBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dnBtn.Location = new Point(92, 3);
            dnBtn.Name = "dnBtn";
            dnBtn.Size = new Size(24, 24);
            dnBtn.TabIndex = 9;
            dnBtn.Text = "▼";
            dnBtn.UseVisualStyleBackColor = true;
            dnBtn.Click += d_Click;
            // 
            // upBtn
            // 
            upBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            upBtn.Location = new Point(122, 3);
            upBtn.Name = "upBtn";
            upBtn.Size = new Size(24, 24);
            upBtn.TabIndex = 8;
            upBtn.Text = "▲";
            upBtn.UseVisualStyleBackColor = true;
            upBtn.Click += u_Click;
            // 
            // editBtn
            // 
            editBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            editBtn.FlatAppearance.BorderSize = 0;
            editBtn.FlatStyle = FlatStyle.Flat;
            editBtn.Image = (Image)resources.GetObject("editBtn.Image");
            editBtn.Location = new Point(213, 3);
            editBtn.Margin = new Padding(4, 3, 4, 3);
            editBtn.Name = "editBtn";
            editBtn.Size = new Size(24, 24);
            editBtn.TabIndex = 7;
            editBtn.UseVisualStyleBackColor = true;
            editBtn.Click += btn1_Click;
            // 
            // saveBtn
            // 
            saveBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            saveBtn.FlatAppearance.BorderSize = 0;
            saveBtn.FlatStyle = FlatStyle.Flat;
            saveBtn.Image = (Image)resources.GetObject("saveBtn.Image");
            saveBtn.Location = new Point(245, 3);
            saveBtn.Margin = new Padding(4, 3, 4, 3);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(24, 24);
            saveBtn.TabIndex = 7;
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += btn2_Click;
            // 
            // editChk
            // 
            editChk.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            editChk.Appearance = Appearance.Button;
            editChk.AutoSize = true;
            editChk.BackColor = SystemColors.ButtonFace;
            editChk.BackgroundImageLayout = ImageLayout.Center;
            editChk.ForeColor = SystemColors.Desktop;
            editChk.Location = new Point(277, 3);
            editChk.Margin = new Padding(4, 3, 4, 3);
            editChk.Name = "editChk";
            editChk.Size = new Size(6, 6);
            editChk.TabIndex = 6;
            editChk.UseVisualStyleBackColor = false;
            editChk.CheckedChanged += chk1_CheckedChanged;
            // 
            // topBtn
            // 
            topBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            topBtn.Location = new Point(182, 3);
            topBtn.Name = "topBtn";
            topBtn.Size = new Size(24, 24);
            topBtn.TabIndex = 10;
            topBtn.Text = "🔼";
            topBtn.UseVisualStyleBackColor = true;
            topBtn.Click += topButton_Click;
            // 
            // botBtn
            // 
            botBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            botBtn.Location = new Point(152, 3);
            botBtn.Name = "botBtn";
            botBtn.Size = new Size(24, 24);
            botBtn.TabIndex = 11;
            botBtn.Text = "🔽";
            botBtn.UseVisualStyleBackColor = true;
            botBtn.Click += bottomButton_Click;
            // 
            // lefBtn
            // 
            lefBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lefBtn.Location = new Point(312, 4);
            lefBtn.Name = "lefBtn";
            lefBtn.Size = new Size(24, 24);
            lefBtn.TabIndex = 12;
            lefBtn.Text = "◀";
            lefBtn.UseVisualStyleBackColor = true;
            lefBtn.Click += leftButton_Click;
            // 
            // rigBtn
            // 
            rigBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rigBtn.Location = new Point(342, 4);
            rigBtn.Name = "rigBtn";
            rigBtn.Size = new Size(24, 24);
            rigBtn.TabIndex = 13;
            rigBtn.Text = "▶";
            rigBtn.UseVisualStyleBackColor = true;
            rigBtn.Click += rightButton_Click;
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
            editModeLabel.Location = new Point(287, 6);
            editModeLabel.Margin = new Padding(0, 6, 4, 3);
            editModeLabel.Name = "editModeLabel";
            editModeLabel.Size = new Size(0, 13);
            editModeLabel.TabIndex = 16;
            editModeLabel.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(menuStrip1, 0, 0);
            tableLayoutPanel1.Controls.Add(toolbarPanel, 0, 1);
            tableLayoutPanel1.Controls.Add(dgv1, 0, 2);
            tableLayoutPanel1.Controls.Add(toolbarPanel2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(4, 20);
            tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(594, 428);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // toolbarPanel
            // 
            tableLayoutPanel1.SetColumnSpan(toolbarPanel, 2);
            toolbarPanel.Controls.Add(cbxListName);
            toolbarPanel.Controls.Add(cb2);
            toolbarPanel.Controls.Add(fontSizeComboBox);
            toolbarPanel.Controls.Add(lefBtn);
            toolbarPanel.Controls.Add(rigBtn);
            toolbarPanel.Controls.Add(tbxSrch);
            toolbarPanel.Dock = DockStyle.Fill;
            toolbarPanel.Location = new Point(0, 38);
            toolbarPanel.Margin = new Padding(0);
            toolbarPanel.Name = "toolbarPanel";
            toolbarPanel.Padding = new Padding(4, 1, 4, 1);
            toolbarPanel.Size = new Size(594, 32);
            toolbarPanel.TabIndex = 5;
            // 
            // toolbarPanel2
            // 
            toolbarPanel2.Controls.Add(editModeLabel);
            toolbarPanel2.Controls.Add(editChk);
            toolbarPanel2.Controls.Add(saveBtn);
            toolbarPanel2.Controls.Add(editBtn);
            toolbarPanel2.Controls.Add(topBtn);
            toolbarPanel2.Controls.Add(botBtn);
            toolbarPanel2.Controls.Add(upBtn);
            toolbarPanel2.Controls.Add(dnBtn);
            toolbarPanel2.FlowDirection = FlowDirection.RightToLeft;
            toolbarPanel2.Location = new Point(300, 3);
            toolbarPanel2.Name = "toolbarPanel2";
            toolbarPanel2.Size = new Size(291, 32);
            toolbarPanel2.TabIndex = 6;
            // 
            // tbxSrch
            // 
            tbxSrch.Dock = DockStyle.Fill;
            tbxSrch.Location = new Point(372, 4);
            tbxSrch.Name = "tbxSrch";
            tbxSrch.Size = new Size(204, 23);
            tbxSrch.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(602, 451);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(200, 0);
            Name = "Form1";
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
            toolbarPanel2.ResumeLayout(false);
            toolbarPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ToolStripLabel titleLabel;
        private FlowLayoutPanel toolbarPanel2;
        private TextBox tbxSrch;
    }
}
