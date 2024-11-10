using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeConverter
{
    public partial class Form2 : Form {

        public Form2(string targetLang, string sourceCode, string targetCode) {
            InitializeComponent();

            this.Text = $"To {targetLang}";
            lblTargetCode.Text = $"결과 코드 ({targetLang})";
            txtSourceCode.Text = sourceCode;
            txtTargetCode.Text = targetCode;
        }

        private void Form2_Load(object sender, EventArgs e) {
            txtSourceCode.Visible = true;
            txtTargetCode.Visible = true;

            this.WindowState = FormWindowState.Maximized;
        }

        private void Form2_Resize(object sender, EventArgs e) {
            txtSourceCode.Width = this.Width / 2 - 72;

            int originalWidth = txtTargetCode.Width;
            txtTargetCode.Width = this.Width / 2 - 72;
            txtTargetCode.Location = new Point(txtTargetCode.Location.X - (txtTargetCode.Width - originalWidth), txtTargetCode.Location.Y);

            lblTargetCode.Location = new Point(txtTargetCode.Location.X, lblTargetCode.Location.Y);
        }
    }
}