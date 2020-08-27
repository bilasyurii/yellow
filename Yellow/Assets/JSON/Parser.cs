using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System;

namespace Yellow.Assets.JSON
{
    public class Parser : IParser
    {
        private readonly List<Node> nodes = new List<Node>();

        readonly Stack<bool> braces = new Stack<bool>();

        private Node root;

        private int currentNode;

        private int nodesCount;

        private int listBraceCount;

        private int dictionaryBraceCount;
        
        public void Parse(string data)
        {
            nodes.Clear();
            braces.Clear();

            SplitToTokens(data);

            listBraceCount = 0;
            dictionaryBraceCount = 0;
            currentNode = 0;
            nodesCount = nodes.Count;

            root = ParseNode();
        }

        private void SplitToTokens(string str)
        {
            char symbol;
            var token = new StringBuilder();
            int dataLength = str.Length;
            int index = 0;
            // tells if the dot symbol has been
            // already spotted in the current numeric token
            bool containsDot;
            string temp;

            while (index < dataLength)
            {
                symbol = str[index];

                token.Clear();
                token.Append(symbol);

                if (IsWhitespace(symbol))
                {
                    ++index;
                }
                else if (IsDigit(symbol))
                {
                    ++index;

                    containsDot = false;

                    while (index < dataLength)
                    {
                        symbol = str[index];

                        if (symbol == '.')
                        {
                            token.Append(symbol);

                            if (containsDot)
                            {
                                UnexpectedSymbol("float can't have more than one dot symbol.");
                            }
                            else
                            {
                                containsDot = true;
                            }
                        }
                        else if (IsDigit(symbol))
                        {
                            token.Append(symbol);

                            if (index == 1 && symbol == '0' && token[0] == '0')
                            {
                                UnexpectedSymbol("octal numbers are unsupported.");
                            }
                        }
                        else if (IsWhitespace(symbol) || IsScope(symbol) || symbol == ',')
                        {
                            // last symbol
                            if (token[^1] == '.')
                            {
                                UnexpectedSymbol("float can't have a dot in the end.");
                            }

                            if (containsDot)
                            {
                                nodes.Add(new Node()
                                {
                                    data = float.Parse(token.ToString(), CultureInfo.InvariantCulture),
                                    type = Node.NodeType.Float
                                });
                            }
                            else
                            {

                                nodes.Add(new Node()
                                {
                                    data = int.Parse(token.ToString(), CultureInfo.InvariantCulture),
                                    type = Node.NodeType.Integer
                                });
                            }

                            break;
                        }
                        else
                        {
                            UnexpectedSymbol(symbol.ToString());
                        }

                        ++index;
                    }
                }
                else if (IsScope(symbol))
                {
                    nodes.Add(new Node()
                    {
                        data = symbol,
                        type = Node.NodeType.Scope
                    });

                    ++index;
                }
                else if (symbol == ':')
                {
                    nodes.Add(new Node()
                    {
                        data = null,
                        type = Node.NodeType.Colon
                    });

                    ++index;
                }
                else if (symbol == '\"')
                {
                    token.Clear();

                    ++index;

                    while (index < dataLength)
                    {
                        symbol = str[index];
                        ++index;

                        if (symbol == '\"')
                        {
                            nodes.Add(new Node()
                            {
                                data = token.ToString(),
                                type = Node.NodeType.String
                            });

                            break;
                        }
                        else if (symbol == '\\')
                        {
                            symbol = str[index];

                            switch (symbol)
                            {
                                case 'n':
                                    token.Append('\n');
                                    break;

                                case '\"':
                                    token.Append('\"');
                                    break;

                                case '\'':
                                    token.Append('\'');
                                    break;

                                case '\\':
                                    token.Append('\\');
                                    break;

                                case 't':
                                    token.Append('\t');
                                    break;
                            }

                            ++index;
                        }
                        else
                        {
                            token.Append(symbol);
                        }
                    }
                }
                else if (IsLetter(symbol))
                {
                    ++index;

                    while (index < dataLength)
                    {
                        symbol = str[index];

                        if (IsLetter(symbol))
                        {
                            token.Append(symbol);
                        }
                        else
                        {
                            temp = token.ToString();

                            switch (temp)
                            {
                                case "true":
                                case "false":
                                    nodes.Add(new Node()
                                    {
                                        data = (temp == "true"),
                                        type = Node.NodeType.Boolean
                                    });
                                    break;

                                case "null":
                                    nodes.Add(new Node()
                                    {
                                        data = null,
                                        type = Node.NodeType.Null
                                    });
                                    break;

                                default:
                                    UnexpectedSymbol("identifiers aren't allowed.");
                                    break;
                            }

                            break;
                        }

                        ++index;
                    }
                }
                else
                {
                    ++index;
                }
            }
        }

        private Node ParseNode()
        {
            Node node;

            while (currentNode < nodesCount)
            {
                node = nodes[currentNode];

                switch (node.type)
                {
                    case Node.NodeType.Scope:
                        {
                            char bracket = node.Char;

                            switch (bracket)
                            {
                                case '{':
                                    return ParseDictionary();

                                case '[':
                                    return ParseList();

                                case '}':
                                    CloseDictionary();
                                    break;

                                case ']':
                                    CloseList();
                                    break;
                            }
                        }
                        break;

                    case Node.NodeType.String:
                    case Node.NodeType.Boolean:
                    case Node.NodeType.Float:
                    case Node.NodeType.Integer:
                    case Node.NodeType.Null:
                        ++currentNode;

                        return node;

                    default:
                        UnexpectedToken(GetNodeTypeName(node.type) + ".");
                        break;
                }
            }

            return null;
        }

        private Node ParseDictionary()
        {
            ++dictionaryBraceCount;
            braces.Push(true);
            ++currentNode;

            var dictionary = new Dictionary<string, Node>();

            while (currentNode < nodesCount)
            {
                if (IsClosingDictionary())
                {
                    CloseDictionary();

                    break;
                }

                dictionary.Add(ParseKey(), ParseNode());
            }

            return new Node()
            {
                type = Node.NodeType.Dictionary,
                data = dictionary
            };
        }

        private Node ParseList()
        {
            ++listBraceCount;
            braces.Push(false);
            ++currentNode;

            var list = new List<Node>();

            while (currentNode < nodesCount)
            {
                if (IsClosingList())
                {
                    CloseList();

                    break;
                }

                list.Add(ParseNode());
            }

            return new Node()
            {
                type = Node.NodeType.List,
                data = list
            };
        }

        private bool IsClosingDictionary()
        {
            var node = nodes[currentNode];

            return node.type == Node.NodeType.Scope && node.Char == '}';
        }

        private bool IsClosingList()
        {
            var node = nodes[currentNode];

            return node.type == Node.NodeType.Scope && node.Char == ']';
        }

        private void CloseDictionary()
        {

            if (--dictionaryBraceCount < 0)
            {
                UnexpectedToken("redundant closing dictionary bracket '}'.");
            }

            // if last opened bracket was for a list
            if (braces.Pop() == false)
            {
                UnexpectedToken("closing dictionary bracket '}' at the beginning of file or after opening list bracket '['.");
            }

            ++currentNode;
        }

        private void CloseList()
        {

            if (--listBraceCount < 0)
            {
                UnexpectedToken("redundant closing list bracket ']'.");
            }

            // if last opened bracket was for a dictionary
            if (braces.Pop() == true)
            {
                UnexpectedToken("closing list bracket ']' at the beginning of file or after opening dictionary bracket '{'.");
            }

            ++currentNode;
        }

        private string ParseKey()
        {
            var node = nodes[currentNode];

            if (node.type == Node.NodeType.String)
            {
                var key = node.String;

                ++currentNode;

                node = nodes[currentNode++];

                if (node.type != Node.NodeType.Colon)
                {
                    UnexpectedToken($"expected colon after a key, but got {GetNodeTypeName(node.type)}");
                }

                return key;
            }
            else
            {
                UnexpectedToken($"expected key as a string, but got {GetNodeTypeName(node.type)}");
            }

            return null;
        }

        private static string GetNodeTypeName(Node.NodeType type)
        {
            return Enum.GetName(typeof (Node.NodeType), type);
        }

        private static void UnexpectedSymbol(string data)
        {
            throw new JSONException(JSONException.ExceptionReason.UnexpectedSymbol, data);
        }

        private static void UnexpectedToken(string data)
        {
            throw new JSONException(JSONException.ExceptionReason.UnexpectedToken, data);
        }

        public static bool IsDigit(char symbol)
        {
            return symbol >= '0' && symbol <= '9';
        }

        public static bool IsLetter(char symbol)
        {
            return (symbol >= 'a' && symbol <= 'z') || (symbol >= 'A' && symbol <= 'Z') || symbol == '_';
        }

        public static bool IsScope(char symbol)
        {
            return symbol == '{' || symbol == '}' || symbol == '[' || symbol == ']';
        }

        public static bool IsWhitespace(char symbol)
        {
            return symbol == ' ' || symbol == '\n' || symbol == '\t';
        }
    }
}
