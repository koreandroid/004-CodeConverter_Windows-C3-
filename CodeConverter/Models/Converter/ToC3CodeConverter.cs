﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Models.Converter
{
    internal class ToC3CodeConverter : CodeConverter {

        public ToC3CodeConverter(string[] sourceCode) : base(sourceCode) {
            declarationKeyword = "var";
        }

        private protected override bool convertFunction() {
            temp[parenthesesDepth] += "private object ";

            processLine();

            if (++blockDepth == identifiers.Count) {
                identifiers.Add(new List<string>());
            }
            string[] idList = temp[0].Split(',');
            idList[0] = idList[0].Substring(idList[0].IndexOf('(') + 1);
            idList = idList.Select(id => id.Trim()).ToArray();
            idList[idList.Length - 1] = idList[idList.Length - 1].Substring(0, idList[idList.Length - 1].LastIndexOf(')'));
            foreach (string id in idList) {
                identifiers[blockDepth].Add(id);
            }

            temp[0] = temp[0].Substring(0, temp[0].Length - 3);
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

        private protected override void organizeResult() {
            Result = "using System;" + Environment.NewLine +
            "using System.Collections.Generic;" + Environment.NewLine +
            Environment.NewLine +
            "namespace Result" + Environment.NewLine +
            '{' + Environment.NewLine +
            "    public class Program {" + Environment.NewLine +
            Environment.NewLine +
            "        static void Main(string[] args) {" + Environment.NewLine +
            String.Join(Environment.NewLine, Result.Trim().Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Select(code => "            " + code)) + Environment.NewLine +
            "        }" + Environment.NewLine +
            "    }" + Environment.NewLine +
            '}';
        }
    }
}