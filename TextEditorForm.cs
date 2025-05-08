using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace ClipBox2
{
    public partial class TextEditorForm : Form
    {
        private string cellValue;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CellValue
        {
            get { return richTextBox.Text; }
            set { richTextBox.Text = value; cellValue = value; }
        }

        private RichTextBox richTextBox;
        private Button btnUpdate;
        private TableLayoutPanel tableLayoutPanel;

        public TextEditorForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.richTextBox = new RichTextBox();
            this.btnUpdate = new Button();
            this.tableLayoutPanel = new TableLayoutPanel();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox
            this.richTextBox.AcceptsTab = true;
            this.richTextBox.Dock = DockStyle.Fill;
            this.richTextBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.richTextBox.Location = new Point(3, 3);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ScrollBars = RichTextBoxScrollBars.Both;
            this.richTextBox.Size = new Size(494, 315);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            this.richTextBox.WordWrap = true;
            this.richTextBox.MouseUp += new MouseEventHandler(this.richTextBox_MouseUp);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnUpdate.Location = new Point(422, 324);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new Size(75, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.richTextBox, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.btnUpdate, 0, 1);
            this.tableLayoutPanel.Dock = DockStyle.Fill;
            this.tableLayoutPanel.Location = new Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            this.tableLayoutPanel.Size = new Size(500, 350);
            this.tableLayoutPanel.TabIndex = 3;
            // 
            // TextEditorForm
            // 
            this.ClientSize = new Size(500, 350);
            this.Controls.Add(this.tableLayoutPanel);
            this.MinimumSize = new Size(400, 300);
            this.Name = "TextEditorForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Text Editor";
            this.TopMost = true;
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Update with the current value and close with OK result
            this.DialogResult = DialogResult.OK;
            
            // Call the Save() method in Form1 if the owner is Form1
            if (this.Owner is Form1 form1)
            {
                // Use reflection to call the private Save() method in Form1
                System.Reflection.MethodInfo saveMethod = typeof(Form1).GetMethod("Save", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                
                if (saveMethod != null)
                {
                    saveMethod.Invoke(form1, null);
                }
            }
            
            this.Close();
        }

        private void richTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            // Copy the content of the rich text box to the clipboard
            if (!string.IsNullOrEmpty(richTextBox.Text))
            {
                Clipboard.SetText(richTextBox.Text);
            }
        }
    }
}
