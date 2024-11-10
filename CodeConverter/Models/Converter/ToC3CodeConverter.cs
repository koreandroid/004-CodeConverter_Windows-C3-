using System;
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
            temp[parenthesesDepth] += "object ";

            if (++blockDepth == identifiers.Count) {
                identifiers.Add(new List<string>());
            }
            processLine();

            var code = temp[0].Substring(temp[0].LastIndexOf("object "));
            if (code.Contains(',')) {
                string[] pIdList = code.Split(',').Select(p => p.Split('=')[0]).ToArray();
                pIdList[0] = pIdList[0].Substring(pIdList[0].IndexOf('(') + 1);
                pIdList = pIdList.Select(id => id.Trim()).ToArray();
                pIdList[pIdList.Length - 1] = pIdList[pIdList.Length - 1].Substring(0, pIdList[pIdList.Length - 1].LastIndexOf(')')).TrimEnd();
                foreach (string pId in pIdList) {
                    identifiers[blockDepth].Add(pId);
                }
            }

            temp[0] = temp[0].Trim(new char[] { ' ', ':', ';' }) + " {";

            convertBlock(() => {
                if (!(identifiers[blockDepth].Contains("return"))) {
                    temp[0] += Environment.NewLine +
                    Environment.NewLine +
                    indentationBlock + "return 0;";
                }
            });

            return true;
        }

        private protected override bool convertForLoop() {
            string result = null;

            if (++blockDepth == identifiers.Count) {
                identifiers.Add(new List<string>());
            }
            processLine();

            string[] codeSplit = temp[0].Split(new string[] { " in " }, StringSplitOptions.None);
            string id = codeSplit[0].Substring(codeSplit[0].LastIndexOf($"{declarationKeyword} ") + declarationKeyword.Length + 1).TrimEnd();
            codeSplit[1] = codeSplit[1].TrimStart();

            if (codeSplit[1].StartsWith("range(") || codeSplit[1].StartsWith("range ")) {
                string[] numList = codeSplit[1].Split(',');
                numList[0] = numList[0].Substring(numList[0].IndexOf('(') + 1);
                numList = numList.Select(num => num.Trim()).ToArray();
                numList[numList.Length - 1] = numList[numList.Length - 1].Substring(0, numList[numList.Length - 1].LastIndexOf(')')).TrimEnd();

                if (numList.Length == 1) {
                    result = $"for (var {id} = 0; {id} < {numList[0]}; {id}++) {{";
                } else if (numList.Length == 2) {
                    result = $"for (var {id} = {numList[0]}; {id} < {numList[1]}; {id}++) {{";
                } else if (numList.Length == 3) {
                    result = $"for (var {id} = {numList[0]}; {id} < {numList[1]}; {id} += {numList[2]}) {{";
                } else {
                    // TODO: Throw exception
                }
            } else {
                result = $"foreach (var {id} in {codeSplit[1].Trim(new char[] { ' ', ':', ';' })}) {{";
            }

            string[] lineList = temp[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            lineList[lineList.Length - 1] = lineList[lineList.Length - 1].Substring(0, Array.FindIndex(lineList[lineList.Length - 1].ToCharArray(), ch => ch != ' ')) + (result ?? String.Empty);

            temp[0] = String.Join(Environment.NewLine, lineList);

            convertBlock();

            return true;
        }

        private protected override bool convertIfStatement() {
            temp[parenthesesDepth] += "if (";

            if (++blockDepth == identifiers.Count) {
                identifiers.Add(new List<string>());
            }
            processLine();

            temp[0] = $"{temp[0].Substring(0, temp[0].LastIndexOf(':')).TrimEnd()}) {{";

            convertBlock();

            return true;
        }

        private protected override bool convertElifStatement() {
            Result = Result.TrimEnd();
            temp[parenthesesDepth] = $"{temp[parenthesesDepth].TrimEnd()} else if (";

            if (++blockDepth == identifiers.Count) {
                identifiers.Add(new List<string>());
            }
            processLine();

            temp[0] = $"{temp[0].Substring(0, temp[0].LastIndexOf(':')).TrimEnd()}) {{";

            convertBlock();

            return true;
        }

        private protected override bool convertElseStatement() {
            Result = Result.TrimEnd();
            temp[parenthesesDepth] = $"{temp[parenthesesDepth].TrimEnd()} else {{";

            if (++blockDepth == identifiers.Count) {
                identifiers.Add(new List<string>());
            }
            convertBlock();

            return true;
        }

        private protected override bool convertReturn() {
            temp[parenthesesDepth] += "return ";
            identifiers[blockDepth].Add("return");

            return false;
        }

        private protected override bool convertWhileLoop() {
            temp[parenthesesDepth] += "while (";

            if (++blockDepth == identifiers.Count) {
                identifiers.Add(new List<string>());
            }
            processLine();

            temp[0] = temp[0].TrimEnd(new char[] { ' ', ':', ';' }) + ')' + Environment.NewLine +
            $"{indentation}{{";

            convertBlock();

            return true;
        }

        private protected override void organizeResult() {
            // ' to "
            Result = Result.Replace('\u0027', '\u0022');

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