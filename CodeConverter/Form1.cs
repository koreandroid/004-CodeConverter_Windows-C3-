using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeConverter.Models.Converter;
using CodeConverter.ViewModels;

namespace CodeConverter
{
    public partial class Form1 : Form {

        private readonly Form1ViewModel viewModel = new Form1ViewModel();

        public Form1() {
            InitializeComponent();
        }

        private void txtSourceCode_TextChanged(object sender, EventArgs e) {
            viewModel.SourceCode = txtSourceCode.Lines.Select(line => line.TrimEnd()).ToArray();
        }

        private void btnConvert_Click(object sender, EventArgs e) {
            if (!viewModel.ValidateSourceCode()) {
                new ErrorDialogForm(viewModel.ValidationErrorMessage).ShowDialog();
                return;
            }

            viewModel.ParseWith(new ToC3CodeConverter(viewModel.SourceCode));
        }
    }
}