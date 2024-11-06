using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Models.Converter
{
    internal class ToC3CodeConverter : CodeConverter {

        public ToC3CodeConverter(string[] sourceCode) : base(sourceCode) {}

        private protected override bool convertFunction() {
            methodList.Add("private object ");

            processLine();

            temp[0] = temp[0].Substring(0, temp[0].Length - 1);
            temp[0] += " {";

            indentationBlock = indentation;
            jump();
            
            if (indentation != indentationBlock + "    ") {
                // TODO: Throw exception
            }
            indentationBlock = indentation;

            return base.convertFunction();
        }

        private protected override bool convertForLoop() {
            return false;       // TODO: Implement the method
        }

        private protected override bool convertConditional() {
            return false;       // TODO: Implement the method
        }

        private protected override bool convertReturn() {
            return false;       // TODO: Implement the method
        }

        private protected override bool convertWhileLoop() {
            return false;       // TODO: Implement the method
        }
    }
}
