using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Models.Converter
{
    internal abstract class CodeConverter {

        private string[] sourceCode;

        private int lineIndex = -1;     // Cursor's index in sourceCode(field) while converting
        private int chIndex;            // Cursor's index in the currently processing line

        private protected string indentation;
        private protected string indentationBlock;

        private protected int parenthesesDepth = 0;
        private protected List<string> temp = new List<string>() { String.Empty };      // Indexed by parenthesesDepth, the List<T> temporarily retains conversion results.

        /// <summary>
        /// Conversion result(target code).
        /// </summary>
        public string Result { get; private protected set; } = String.Empty;

        private protected CodeConverter(string[] sourceCode) {
            this.sourceCode = sourceCode.Select(line => $"{line} ").ToArray();
        }

        /// <summary>
        /// Starts the conversion.
        /// </summary>
        public void Start() {
            jump();

            do
            {
                processLine();

                Result += temp[0];
                temp[0] = String.Empty;

                jump();
            } while (lineIndex <= sourceCode.Length - 1);

            organizeResult();
        }

        private protected virtual bool convertFunction() {
            do
            {
                processLine();

                jump();
            } while (lineIndex <= sourceCode.Length - 1 && indentation == indentationBlock);

            temp[0] = temp[0].TrimEnd();
            temp[0] += Environment.NewLine + '}' + Environment.NewLine;

            lineIndex--;

            return true;
        }

        private protected abstract bool convertForLoop();

        private protected abstract bool convertConditional();

        private protected abstract bool convertReturn();

        private protected abstract bool convertWhileLoop();

        private protected void processLine() {
            while (!proceed()) { }
            if (0 < parenthesesDepth) {
                // TODO: Throw exception
            }

            if (temp[0][temp[0].Length - 1] == ' ') {
                temp[0] = $"{temp[0].TrimEnd()};";
            }
        }

        private protected void jump() {
            lineIndex++;
            while (lineIndex <= sourceCode.Length - 1 && sourceCode[lineIndex] == " ")
            {
                lineIndex++;
            }

            if (lineIndex <= sourceCode.Length - 1) {
                chIndex = 0;
                indentation = String.Empty;
                while (chIndex <= sourceCode[lineIndex].Length - 1 && sourceCode[lineIndex][chIndex] == ' ')
                {
                    chIndex++;
                    indentation += ' ';
                }
                temp[0] += Environment.NewLine + indentation;
            }
        }

        private protected abstract void organizeResult();

        private bool proceed() {
            var word = readNext();

            switch (word) {
                case "":
                case "import":
                case "pass":
                    return true;
                case "(":
                    if (++parenthesesDepth == temp.Count) {
                        temp.Add(String.Empty);
                    }
                    temp[parenthesesDepth - 1] += "(";
                    return false;
                case ")":
                    if (--parenthesesDepth < 0) {
                        // TODO: Throw exception
                    }
                    temp[parenthesesDepth] += $"{temp[parenthesesDepth + 1].TrimEnd()}) ";
                    temp[parenthesesDepth + 1] = String.Empty;
                    return false;
                case "[":
                case "]":
                    return false;       // TODO: Implement cases
                case "and":
                    temp[parenthesesDepth] += "&& ";
                    return false;
                case "or":
                    temp[parenthesesDepth] += "|| ";
                    return false;
                case "not":
                    temp[parenthesesDepth] += "!";
                    return false;
                case "def":
                    return convertFunction();
                case "for":
                    return convertForLoop();
                case "if":
                    return convertConditional();
                case "return":
                    return convertReturn();
                case "while":
                    return convertWhileLoop();
                case ", ":
                case ".":
                default:
                    temp[parenthesesDepth] += word;
                    return false;
            }
        }

        private string readNext() {
            while (chIndex <= sourceCode[lineIndex].Length - 1 && sourceCode[lineIndex][chIndex] == ' ')
            {
                chIndex++;
            }

            string toRead = sourceCode[lineIndex].Substring(chIndex);

            if (toRead == String.Empty) {
                return "";
            } else if (toRead[0] == '(' || toRead[0] == ')' || toRead[0] == '[' || toRead[0] == ']' || toRead[0] == '.') {
                chIndex++;

                return toRead[0].ToString();
            } else if (toRead[0] == ',') {
                chIndex++;

                return ", ";
            } else if (toRead.StartsWith("and ")) {
                chIndex += 3;

                return "and";
            } else if (toRead.StartsWith("or ")) {
                chIndex += 2;

                return "or";
            } else if (toRead.StartsWith("not ")) {
                chIndex += 3;

                return "not";
            } else if (toRead.StartsWith("import ")) {
                chIndex += 6;

                return "import";
            } else if (toRead.StartsWith("def ")) {
                chIndex += 3;

                return "def";
            } else if (toRead.StartsWith("for ")) {
                chIndex += 3;

                return "for";
            } else if (toRead.StartsWith("if ")) {
                chIndex += 2;

                return "if";
            } else if (toRead.StartsWith("pass")) {
                chIndex += 4;

                return "pass";
            } else if (toRead.StartsWith("return ")) {
                chIndex += 6;

                return "return";
            } else if (toRead.StartsWith("while ")) {
                chIndex += 5;

                return "while";
            } else {
                int length = toRead.IndexOfAny(new char[] { '(', ')', '[', ']', ',', '.', ' ' });
                chIndex += length;

                return toRead.Substring(0, length) + ((toRead[length] != ' ') ? String.Empty : " ");
            }
        }
    }
}