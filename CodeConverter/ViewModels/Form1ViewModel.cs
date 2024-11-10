using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.ViewModels
{
    using CodeConverter.Models.Converter;

    internal sealed class Form1ViewModel {

        internal string[] SourceCode { get; set; } = Array.Empty<string>();
        internal string TargetCode { get; private set; }

        internal bool isSuccessful { get; private set; }
        internal string ValidationErrorMessage { get; private set; }

        internal bool ValidateSourceCode() {
            if (isSourceCodeEmpty()) {
                ValidationErrorMessage = "빈 코드입니다..";

                return false;
            } else if (isFirstLineIndented()) {
                ValidationErrorMessage = "첫 번째 줄에 해당하는 코드의 들여쓰기는 허용하지 않습니다.";

                return false;
            } else if (hasViolatedIndentationRule()) {
                ValidationErrorMessage = "들여쓰기 규칙을 위반한 코드입니다. 변환하고자 하는 코드의" + Environment.NewLine +
                "들여쓰기 단위를 4개의 공백씩으로 맞춰주세요.";

                return false;
            } else if (isSemicolonTyped()) {
                ValidationErrorMessage = "파이썬 코드에서는 세미콜론(;)을 사용하실 수 없습니다.";

                return false;
            }

            return true;
        }

        internal void ParseWith(CodeConverter converter) {
            isSuccessful = false;

            converter.Start();
            TargetCode = converter.Result;
            isSuccessful = true;
        }

        private bool isSourceCodeEmpty() {
            return !Array.Exists(SourceCode, line => line != String.Empty);
        }

        private bool isFirstLineIndented() {
            return Array.Find(SourceCode, line => line != String.Empty)[0] == ' ';
        }

        private bool hasViolatedIndentationRule() {
            foreach (string line in SourceCode) {
                var result = Array.FindIndex(line.ToCharArray(), ch => ch != ' ');
                if (result != -1 && result % 4 != 0) {
                    return true;
                }
            }

            return false;
        }

        private bool isSemicolonTyped() {
            return Array.Exists(SourceCode, line => line.Contains(';'));
        }
    }
}
