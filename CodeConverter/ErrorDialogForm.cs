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

        public ErrorDialogForm(string errorMessage) {
            InitializeComponent();

            lblErrorMessage.Text = errorMessage;
        }
    }
}
