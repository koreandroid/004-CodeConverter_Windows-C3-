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
                new ErrorDialogForm("1단계 오류(유효성 검사)", viewModel.ValidationErrorMessage).ShowDialog();
                addHistory(viewModel.ValidationErrorMessage);

                return;
            }

            try {
                viewModel.ParseWith(new ToC3CodeConverter(viewModel.SourceCode));
                addHistory("C# 코드로의 변환에 성공하였습니다.");

                new Form2("C#", txtSourceCode.Text, viewModel.TargetCode).Show();
            } catch (Exception) {
                //new ErrorDialogForm("2단계 오류(구문 분석)", viewModel.ParsingErrorMessage).ShowDialog();
                //addHistory(viewModel.ParsingErrorMessage);
            }
        }

        private void addHistory(string message) {
            txtHistory.AppendText((String.IsNullOrEmpty(txtHistory.Text) ? String.Empty : Environment.NewLine ) + message);
        }
    }
}