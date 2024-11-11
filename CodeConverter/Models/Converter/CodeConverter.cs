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

        /// <summary>
        /// This field represents the depth of curly bracket blocks.
        /// </summary>
        private protected int blockDepth = 0;
        private protected List<List<string>> identifiers = new List<List<string>>() { new List<string>() };

        private protected int parenthesesDepth = 0;
        private protected List<string> temp = new List<string>() { String.Empty };      // Indexed by parenthesesDepth, the List<T> temporarily retains conversion results.

        private protected string declarationKeyword;
        private protected string listInitializationExpr;
        private protected string fromAppendTo;
        private protected string fromClearTo;
        private protected string fromExtendTo;
        private protected string fromInsertTo;
        private protected string fromIndexTo;
        private protected string fromPopTo;
        private protected string fromPrintTo;
        private protected string fromRemoveTo;
        private protected string fromReverseTo;
        private protected string fromSortTo;

        /// <summary>
        /// Conversion result(target code).
        /// </summary>
        public string Result { get; private protected set; } = String.Empty;

        private protected delegate void SupplementalsCallback();

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

        private protected abstract bool convertFunction();

        private protected abstract bool convertForLoop();

        private protected abstract bool convertIfStatement();

        private protected abstract bool convertElifStatement();

        private protected abstract bool convertElseStatement();

        private protected abstract bool convertReturn();

        private protected abstract bool convertWhileLoop();

        private protected bool convertBlock(SupplementalsCallback supplementalsCallback = null) {
            indentationBlock = indentation;
            jump();
            if (indentation != indentationBlock + "    ") {
                // TODO: Throw exception
            }

            indentationBlock = indentation;

            do
            {
                processLine();

                jump();
            } while (lineIndex <= sourceCode.Length - 1 && indentation == indentationBlock);

            temp[0] = temp[0].TrimEnd();
            if (supplementalsCallback != null) {
                supplementalsCallback();
            }

            indentationBlock = indentationBlock.Substring(4);
            temp[0] += Environment.NewLine + $"{indentationBlock}}}" + Environment.NewLine;

            identifiers[blockDepth--].Clear();

            lineIndex--;

            return true;
        }

        private protected void processLine() {
            while (!proceed()) { }
            if (0 < parenthesesDepth) {
                // TODO: Throw exception
            }

            if (!(temp[0].TrimEnd(' ').EndsWith(Environment.NewLine))) {
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
                    temp[parenthesesDepth - 1] += '(';
                    return false;
                case ")":
                case ") ":
                    if (--parenthesesDepth < 0) {
                        // TODO: Throw exception
                    }
                    temp[parenthesesDepth] += temp[parenthesesDepth + 1].TrimEnd() + word;
                    temp[parenthesesDepth + 1] = String.Empty;
                    return false;
                case "[":
                    if (++parenthesesDepth == temp.Count) {
                        temp.Add(String.Empty);
                    }
                    if (!(String.IsNullOrEmpty(temp[parenthesesDepth - 1])) &&
                        !(new char[] { ':', ' ', '=' }.Contains(temp[parenthesesDepth - 1][temp[parenthesesDepth - 1].Length - 1]))) {
                        temp[parenthesesDepth - 1] += '[';
                    } else {
                        temp[parenthesesDepth - 1] += listInitializationExpr;
                    }
                    return false;
                case "]":
                case "] ":
                    if (--parenthesesDepth < 0) {
                        // TODO: Throw exception
                    }
                    temp[parenthesesDepth] += temp[parenthesesDepth + 1].TrimEnd() +
                    (temp[parenthesesDepth][temp[parenthesesDepth].Length - 1] == ' ' ? " " : String.Empty) + (char)(temp[parenthesesDepth][Array.FindLastIndex(temp[parenthesesDepth].ToCharArray(), ch => ch != ' ')] + 2) + word.Substring(1);
                    temp[parenthesesDepth + 1] = String.Empty;
                    return false;
                case ", ":
                case ".":
                    temp[parenthesesDepth] = temp[parenthesesDepth].TrimEnd();
                    temp[parenthesesDepth] += word;
                    return false;
                case ":":
                case ": ":
                    temp[parenthesesDepth] += word;
                    return false;
                case "True":
                case "True ":
                case "False":
                case "False ":
                    temp[parenthesesDepth] += word.ToLower();
                    return false;
                case "and":
                    temp[parenthesesDepth] += "&& ";
                    return false;
                case "or":
                    temp[parenthesesDepth] += "|| ";
                    return false;
                case "not":
                    temp[parenthesesDepth] += "!";
                    return false;
                case "break":
                case "continue":
                    temp[parenthesesDepth] += word;
                    return true;
                case "def":
                    return convertFunction();
                case "for":
                    return convertForLoop();
                case "if":
                    return convertIfStatement();
                case "elif":
                    return convertElifStatement();
                case "else":
                    return convertElseStatement();
                case "return":
                    return convertReturn();
                case "while":
                    return convertWhileLoop();
                case "append":
                case "append ":
                    temp[parenthesesDepth] += fromAppendTo;
                    return false;
                case "clear":
                case "clear ":
                    temp[parenthesesDepth] += fromClearTo;
                    return false;
                case "extend":
                case "extend ":
                    temp[parenthesesDepth] += fromExtendTo;
                    return false;
                case "index":
                case "index ":
                    temp[parenthesesDepth] += fromIndexTo;
                    return false;
                case "insert":
                case "insert ":
                    temp[parenthesesDepth] += fromInsertTo;
                    return false;
                case "pop":
                case "pop ":
                    temp[parenthesesDepth] += fromPopTo;
                    return false;
                case "print":
                case "print ":
                    temp[parenthesesDepth] += fromPrintTo;
                    return false;
                case "remove":
                case "remove ":
                    temp[parenthesesDepth] += fromRemoveTo;
                    return false;
                case "reverse":
                case "reverse ":
                    temp[parenthesesDepth] += fromReverseTo;
                    return false;
                case "sort":
                case "sort ":
                    temp[parenthesesDepth] += fromSortTo;
                    return false;
                default:
                    string[] lineList = temp[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    string id = word.TrimEnd();
                    if (String.IsNullOrWhiteSpace(lineList[lineList.Length - 1]) && sourceCode[lineIndex][chIndex] != '(' &&
                        identifiers.Find(idList => idList.Contains(id)) == null) {
                        identifiers[blockDepth].Add(id);
                        temp[parenthesesDepth] += $"{declarationKeyword} ";
                    }
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
            } else if (toRead[0] == '(' || toRead[0] == '[' || toRead[0] == '.') {
                chIndex++;

                return toRead[0].ToString();
            } else if (toRead[0] == ')' || toRead[0] == ']' || toRead[0] == ':') {
                chIndex++;

                return toRead[0] + (toRead[1] != ' ' ? String.Empty : " ");
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
            } else if (toRead.StartsWith("break ")) {
                chIndex += 5;

                return "break";
            } else if (toRead.StartsWith("continue ")) {
                chIndex += 8;

                return "continue";
            } else if (toRead.StartsWith("import ")) {
                chIndex += 6;

                return "import";
            } else if (toRead.StartsWith("pass ")) {
                chIndex += 4;

                return "pass";
            } else if (toRead.StartsWith("def ")) {
                chIndex += 3;

                return "def";
            } else if (toRead.StartsWith("for ")) {
                chIndex += 3;

                return "for";
            } else if (toRead.StartsWith("if ")) {
                chIndex += 2;

                return "if";
            } else if (toRead.StartsWith("elif ")) {
                chIndex += 4;

                return "elif";
            } else if (toRead.StartsWith("else:") || toRead.StartsWith("else ")) {
                chIndex += 4;

                return "else";
            } else if (toRead.StartsWith("return ")) {
                chIndex += 6;

                return "return";
            } else if (toRead.StartsWith("while ")) {
                chIndex += 5;

                return "while";
            } else {
                int length = toRead.IndexOfAny(new char[] { '(', ')', '[', ']', ',', '.', ':', ' ', '=' }, 1);
                chIndex += length;

                return toRead.Substring(0, length) + (toRead[length] == ' ' ? " " : String.Empty);
            }
        }
    }
}