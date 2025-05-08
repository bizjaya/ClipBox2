using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClipBox2;

partial class Edit : MaterialSkin.Controls.MaterialForm
{
    private IContainer components = null;
    private Button btnMinus;
    private Button btnPlus;
    private Button btnEdit;
    private DataGridView dgvColumns; // DataGridView for columns
    private TextBox tbxListName; // TextBox for Add mode
    private ComboBox cbxListName;
    private Button btnLeft;
    private Button btnRight;
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
        btnMinus = new Button();
        btnPlus = new Button();
        btnEdit = new Button();
        dgvColumns = new DataGridView();
        colName = new DataGridViewTextBoxColumn();
        colPswd = new DataGridViewCheckBoxColumn();
        colMulti = new DataGridViewCheckBoxColumn();
        tbxListName = new TextBox();
        cbxListName = new ComboBox();
        btnLeft = new Button();
        btnRight = new Button();
        fontSizeComboBox = new ComboBox();
        tableLayoutPanel1 = new TableLayoutPanel();
        panel1 = new Panel();
        dataGridView1 = new DataGridView();
        lblListName = new Label();
        ((ISupportInitialize)dgvColumns).BeginInit();
        tableLayoutPanel1.SuspendLayout();
        panel1.SuspendLayout();
        ((ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // btnMinus
        // 
        btnMinus.Location = new Point(83, 42);
        btnMinus.Margin = new Padding(4, 3, 4, 3);
        btnMinus.Name = "btnMinus";
        btnMinus.Size = new Size(48, 27);
        btnMinus.TabIndex = 14;
        btnMinus.Text = "Del➖";
        btnMinus.UseVisualStyleBackColor = true;
        // 
        // btnPlus
        // 
        btnPlus.Location = new Point(29, 42);
        btnPlus.Margin = new Padding(4, 3, 4, 3);
        btnPlus.Name = "btnPlus";
        btnPlus.Size = new Size(51, 27);
        btnPlus.TabIndex = 13;
        btnPlus.Text = "Add➕";
        btnPlus.UseVisualStyleBackColor = true;
        // 
        // btnEdit
        // 
        btnEdit.AutoSize = true;
        btnEdit.Location = new Point(305, 44);
        btnEdit.Margin = new Padding(4, 3, 4, 3);
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(72, 25);
        btnEdit.TabIndex = 12;
        btnEdit.Text = "Edit";
        btnEdit.UseVisualStyleBackColor = true;
        btnEdit.Click += btnEdit_Click;
        // 
        // dgvColumns
        // 
        dgvColumns.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvColumns.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvColumns.Columns.AddRange(new DataGridViewColumn[] { colName, colPswd, colMulti });
        dgvColumns.Location = new Point(3, 90);
        dgvColumns.Name = "dgvColumns";
        dgvColumns.Size = new Size(443, 469);
        dgvColumns.TabIndex = 100;
        // 
        // colName
        // 
        colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colName.HeaderText = "Column Name";
        colName.Name = "colName";
        // 
        // colPswd
        // 
        colPswd.HeaderText = "Pswd";
        colPswd.Name = "colPswd";
        colPswd.Width = 50;
        // 
        // colMulti
        // 
        colMulti.HeaderText = "Multi";
        colMulti.Name = "colMulti";
        colMulti.Width = 50;
        // 
        // tbxListName
        // 
        tbxListName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        tbxListName.Location = new Point(199, 15);
        tbxListName.Name = "tbxListName";
        tbxListName.Size = new Size(194, 23);
        tbxListName.TabIndex = 101;
        tbxListName.Visible = true;
        // 
        // cbxListName
        // 
        cbxListName.FormattingEnabled = true;
        cbxListName.Location = new Point(29, 15);
        cbxListName.Margin = new Padding(4, 3, 4, 3);
        cbxListName.Name = "cbxListName";
        cbxListName.Size = new Size(162, 23);
        cbxListName.TabIndex = 16;
        // 
        // btnLeft
        // 
        btnLeft.Location = new Point(136, 42);
        btnLeft.Margin = new Padding(4, 3, 4, 3);
        btnLeft.Name = "btnLeft";
        btnLeft.Size = new Size(26, 27);
        btnLeft.TabIndex = 20;
        btnLeft.Text = "◀";
        btnLeft.UseVisualStyleBackColor = true;
        // 
        // btnRight
        // 
        btnRight.Location = new Point(166, 42);
        btnRight.Margin = new Padding(4, 3, 4, 3);
        btnRight.Name = "btnRight";
        btnRight.Size = new Size(25, 27);
        btnRight.TabIndex = 21;
        btnRight.Text = "▶";
        btnRight.UseVisualStyleBackColor = true;
        // 
        // fontSizeComboBox
        // 
        fontSizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        fontSizeComboBox.FormattingEnabled = true;
        fontSizeComboBox.Location = new Point(199, 44);
        fontSizeComboBox.Margin = new Padding(4, 3, 4, 3);
        fontSizeComboBox.Name = "fontSizeComboBox";
        fontSizeComboBox.Size = new Size(98, 23);
        fontSizeComboBox.TabIndex = 23;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(panel1, 0, 0);
        tableLayoutPanel1.Controls.Add(dgvColumns, 0, 1);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(4, 20);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 87F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(449, 562);
        tableLayoutPanel1.TabIndex = 25;
        // 
        // panel1
        // 
        panel1.Controls.Add(lblListName);
        panel1.Controls.Add(tbxListName);
        panel1.Controls.Add(btnEdit);
        panel1.Controls.Add(btnRight);
        panel1.Controls.Add(btnMinus);
        panel1.Controls.Add(btnPlus);
        panel1.Controls.Add(cbxListName);
        panel1.Controls.Add(btnLeft);
        panel1.Controls.Add(fontSizeComboBox);
        panel1.Dock = DockStyle.Fill;
        panel1.Location = new Point(3, 3);
        panel1.Name = "panel1";
        panel1.Size = new Size(443, 81);
        panel1.TabIndex = 25;
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Dock = DockStyle.Fill;
        dataGridView1.Location = new Point(3, 118);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.Size = new Size(615, 330);
        dataGridView1.TabIndex = 26;
        // 
        // label1
        // 
        lblListName.AutoSize = true;
        lblListName.Location = new Point(153, 18);
        lblListName.Name = "lblListName";
        lblListName.Size = new Size(38, 15);
        lblListName.TabIndex = 102;
        lblListName.Text = "List Name";
        // 
        // Edit
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(457, 585);
        Controls.Add(tableLayoutPanel1);
        FormBorderStyle = FormBorderStyle.SizableToolWindow;
        Margin = new Padding(4, 3, 4, 3);
        MinimumSize = new Size(371, 317);
        Name = "Edit";
        Padding = new Padding(4, 20, 4, 3);
        Text = "Edit";
        Load += Edit_Load;
        ((ISupportInitialize)dgvColumns).EndInit();
        tableLayoutPanel1.ResumeLayout(false);
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ((ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private DataGridViewTextBoxColumn colName;
    private DataGridViewCheckBoxColumn colPswd;
    private DataGridViewCheckBoxColumn colMulti;
    private TableLayoutPanel tableLayoutPanel1;
    private Panel panel1;
    private DataGridView dataGridView1;
    private Label lblListName;
}
