using System.Collections.Generic;
using System.Text;

namespace Yellow.Assets.JSON
{
    public class Parser : IParser
    {
        private List<Token> tokens = new List<Token>();

        public void Parse(string data)
        {
            tokens.Clear();

            SplitToTokens(data);

            foreach (var token in tokens)
            {
                System.Console.WriteLine(System.Enum.GetName(typeof(Token.TokenType), token.type) + " " + token.data);
            }
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
                                Unexpected("float can't have more than one dot symbol.");
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
                                Unexpected("octal numbers are unsupported.");
                            }
                        }
                        else if (IsWhitespace(symbol) || IsScope(symbol) || symbol == ',')
                        {
                            // last symbol
                            if (token[^1] == '.')
                            {
                                Unexpected("float can't have a dot in the end.");
                            }

                            tokens.Add(new Token()
                            {
                                data = token.ToString(),
                                type = containsDot ? Token.TokenType.Float : Token.TokenType.Integer
                            });

                            break;
                        }
                        else
                        {
                            Unexpected(symbol.ToString());
                        }

                        ++index;
                    }
                }
                else if (IsScope(symbol))
                {
                    tokens.Add(new Token()
                    {
                        data = token.ToString(),
                        type = Token.TokenType.Scope
                    });

                    ++index;
                }
                else if (symbol == ':')
                {
                    tokens.Add(new Token()
                    {
                        data = null,
                        type = Token.TokenType.Colon
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
                            tokens.Add(new Token()
                            {
                                data = token.ToString(),
                                type = Token.TokenType.String
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
                                    tokens.Add(new Token()
                                    {
                                        data = temp,
                                        type = Token.TokenType.Boolean
                                    });

                                    break;

                                case "null":
                                    tokens.Add(new Token()
                                    {
                                        data = null,
                                        type = Token.TokenType.Null
                                    });

                                    break;
                                default:
                                    Unexpected("identifiers aren't allowed.");

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

        private static void Unexpected(string data)
        {
            throw new JSONException(JSONException.ExceptionReason.UnexpectedSymbol, data);
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
