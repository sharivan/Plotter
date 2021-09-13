using System;
using System.Collections.Generic;
using System.Text;

namespace Plotter
{
    public class Parser
    {
        private string input;
        private int pos;

        private List<Token> tokens;
        private int tokenIndex;

        public string Input
        {
            get
            {
                return input;
            }

            set
            {
                input = value + '\0';
                tokens.Clear();
                tokenIndex = -1;
                pos = 0;
            }
        }

        public Parser() : this("")
        {
        }

        public Parser(string input)
        {
            tokens = new List<Token>();

            Input = input;
        }

        public Token CurrentToken()
        {
            if (tokens.Count == 0)
                return null;

            return tokens[tokenIndex];
        }

        public Token NextToken()
        {
            if (tokenIndex < tokens.Count - 1)
                return tokens[++tokenIndex];

            if (pos >= input.Length)
                return null;

            char c = input[pos++];

            if (c == '\0')
                return null;

            Token result = null;

            if (c >= '0' && c <= '9') // é um digito numérico?
            {
                string number = c + "";
                c = input[pos++];
                while (c >= '0' && c <= '9') // enquanto for um digito numérico
                {
                    number += c;
                    if (pos >= input.Length)
                        break;

                    c = input[pos++];
                }

                if (c == '.')
                {
                    number += '.';                           
                    c = input[pos++];
                    while (c >= '0' && c <= '9') // enquanto for um digito numérico
                    {
                        number += c;
                        c = input[pos++];
                    }

                    pos--;
                }
                else
                    pos--;

                try
                {
                    float value = float.Parse(number, System.Globalization.CultureInfo.InvariantCulture);
                    result = new Number(value);
                }
                catch (FormatException e)
                {
                    throw new ParserException("Invalid number format: " + number, e);
                }
                catch (OverflowException e)
                {
                    throw new ParserException("Overflow on converting number: " + number, e);
                }
            }
            else if (Symbol.IsSymbol(c))
                result = new Symbol(c);
            else if (Variable.IsLetter(c)) // pode ser uma variável, uma constante ou uma função
            {
                string name = c + "";
                c = input[pos++];
                while (Variable.IsLetter(c) || c == '_') // uma variável, constante ou função pode conter somente letras ou _ (mas não iniciar com _)
                {
                    name += c;
                    c = input[pos++];
                }

                pos--;

                if (Constant.IsConstant(name))
                    result = new Constant(name);
                else if (Function.IsFunction(name))
                    result = new Function(name);
                else
                    result = new Variable(name);
            }
            else
                throw new ParserException("Invalid character: " + c);

            tokens.Add(result);
            tokenIndex = tokens.Count - 1;

            return result;
        }

        public Token PreviusToken()
        {
            if (tokenIndex < 0)
                return null;

            --tokenIndex;
            if (tokenIndex < 0)
                return null;

            return tokens[tokenIndex];
        }
    }
}
