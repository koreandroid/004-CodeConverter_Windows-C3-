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
    public partial class ErrorDialogForm : Form {

        public ErrorDialogForm(string title, string errorMessage) {
            InitializeComponent();

            this.Text = title;
            setErrorMessage(errorMessage);
        }

        private void setErrorMessage(string errorMessage) {
            lblErrorMessage.Text = String.Empty;

            string toShow = errorMessage.Replace("&", "&&");
            while (32 < toShow.Length)
            {
                lblErrorMessage.Text += toShow.Substring(0, 32) + Environment.NewLine;
                toShow = toShow.Substring(32).TrimStart();
            }
            lblErrorMessage.Text += toShow;
        }
    }
}