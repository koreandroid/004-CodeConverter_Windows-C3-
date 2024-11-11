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

        internal string ValidationErrorMessage { get; private set; }

        private int errorLineIndex;
        private int errorChIndex;

        internal bool ValidateSourceCode() {
            if (isSourceCodeEmpty()) {
                ValidationErrorMessage = "빈 코드입니다..";

                return false;
            } else if (isFirstLineIndented()) {
                ValidationErrorMessage = "첫 번째 줄에 해당하는 코드의 들여쓰기는 허용하지 않습니다.";

                return false;
            } else if (hasViolatedIndentationRule()) {
                ValidationErrorMessage = "들여쓰기 규칙을 위반한 코드입니다. 변환하고자 하는 코드의 들여쓰기 단위를 4개의 공백씩으로 맞춰주세요.";

                return false;
            } else if (isSemicolonTyped()) {
                ValidationErrorMessage = "파이썬 코드에서는 세미콜론(;)을 사용하실 수 없습니다.";

                return false;
            } else if (doesCompoundAssignmentLackSpace()) {
                ValidationErrorMessage = "+=, -=, *=, /=, //=, %=, &=, |=, ^=, 그리고 **= 등의 복합 대입 구문은 공백으로써 좌변과 구분되어야 합니다.";

                return false;
            }

            return true;
        }

        internal void ParseWith(CodeConverter converter) {
            try {
                converter.Start();
            }
            finally {
                TargetCode = converter.Result;
            }
        }

        internal string GetFullErrorMessage(int type) {
            return $"[Line {errorLineIndex + 1}, Ch {errorChIndex + 1}] {(type == 0 ? ValidationErrorMessage : String.Empty)}";
        }

        private bool isSourceCodeEmpty() {
            int index;
            if (!Array.Exists(SourceCode, line => !(String.IsNullOrWhiteSpace(line.Substring(0, (index = line.IndexOf('#')) != -1 ? index : line.Length))))) {
                errorLineIndex = 0;
                errorChIndex = -1;
                return true;
            }
            return false;
        }

        private bool isFirstLineIndented() {
            var index = Array.FindIndex(SourceCode, line => line != String.Empty);
            if (SourceCode[index][0] == ' ') {
                errorLineIndex = index;
                errorChIndex = 0;
                return true;
            }
            return false;
        }

        private bool hasViolatedIndentationRule() {
            for (var idx = 0; idx < SourceCode.Length; idx++) {
                var result = Array.FindIndex(SourceCode[idx].ToCharArray(), ch => ch != ' ');
                if (result != -1 && result % 4 != 0) {
                    errorLineIndex = idx;
                    errorChIndex = result;

                    return true;
                }
            }

            return false;
        }

        private bool isSemicolonTyped() {
            for (var idx = 0; idx < SourceCode.Length; idx++) {
                if (SourceCode[idx].LastOrDefault() == ';') {
                    errorLineIndex = idx;
                    errorChIndex = SourceCode[idx].Length - 1;

                    return true;
                }
            }

            return false;
        }

        private bool doesCompoundAssignmentLackSpace() {
            string[] delimiterList = new string[] { "+=", "-=", "*=", "/=", "//=", "%=", "&=", "|=", "^=", "**=" };

            for (var idx = 0; idx < SourceCode.Length; idx++) {
                foreach (var delimiter in delimiterList) {
                    var ch = SourceCode[idx].ElementAtOrDefault(SourceCode[idx].IndexOf(delimiter) - 1);
                    if (ch != '\0' && ch != ' ') {
                        errorLineIndex = idx;
                        errorChIndex = SourceCode[idx].IndexOf(delimiter);

                        return true;
                    }
                }
            }

            return false;
        }
    }
}