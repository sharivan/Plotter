using System;
using System.Collections.Generic;
using System.Text;

namespace Plotter
{
    public class Parser
    {
        private string input;
        private int pos;

        private readonly List<Token> tokens;
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

        public Token CurrentToken() => tokens.Count == 0 ? null : tokens[tokenIndex];

        public Token NextToken()
        {
            if (tokenIndex < tokens.Count - 1)
                return tokens[++tokenIndex];

            if (pos >= input.Length)
                return null;

            char c = input[pos++];
           
            Token result = null;

            if (c != '\0')
            {
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
            }

            tokens.Add(result);
            tokenIndex = tokens.Count - 1;

            return result;
        }

        public Number NextNumber(bool throwException = true, string errorMessage = null)
        {
            Token token = NextToken();
            if (token == null)
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "Number expected but end of expression found.")
                    : (Number) null;
            }

            if (!(token is Number))
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "Number expected but " + token + " found.")
                    : (Number) null;
            }

            return (Number)token;
        }

        public Number NextNumber(float expectedValue, bool throwException = true, string errorMessage = null)
        {
            Token token = NextToken();
            if (token == null)
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "'" + expectedValue + "' but end of expression found.")
                    : (Number) null;
            }

            if (!(token is Symbol))
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "'" + expectedValue + "' expected but " + token + " found.")
                    : (Number) null;
            }

            var number = (Number)token;
            if (number.Value != expectedValue)
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "'" + expectedValue + "' expected but '" + number.Value + "' found.")
                    : (Number) null;
            }

            return number;
        }

        public Symbol NextSymbol(bool throwException = true, string errorMessage = null)
        {
            Token token = NextToken();
            if (token == null)
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "Symbol expected but end of expression found.")
                    : (Symbol) null;
            }

            if (!(token is Symbol))
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "Symbol expected but " + token + " found.")
                    : (Symbol) null;
            }

            return (Symbol)token;
        }

        public Symbol NextSymbol(char expectedValue, bool throwException = true, string errorMessage = null)
        {
            Token token = NextToken();
            if (token == null)
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "'" + expectedValue + "' expected but end of expression found.")
                    : (Symbol) null;
            }

            if (!(token is Symbol))
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "'" + expectedValue + "' expected but " + token + " found.")
                    : (Symbol) null;
            }

            var symbol = (Symbol)token;
            if (symbol.Value != expectedValue)
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "'" + expectedValue + "' expected but '" + symbol.Value + "' found.")
                    : (Symbol) null;
            }

            return symbol;
        }

        public Variable NextVariable(bool throwException = true, string errorMessage = null)
        {
            Token token = NextToken();
            if (token == null)
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "Variable expected but end of expression found.")
                    : (Variable) null;
            }

            if (!(token is Variable))
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "Variable expected but " + token + " found.")
                    : (Variable) null;
            }

            return (Variable)token;
        }

        public Variable NextVariable(string expectedValue, bool throwException = true, string errorMessage = null)
        {
            Token token = NextToken();
            if (token == null)
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "'" + expectedValue + "' but end of expression found.")
                    : (Variable) null;
            }

            if (!(token is Symbol))
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "'" + expectedValue + "' expected but " + token + " found.")
                    : (Variable) null;
            }

            var variable = (Variable)token;
            if (variable.Name != expectedValue)
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "'" + expectedValue + "' expected but '" + variable.Name + "' found.")
                    : (Variable) null;
            }

            return variable;
        }

        public Constant NextConstant(bool throwException = true, string errorMessage = null)
        {
            Token token = NextToken();
            if (token == null)
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "Constant expected but end of expression found.")
                    : (Constant) null;
            }

            if (!(token is Constant))
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "Constant expected but " + token + " found.")
                    : (Constant) null;
            }

            return (Constant)token;
        }

        public Constant NextConstant(string expectedValue, bool throwException = true, string errorMessage = null)
        {
            Token token = NextToken();
            if (token == null)
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "'" + expectedValue + "' but end of expression found.")
                    : (Constant) null;
            }

            if (!(token is Symbol))
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "'" + expectedValue + "' expected but " + token + " found.")
                    : (Constant) null;
            }

            var constant = (Constant)token;
            if (constant.Name != expectedValue)
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "'" + expectedValue + "' expected but '" + constant.Name + "' found.")
                    : (Constant) null;
            }

            return constant;
        }

        public Function NextFunction(bool throwException = true, string errorMessage = null)
        {
            Token token = NextToken();
            if (token == null)
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "Function expected but end of expression found.")
                    : (Function) null;
            }

            if (!(token is Function))
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "Function expected but " + token + " found.")
                    : (Function) null;
            }

            return (Function)token;
        }

        public Function NextFunction(string expectedValue, bool throwException = true, string errorMessage = null)
        {
            Token token = NextToken();
            if (token == null)
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "'" + expectedValue + "' but end of expression found.")
                    : (Function) null;
            }

            if (!(token is Symbol))
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "'" + expectedValue + "' expected but " + token + " found.")
                    : (Function) null;
            }

            var function = (Function)token;
            if (function.Name != expectedValue)
            {
                PreviusToken();

                return throwException
                    ? throw new ParserException(errorMessage != null ? errorMessage : "'" + expectedValue + "' expected but '" + function.Name + "' found.")
                    : (Function) null;
            }

            return function;
        }

        public Token PreviusToken()
        {
            if (tokenIndex < 0)
                return null;

            --tokenIndex;
            return tokenIndex < 0 ? null : tokens[tokenIndex];
        }
    }
}
