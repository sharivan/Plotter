using System;
using System.Collections.Generic;
using System.Text;

namespace Plotter
{
    public class ExpressionCompiler
    {
        private Parser parser;

        public ExpressionCompiler()
        {
            parser = new Parser();
        }

        private void PrimaryExpression(StackMachine machine)
        {
            Token token = parser.NextToken();
            if (token is Number)
            {
                Number number = (Number) token;
                machine.PushLConst(number.Value);
                return;
            }

            if (token is Function)
            {
                Function func = (Function) token;

                token = parser.NextToken();
                if (!(token is Symbol))
                    throw new ParserException("Unexpected token: " + token);

                Symbol symbol = (Symbol)token;
                if (symbol.Value != '(')
                    throw new ParserException("Unexpected symbol: " + symbol.Value);

                AdditiveExpression(machine);

                token = parser.NextToken();
                if (!(token is Symbol))
                    throw new ParserException("Unexpected token: " + token);

                symbol = (Symbol)token;
                if (symbol.Value != ')')
                    throw new ParserException("Unexpected symbol: " + symbol.Value);

                switch (func.Name)
                {
                    case "sqrt":
                        machine.PushSqrt();
                        break;

                    case "cbrt":
                        machine.PushCbrt();
                        break;

                    case "exp":
                        machine.PushExp();
                        break;

                    case "ln":
                        machine.PushLn();
                        break;

                    case "sen":
                        machine.PushSin();
                        break;

                    case "cos":
                        machine.PushCos();
                        break;

                    case "tg":
                        machine.PushTan();
                        break;

                    case "sec":
                        machine.PushSec();
                        break;

                    case "cosec":
                        machine.PushCosec();
                        break;

                    case "cotg":
                        machine.PushCotan();
                        break;

                    case "arcsen":
                        machine.PushASin();
                        break;

                    case "arccos":
                        machine.PushACos();
                        break;

                    case "arctg":
                        machine.PushATan();
                        break;

                    case "abs":
                        machine.PushAbs();
                        break;

                    case "senh":
                        machine.PushSinH();
                        break;

                    case "cosh":
                        machine.PushCosH();
                        break;

                    case "tgh":
                        machine.PushTanH();
                        break;

                    case "argsenh":
                        machine.PushASinH();
                        break;

                    case "argcosh":
                        machine.PushACosH();
                        break;

                    case "argtgh":
                        machine.PushATanH();
                        break;
                }

                return;
            }

            if (token is Constant)
            {
                Constant c = (Constant) token;
                switch (c.Name)
                {
                    case "e":
                        machine.PushLConst((float) Math.E);
                        break;

                    case "pi":
                        machine.PushLConst((float)Math.PI);
                        break;
                }
                
                return;
            }

            if (token is Variable)
            {
                Variable var = (Variable)token;
                machine.PushLVar(var.Name);
                return;
            }

            if (token is Symbol)
            {
                Symbol symbol = (Symbol) token;
                if (symbol.Value != '(')
                    throw new ParserException("Unexpected symbol: " + symbol.Value);

                AdditiveExpression(machine);

                token = parser.NextToken();
                if (!(token is Symbol))
                    throw new ParserException("Unexpected token: " + token);

                symbol = (Symbol)token;
                if (symbol.Value != ')')
                    throw new ParserException("Unexpected symbol: " + symbol.Value);

                return;
            }

            throw new ParserException("Unexpected token: " + token);
        }

        private void PotenciativeExpression(StackMachine machine)
        {
            PrimaryExpression(machine);

            bool loop = true;
            while (loop)
            {
                Token token = parser.NextToken();
                if (token is Symbol)
                {
                    Symbol symbol = (Symbol)token;
                    if (symbol.Value != '^')
                    {
                        parser.PreviusToken();
                        loop = false;
                    }

                    if (loop)
                    {
                        UnaryExpression(machine);
                        machine.PushPow();
                    }
                }
                else
                {
                    parser.PreviusToken();
                    loop = false;
                }
            }
        }

        private void UnaryExpression(StackMachine machine)
        {
            bool neg = false;
            Token token = parser.NextToken();
            if (token is Symbol)
            {
                Symbol symbol = (Symbol)token;
                switch (symbol.Value)
                {
                    case '+':
                        break;

                    case '-':
                        neg = true;
                        break;

                    default:
                        parser.PreviusToken();
                        break;
                }
            }
            else
                parser.PreviusToken();

            PotenciativeExpression(machine);

            if (neg)
                machine.PushNeg();
        }

        private void MultiplicativeExpression(StackMachine machine)
        {
            UnaryExpression(machine);

            bool loop = true;
            while (loop)
            {
                Token token = parser.NextToken();
                if (token is Symbol)
                {
                    Symbol symbol = (Symbol)token;
                    switch (symbol.Value)
                    {
                        case '*':
                            break;

                        case '/':
                            break;

                        default:
                            parser.PreviusToken();
                            loop = false;
                            break;
                    }

                    if (loop)
                    {
                        UnaryExpression(machine);

                        switch (symbol.Value)
                        {
                            case '*':
                                machine.PushMul();
                                break;

                            case '/':
                                machine.PushDiv();
                                break;
                        }
                    }
                }
                else
                {
                    parser.PreviusToken();
                    loop = false;
                }
            }
        }

        private void AdditiveExpression(StackMachine machine)
        {
            MultiplicativeExpression(machine);

            bool loop = true;
            while (loop)
            {
                Token token = parser.NextToken();
                if (token is Symbol)
                {
                    Symbol symbol = (Symbol)token;
                    switch (symbol.Value)
                    {
                        case '+':
                            break;

                        case '-':
                            break;

                        default:
                            parser.PreviusToken();
                            loop = false;
                            break;
                    }

                    if (loop)
                    {
                        MultiplicativeExpression(machine);

                        switch (symbol.Value)
                        {
                            case '+':
                                machine.PushAdd();
                                break;

                            case '-':
                                machine.PushSub();
                                break;
                        }
                    }
                }
                else
                {
                    parser.PreviusToken();
                    loop = false;
                }
            }
        }

        public void Compile(string expression, StackMachine machine)
        {
            parser.Input = expression;
            AdditiveExpression(machine);
        }
    }
}
