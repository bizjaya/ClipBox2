using System;
using System.Windows.Forms;
using System.Text;

namespace ClipBox2
{
    public partial class PasswordGeneratorForm : Form
    {
        private NumericUpDown txtLength;
        private RadioButton rdMixCase;
        private RadioButton rdUpperCase;
        private RadioButton rdLowerCase;
        private CheckBox chkUseSymbols;
        private NumericUpDown txtSymCount;
        private Button btnGenerate;
        private TextBox txtResult;

        public PasswordGeneratorForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Password Generator";
            this.Size = new System.Drawing.Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            var lblLength = new Label { Text = "Password Length:", Location = new System.Drawing.Point(20, 20) };
            txtLength = new NumericUpDown 
            { 
                Location = new System.Drawing.Point(150, 20),
                Minimum = 4,
                Maximum = 100,
                Value = 12
            };

            var grpCase = new GroupBox 
            { 
                Text = "Case Options",
                Location = new System.Drawing.Point(20, 50),
                Size = new System.Drawing.Size(200, 100)
            };

            rdMixCase = new RadioButton { Text = "Mixed Case", Location = new System.Drawing.Point(10, 20), Checked = true };
            rdUpperCase = new RadioButton { Text = "Upper Case", Location = new System.Drawing.Point(10, 45) };
            rdLowerCase = new RadioButton { Text = "Lower Case", Location = new System.Drawing.Point(10, 70) };

            grpCase.Controls.AddRange(new Control[] { rdMixCase, rdUpperCase, rdLowerCase });

            chkUseSymbols = new CheckBox 
            { 
                Text = "Use Symbols",
                Location = new System.Drawing.Point(20, 160)
            };

            var lblSymCount = new Label { Text = "Symbol Count:", Location = new System.Drawing.Point(40, 185) };
            txtSymCount = new NumericUpDown
            {
                Location = new System.Drawing.Point(150, 185),
                Minimum = 1,
                Maximum = 20,
                Value = 1,
                Enabled = false
            };

            chkUseSymbols.CheckedChanged += (s, e) => txtSymCount.Enabled = chkUseSymbols.Checked;

            btnGenerate = new Button
            {
                Text = "Generate Password",
                Location = new System.Drawing.Point(20, 220),
                Size = new System.Drawing.Size(120, 30)
            };

            txtResult = new TextBox
            {
                Location = new System.Drawing.Point(150, 220),
                Size = new System.Drawing.Size(200, 25),
                ReadOnly = true
            };

            btnGenerate.Click += BtnGenerate_Click;

            this.Controls.AddRange(new Control[] 
            { 
                lblLength, txtLength, 
                grpCase,
                chkUseSymbols, lblSymCount, txtSymCount,
                btnGenerate, txtResult
            });
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            string chars = "";
            if (rdMixCase.Checked)
                chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            else if (rdUpperCase.Checked)
                chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            else
                chars = "abcdefghijklmnopqrstuvwxyz0123456789";

            string symbols = "!@#$%^&*()_+-=[]{}|;:,.<>?";
            
            var random = new Random();
            var result = new StringBuilder();
            
            int mainLength = (int)txtLength.Value;
            if (chkUseSymbols.Checked)
                mainLength -= (int)txtSymCount.Value;

            for (int i = 0; i < mainLength; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            if (chkUseSymbols.Checked)
            {
                for (int i = 0; i < txtSymCount.Value; i++)
                {
                    int pos = random.Next(result.Length + 1);
                    result.Insert(pos, symbols[random.Next(symbols.Length)]);
                }
            }

            txtResult.Text = result.ToString();
        }
    }
}
