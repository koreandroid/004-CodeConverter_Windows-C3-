using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.ViewModels
{
    internal sealed class Form1ViewModel {

        internal string[] SourceCode { get; set; }

        internal string ValidationErrorMessage { get; private set; }

        internal bool ValidateSourceCode() {
            if (isSourceCodeEmpty()) {
                ValidationErrorMessage = "빈 코드입니다..";

                return false;
            } else if (hasViolatedIndentationRule()) {
                ValidationErrorMessage = "들여쓰기 규칙을 위반한 코드입니다. 변환하고자 하는 코드의" + Environment.NewLine +
                "들여쓰기 단위를 4개의 공백씩으로 맞춰주세요.";

                return false;
            }

            return true;
        }

        private bool isSourceCodeEmpty() {
            foreach (var code in SourceCode) {
                if (!String.IsNullOrEmpty(code)) {
                    return false;
                }
            }

            return true;
        }

        private bool hasViolatedIndentationRule() {
            foreach (var code in SourceCode) {
                int idx = 0;
                while (idx <= code.Length - 1 && code[idx] == ' ')
                {
                    idx++;
                }

                if (idx % 4 != 0) {
                    return true;
                }
            }

            return false;
        }
    }
}
