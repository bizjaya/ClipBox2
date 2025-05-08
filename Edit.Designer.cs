using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClipBox2;

partial class Edit : MaterialSkin.Controls.MaterialForm
{
    private IContainer components = null;

    private TextBox tbcolname;
    private Button btnminus;
    private Button btnplus;
    private Button btnEdit;
    private Label lblColumns;
    private Label lblAdd;
    private ListBox lboColumns;
    private DataGridView dgvColumns; // DataGridView for columns
    private TextBox tbxList; // TextBox for Add mode
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
        tbcolname = new TextBox();
        btnminus = new Button();
        btnplus = new Button();
        btnEdit = new Button();
        lblColumns = new Label();
        lblAdd = new Label();
        lboColumns = new ListBox();
        dgvColumns = new DataGridView();
        colName = new DataGridViewTextBoxColumn();
        colMulti = new DataGridViewCheckBoxColumn();
        tbxList = new TextBox();
        cboList = new ComboBox();
        btnColumnLeft = new Button();
        btnColumnRight = new Button();
        chkPswd = new CheckBox();
        fontSizeComboBox = new ComboBox();
        tableLayoutPanel1 = new TableLayoutPanel();
        panel1 = new Panel();
        chkMulti = new CheckBox();
        ((ISupportInitialize)dgvColumns).BeginInit();
        tableLayoutPanel1.SuspendLayout();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // tbcolname
        // 
        tbcolname.Location = new Point(99, 44);
        tbcolname.Margin = new Padding(4, 3, 4, 3);
        tbcolname.Name = "tbcolname";
        tbcolname.Size = new Size(75, 23);
        tbcolname.TabIndex = 11;
        // 
        // btnminus
        // 
        btnminus.Location = new Point(204, 43);
        btnminus.Margin = new Padding(4, 3, 4, 3);
        btnminus.Name = "btnminus";
        btnminus.Size = new Size(22, 27);
        btnminus.TabIndex = 14;
        btnminus.Text = "-";
        btnminus.UseVisualStyleBackColor = true;
        btnminus.Click += btnminus_Click;
        // 
        // btnplus
        // 
        btnplus.Location = new Point(181, 43);
        btnplus.Margin = new Padding(4, 3, 4, 3);
        btnplus.Name = "btnplus";
        btnplus.Size = new Size(22, 27);
        btnplus.TabIndex = 13;
        btnplus.Text = "+";
        btnplus.UseVisualStyleBackColor = true;
        btnplus.Click += btnplus_Click;
        // 
        // btnEdit
        // 
        btnEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnEdit.AutoSize = true;
        btnEdit.Location = new Point(431, 303);
        btnEdit.Margin = new Padding(4, 3, 4, 3);
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(88, 24);
        btnEdit.TabIndex = 12;
        btnEdit.Text = "Edit List";
        btnEdit.UseVisualStyleBackColor = true;
        btnEdit.Click += btnEdit_Click;
        // 
        // lblColumns
        // 
        lblColumns.AutoSize = true;
        lblColumns.Location = new Point(29, 47);
        lblColumns.Margin = new Padding(4, 0, 4, 0);
        lblColumns.Name = "lblColumns";
        lblColumns.Size = new Size(50, 15);
        lblColumns.TabIndex = 11;
        lblColumns.Text = "Column";
        // 
        // lblAdd
        // 
        lblAdd.AutoSize = true;
        lblAdd.Location = new Point(29, 18);
        lblAdd.Margin = new Padding(4, 0, 4, 0);
        lblAdd.Name = "lblAdd";
        lblAdd.Size = new Size(60, 15);
        lblAdd.TabIndex = 10;
        lblAdd.Text = "List Name";
        // 
        // lboColumns
        // 
        lboColumns.Dock = DockStyle.Fill;
        lboColumns.Location = new Point(4, 153);
        lboColumns.Margin = new Padding(4, 3, 4, 3);
        lboColumns.Name = "lboColumns";
        lboColumns.Size = new Size(515, 144);
        lboColumns.TabIndex = 24;
        // 
        // dgvColumns
        // 
        dgvColumns.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvColumns.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvColumns.Columns.AddRange(new DataGridViewColumn[] { colName, colMulti });
        dgvColumns.Location = new Point(25, 91);
        dgvColumns.Name = "dgvColumns";
        dgvColumns.Size = new Size(400, 180);
        dgvColumns.TabIndex = 100;
        // 
        // colName
        // 
        colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colName.HeaderText = "Column Name";
        colName.Name = "colName";
        // 
        // colMulti
        // 
        colMulti.HeaderText = "Multi";
        colMulti.Name = "colMulti";
        colMulti.Width = 50;
        // 
        // tbxList
        // 
        tbxList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        tbxList.Location = new Point(85, 11);
        tbxList.Name = "tbxList";
        tbxList.Size = new Size(130, 23);
        tbxList.TabIndex = 101;
        tbxList.Visible = false;
        // 
        // cboList
        // 
        cboList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboList.FormattingEnabled = true;
        cboList.Location = new Point(99, 13);
        cboList.Margin = new Padding(4, 3, 4, 3);
        cboList.Name = "cboList";
        cboList.Size = new Size(272, 23);
        cboList.TabIndex = 16;
        cboList.SelectedIndexChanged += cboList_SelectedIndexChanged;
        // 
        // btnColumnLeft
        // 
        btnColumnLeft.Location = new Point(227, 43);
        btnColumnLeft.Margin = new Padding(4, 3, 4, 3);
        btnColumnLeft.Name = "btnColumnLeft";
        btnColumnLeft.Size = new Size(22, 27);
        btnColumnLeft.TabIndex = 20;
        btnColumnLeft.Text = "←";
        btnColumnLeft.UseVisualStyleBackColor = true;
        btnColumnLeft.Click += btnColumnLeft_Click;
        // 
        // btnColumnRight
        // 
        btnColumnRight.Location = new Point(251, 43);
        btnColumnRight.Margin = new Padding(4, 3, 4, 3);
        btnColumnRight.Name = "btnColumnRight";
        btnColumnRight.Size = new Size(22, 27);
        btnColumnRight.TabIndex = 21;
        btnColumnRight.Text = "→";
        btnColumnRight.UseVisualStyleBackColor = true;
        btnColumnRight.Click += btnColumnRight_Click;
        // 
        // chkPswd
        // 
        chkPswd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        chkPswd.AutoSize = true;
        chkPswd.Location = new Point(317, 48);
        chkPswd.Margin = new Padding(4, 3, 4, 3);
        chkPswd.Name = "chkPswd";
        chkPswd.Size = new Size(54, 19);
        chkPswd.TabIndex = 22;
        chkPswd.Text = "Pswd";
        chkPswd.UseVisualStyleBackColor = true;
        // 
        // fontSizeComboBox
        // 
        fontSizeComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        fontSizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        fontSizeComboBox.FormattingEnabled = true;
        fontSizeComboBox.Location = new Point(379, 13);
        fontSizeComboBox.Margin = new Padding(4, 3, 4, 3);
        fontSizeComboBox.Name = "fontSizeComboBox";
        fontSizeComboBox.Size = new Size(75, 23);
        fontSizeComboBox.TabIndex = 23;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(panel1, 0, 0);
        tableLayoutPanel1.Controls.Add(btnEdit, 0, 2);
        tableLayoutPanel1.Controls.Add(lboColumns, 0, 1);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(29, 29);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 3;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.Size = new Size(523, 330);
        tableLayoutPanel1.TabIndex = 25;
        // 
        // panel1
        // 
        panel1.Controls.Add(chkMulti);
        panel1.Controls.Add(chkPswd);
        panel1.Controls.Add(btnColumnRight);
        panel1.Controls.Add(btnminus);
        panel1.Controls.Add(tbcolname);
        panel1.Controls.Add(lblAdd);
        panel1.Controls.Add(lblColumns);
        panel1.Controls.Add(btnplus);
        panel1.Controls.Add(cboList);
        panel1.Controls.Add(btnColumnLeft);
        panel1.Controls.Add(fontSizeComboBox);
        panel1.Dock = DockStyle.Fill;
        panel1.Location = new Point(3, 3);
        panel1.Name = "panel1";
        panel1.Size = new Size(517, 144);
        panel1.TabIndex = 25;
        // 
        // chkMulti
        // 
        chkMulti.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        chkMulti.AutoSize = true;
        chkMulti.Location = new Point(357, 48);
        chkMulti.Margin = new Padding(4, 3, 4, 3);
        chkMulti.Name = "chkMulti";
        chkMulti.Size = new Size(76, 19);
        chkMulti.TabIndex = 24;
        chkMulti.Text = "MultiLine";
        chkMulti.UseVisualStyleBackColor = true;
        // 
        // Edit
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        FormBorderStyle = FormBorderStyle.SizableToolWindow;
        ClientSize = new Size(581, 388);
        Controls.Add(tableLayoutPanel1);
        Margin = new Padding(4, 3, 4, 3);
        Padding = new Padding(4, 20, 4, 3);

        //MaximumSize = new Size(581, 571);
        MinimumSize = new Size(371, 317);
        Name = "Edit";
        //Padding = new Padding(29);
        Text = "Edit";
        Load += Edit_Load;
        ((ISupportInitialize)dgvColumns).EndInit();
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private DataGridViewTextBoxColumn colName;
    private DataGridViewCheckBoxColumn colMulti;
    private TableLayoutPanel tableLayoutPanel1;
    private Panel panel1;
    private CheckBox chkMulti;
}
