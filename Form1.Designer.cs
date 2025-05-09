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
            passwordGeneratorToolStripMenuItem = new ToolStripMenuItem();
            migrateXmlToolStripMenuItem = new ToolStripMenuItem();
            openDataFolderToolStripMenuItem = new ToolStripMenuItem();
            saveAsEncryptedToolStripMenuItem = new ToolStripMenuItem();
            saveAsNormalToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            titleLabel = new ToolStripLabel();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
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
            titleStripTable = new TableLayoutPanel();
            toolbarPanel0 = new FlowLayoutPanel();
            panel = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tbxSrch = new TextBox();
            clrBtn = new Button();
            menuStrip1.SuspendLayout();
            ((ISupportInitialize)dgv1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            titleStripTable.SuspendLayout();
            toolbarPanel0.SuspendLayout();
            panel.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // cbxListName
            // 
            cbxListName.Dock = DockStyle.Fill;
            cbxListName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxListName.FormattingEnabled = true;
            cbxListName.Location = new Point(0, 0);
            cbxListName.Margin = new Padding(4, 3, 4, 3);
            cbxListName.Name = "cbxListName";
            cbxListName.Size = new Size(295, 23);
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
            cb2.Location = new Point(229, 3);
            cb2.Margin = new Padding(4, 3, 4, 3);
            cb2.Name = "cb2";
            cb2.Size = new Size(62, 23);
            cb2.TabIndex = 3;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem, titleLabel });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 2, 0, 2);
            menuStrip1.Size = new Size(155, 24);
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
            addListToolStripMenuItem.Size = new Size(179, 22);
            addListToolStripMenuItem.Text = "Add List";
            addListToolStripMenuItem.Click += addListToolStripMenuItem_Click;
            // 
            // editListToolStripMenuItem
            // 
            editListToolStripMenuItem.Name = "editListToolStripMenuItem";
            editListToolStripMenuItem.Size = new Size(179, 22);
            editListToolStripMenuItem.Text = "Edit List";
            editListToolStripMenuItem.Click += editListToolStripMenuItem_Click;
            // 
            // deleteListToolStripMenuItem
            // 
            deleteListToolStripMenuItem.Name = "deleteListToolStripMenuItem";
            deleteListToolStripMenuItem.Size = new Size(179, 22);
            deleteListToolStripMenuItem.Text = "Delete List";
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
            titleLabel.Size = new Size(0, 17);
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { passwordGeneratorToolStripMenuItem, migrateXmlToolStripMenuItem, openDataFolderToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(47, 20);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveAsEncryptedToolStripMenuItem, saveAsNormalToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
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
            dataGridViewCellStyle1.Font = new Font("Arial", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(173, 216, 230);
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgv1.DefaultCellStyle = dataGridViewCellStyle1;
            dgv1.Dock = DockStyle.Fill;
            dgv1.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgv1.GridColor = SystemColors.ButtonHighlight;
            dgv1.Location = new Point(4, 75);
            dgv1.Margin = new Padding(4, 3, 4, 3);
            dgv1.MultiSelect = false;
            dgv1.Name = "dgv1";
            dgv1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv1.RowHeadersVisible = false;
            dgv1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv1.ShowCellErrors = false;
            dgv1.Size = new Size(573, 576);
            dgv1.StandardTab = true;
            dgv1.TabIndex = 0;
            dgv1.TabStop = false;
            dgv1.CellContentClick += dgv1_CellClick;
            dgv1.CellDoubleClick += dgv1_CellDoubleClick;
            dgv1.LostFocus += dgv1_LostFocus;
            // 
            // dnBtn
            // 
            dnBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dnBtn.Location = new Point(50, 0);
            dnBtn.Margin = new Padding(0);
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
            upBtn.Location = new Point(74, 0);
            upBtn.Margin = new Padding(0);
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
            editBtn.Location = new Point(150, 3);
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
            saveBtn.Location = new Point(182, 3);
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
            editChk.Location = new Point(214, 3);
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
            topBtn.Location = new Point(122, 0);
            topBtn.Margin = new Padding(0);
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
            botBtn.Location = new Point(98, 0);
            botBtn.Margin = new Padding(0);
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
            lefBtn.Location = new Point(2, 0);
            lefBtn.Margin = new Padding(0);
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
            rigBtn.Location = new Point(26, 0);
            rigBtn.Margin = new Padding(0);
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
            fontSizeComboBox.Location = new Point(169, 3);
            fontSizeComboBox.Margin = new Padding(4, 3, 4, 3);
            fontSizeComboBox.Name = "fontSizeComboBox";
            fontSizeComboBox.Size = new Size(52, 23);
            fontSizeComboBox.TabIndex = 14;
            fontSizeComboBox.SelectedIndexChanged += fontSizeComboBox_SelectedIndexChanged;
            // 
            // editModeLabel
            // 
            editModeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            editModeLabel.AutoSize = true;
            editModeLabel.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            editModeLabel.ForeColor = Color.Red;
            editModeLabel.Location = new Point(224, 6);
            editModeLabel.Margin = new Padding(0, 6, 4, 3);
            editModeLabel.Name = "editModeLabel";
            editModeLabel.Size = new Size(0, 13);
            editModeLabel.TabIndex = 16;
            editModeLabel.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.93798F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.0620155F));
            tableLayoutPanel1.Controls.Add(titleStripTable, 0, 0);
            tableLayoutPanel1.Controls.Add(toolbarPanel0, 1, 0);
            tableLayoutPanel1.Controls.Add(panel, 0, 1);
            tableLayoutPanel1.Controls.Add(dgv1, 0, 2);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(4, 20);
            tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(581, 654);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // titleStripTable
            // 
            titleStripTable.ColumnCount = 3;
            titleStripTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            titleStripTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            titleStripTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            titleStripTable.Controls.Add(menuStrip1, 0, 0);
            titleStripTable.Controls.Add(fontSizeComboBox, 1, 0);
            titleStripTable.Controls.Add(cb2, 2, 0);
            titleStripTable.Dock = DockStyle.Fill;
            titleStripTable.Location = new Point(3, 3);
            titleStripTable.Name = "titleStripTable";
            titleStripTable.RowCount = 1;
            titleStripTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            titleStripTable.Size = new Size(295, 30);
            titleStripTable.TabIndex = 18;
            // 
            // toolbarPanel0
            // 
            toolbarPanel0.Controls.Add(editModeLabel);
            toolbarPanel0.Controls.Add(editChk);
            toolbarPanel0.Controls.Add(saveBtn);
            toolbarPanel0.Controls.Add(editBtn);
            toolbarPanel0.Controls.Add(topBtn);
            toolbarPanel0.Controls.Add(botBtn);
            toolbarPanel0.Controls.Add(upBtn);
            toolbarPanel0.Controls.Add(dnBtn);
            toolbarPanel0.Controls.Add(rigBtn);
            toolbarPanel0.Controls.Add(lefBtn);
            toolbarPanel0.Dock = DockStyle.Right;
            toolbarPanel0.FlowDirection = FlowDirection.RightToLeft;
            toolbarPanel0.Location = new Point(350, 3);
            toolbarPanel0.Name = "toolbarPanel0";
            toolbarPanel0.Size = new Size(228, 30);
            toolbarPanel0.TabIndex = 6;
            // 
            // panel
            // 
            panel.Controls.Add(cbxListName);
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(3, 39);
            panel.Name = "panel";
            panel.Size = new Size(295, 30);
            panel.TabIndex = 17;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.Controls.Add(tbxSrch, 0, 0);
            tableLayoutPanel2.Controls.Add(clrBtn, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(304, 39);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(274, 30);
            tableLayoutPanel2.TabIndex = 17;
            // 
            // tbxSrch
            // 
            tbxSrch.BackColor = SystemColors.Control;
            tbxSrch.Dock = DockStyle.Fill;
            tbxSrch.Location = new Point(3, 3);
            tbxSrch.Name = "tbxSrch";
            tbxSrch.Size = new Size(238, 23);
            tbxSrch.TabIndex = 15;
            // 
            // clrBtn
            // 
            clrBtn.Location = new Point(247, 3);
            clrBtn.Name = "clrBtn";
            clrBtn.Size = new Size(24, 24);
            clrBtn.TabIndex = 16;
            clrBtn.Text = "🗙";
            clrBtn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(589, 677);
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
            titleStripTable.ResumeLayout(false);
            titleStripTable.PerformLayout();
            toolbarPanel0.ResumeLayout(false);
            toolbarPanel0.PerformLayout();
            panel.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ToolStripLabel titleLabel;
        private FlowLayoutPanel toolbarPanel0;
        private TextBox tbxSrch;
        private Button clrBtn;
        private Panel panel;
        private TableLayoutPanel titleStripTable;
        private TableLayoutPanel tableLayoutPanel2;
    }
}
