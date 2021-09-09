using System;
using System.Collections.Generic;
using System.Text;

namespace Plotter
{
    public class StackMachine
    {
        private enum Operator
        {
            LCONST,
            LVAR,
            ADD,
            SUB,
            MUL,
            DIV,
            POW,
            SQRT,
            EXP,
            LN,
            SIN,
            COS,
            TAN,
            ASIN,
            ACOS,
            ATAN,
            ABS,
            NEG,
            SINH,
            COSH,
            TANH,
            ASINH,
            ACOSH,
            ATANH
        }

        private struct Operation
        {
            public Operator op;
            public float number;
            public char var;

            public Operation(Operator op)
            {
                this.op = op;
                number = 0;
                var = '\0';
            }

            public Operation(float number)
            {
                this.op = Operator.LCONST;
                this.number = number;
                var = '\0';
            }

            public Operation(char var)
            {
                this.op = Operator.LVAR;
                this.number = 0;
                this.var = var;
            }
        }

        private bool generateExceptionObject;

        private List<Operation> operations;
        private Stack<float> stack;
        private Dictionary<char, float> vars;

        private bool exception;
        private ArithmeticException exceptionObject;

        public bool GenerateExceptionObject
        {
            get
            {
                return generateExceptionObject;
            }

            set
            {
                generateExceptionObject = value;
            }
        }

        public int Count
        {
            get
            {
                return operations.Count;
            }
        }

        public bool Exception
        {
            get
            {
                return exception;
            }
        }

        public ArithmeticException ExceptionObject
        {
            get
            {
                return exceptionObject;
            }
        }

        public StackMachine()
        {
            operations = new List<Operation>();
            stack = new Stack<float>();
            vars = new Dictionary<char, float>();

            generateExceptionObject = true;
        }

        public void PushLConst(float number)
        {
            operations.Add(new Operation(number));
        }

        public void PushLVar(char var)
        {
            operations.Add(new Operation(var));
            vars[var] = 0;
        }

        public void PushAdd()
        {
            operations.Add(new Operation(Operator.ADD));
        }

        public void PushSub()
        {
            operations.Add(new Operation(Operator.SUB));
        }

        public void PushMul()
        {
            operations.Add(new Operation(Operator.MUL));
        }

        public void PushDiv()
        {
            operations.Add(new Operation(Operator.DIV));
        }

        public void PushPow()
        {
            operations.Add(new Operation(Operator.POW));
        }

        public void PushSqrt()
        {
            operations.Add(new Operation(Operator.SQRT));
        }

        public void PushExp()
        {
            operations.Add(new Operation(Operator.EXP));
        }

        public void PushLn()
        {
            operations.Add(new Operation(Operator.LN));
        }

        public void PushSin()
        {
            operations.Add(new Operation(Operator.SIN));
        }

        public void PushCos()
        {
            operations.Add(new Operation(Operator.COS));
        }

        public void PushTan()
        {
            operations.Add(new Operation(Operator.TAN));
        }
        public void PushASin()
        {
            operations.Add(new Operation(Operator.ASIN));
        }
        public void PushACos()
        {
            operations.Add(new Operation(Operator.ACOS));
        }

        public void PushATan()
        {
            operations.Add(new Operation(Operator.ATAN));
        }

        public void PushAbs()
        {
            operations.Add(new Operation(Operator.ABS));
        }

        public void PushSinH()
        {
            operations.Add(new Operation(Operator.SINH));
        }

        public void PushCosH()
        {
            operations.Add(new Operation(Operator.COSH));
        }

        public void PushTanH()
        {
            operations.Add(new Operation(Operator.COSH));
        }

        public void PushASinH()
        {
            operations.Add(new Operation(Operator.ASINH));
        }

        public void PushACosH()
        {
            operations.Add(new Operation(Operator.ACOSH));
        }

        public void PushATanH()
        {
            operations.Add(new Operation(Operator.ACOSH));
        }

        public void PushNeg()
        {
            operations.Add(new Operation(Operator.NEG));
        }

        public void SetVar(char var, float value)
        {
            vars[var] = value;
        }

        private float CheckResult(float result)
        {
            if (float.IsInfinity(result))
            {
                exception = true;
                
                if (generateExceptionObject)
                {
                    exceptionObject = new ArithmeticException("Result is infinity.");
                    throw exceptionObject;
                }
            }

            if (float.IsNaN(result))
            {
                exception = true;

                if (generateExceptionObject)
                {
                    exceptionObject = new ArithmeticException("Result is NaN.");
                    throw exceptionObject;
                }
            }

            return result;
        }

        public float Eval()
        {
            exception = false;
            exceptionObject = null;

            if (operations.Count == 0)
                throw new Exception("Empty expression.");

            stack.Clear();

            float operand1;
            float operand2;
           
            for (int i = 0; i < operations.Count; i++)
            {
                Operation op = operations[i];
                switch (op.op)
                {
                    case Operator.LCONST:
                        stack.Push(op.number);
                        break;

                    case Operator.LVAR:
                        stack.Push(vars[op.var]);
                        break;

                    case Operator.ADD:
                        operand2 = stack.Pop();
                        operand1 = stack.Pop();
                        try
                        {
                            stack.Push(checked(operand1 + operand2));
                        }
                        catch (ArithmeticException e)
                        {
                            exception = true;
                            if (generateExceptionObject)
                            {
                                exceptionObject = e;
                                throw e;
                            }
                        }

                        break;

                    case Operator.SUB:
                        operand2 = stack.Pop();
                        operand1 = stack.Pop();

                        try
                        {
                            stack.Push(checked(operand1 - operand2));
                        }
                        catch (ArithmeticException e)
                        {
                            exception = true;
                            if (generateExceptionObject)
                            {
                                exceptionObject = e;
                                throw e;
                            }
                        }

                        break;

                    case Operator.MUL:
                        operand2 = stack.Pop();
                        operand1 = stack.Pop();

                        try
                        {
                            stack.Push(checked(operand1 * operand2));
                        }
                        catch (ArithmeticException e)
                        {
                            exception = true;
                            if (generateExceptionObject)
                            {
                                exceptionObject = e;
                                throw e;
                            }
                        }

                        break;

                    case Operator.DIV:
                        operand2 = stack.Pop();
                        operand1 = stack.Pop();

                        if (operand2 == 0)
                        {
                            exception = true;

                            if (generateExceptionObject)
                            {
                                exceptionObject = new ArithmeticException("Division by zero.");
                                throw exceptionObject;
                            }

                            break;
                        }

                        try
                        {
                            stack.Push(checked(operand1 / operand2));
                        }
                        catch (ArithmeticException e)
                        {
                            exception = true;
                            if (generateExceptionObject)
                            {
                                exceptionObject = e;
                                throw e;
                            }
                        }

                        break;

                    case Operator.POW:
                        operand2 = stack.Pop();
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float) Math.Pow(operand1, operand2)));
                        break;

                    case Operator.SQRT:
                        operand1 = stack.Pop();
                        if (operand1 < 0)
                        {
                            exception = true;

                            if (generateExceptionObject)
                            {
                                exceptionObject = new ArithmeticException("Sqrt of a negative operand: " + operand1);
                                throw exceptionObject;
                            }

                            break;
                        }

                        stack.Push(CheckResult((float) Math.Sqrt(operand1)));
                        break;

                    case Operator.EXP:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float) Math.Exp(operand1)));
                        break;

                    case Operator.LN:
                        operand1 = stack.Pop();
                        if (operand1 <= 0)
                        {
                            exception = true;

                            if (generateExceptionObject)
                            {
                                exceptionObject = new ArithmeticException("Ln of a non positive operand: " + operand1);
                                throw exceptionObject;
                            }

                            break;
                        }

                        stack.Push(CheckResult((float) Math.Log(operand1)));
                        break;

                    case Operator.SIN:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float) Math.Sin(operand1)));
                        break;

                    case Operator.COS:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float) Math.Cos(operand1)));
                        break;

                    case Operator.TAN:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float) Math.Tan(operand1)));
                        break;

                    case Operator.ASIN:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float) Math.Asin(operand1)));
                        break;

                    case Operator.ACOS:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float) Math.Acos(operand1)));
                        break;

                    case Operator.ATAN:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float) Math.Atan(operand1)));
                        break;

                    case Operator.ABS:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float) Math.Abs(operand1)));
                        break;

                    case Operator.NEG:
                        operand1 = stack.Pop();
                        stack.Push(-operand1);
                        break;

                    case Operator.SINH:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float) Math.Sinh(operand1)));
                        break;

                    case Operator.COSH:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float) Math.Cosh(operand1)));
                        break;

                    case Operator.TANH:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float) Math.Tanh(operand1)));
                        break;

                    case Operator.ASINH:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float) Math.Asinh(operand1)));
                        break;

                    case Operator.ACOSH:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float) Math.Acosh(operand1)));
                        break;

                    case Operator.ATANH:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float) Math.Atanh(operand1)));
                        break;

                    default:
                        throw new InvalidOperationException("Invalid operation: " + op.op);
                }

                if (exception)
                    return float.NaN;
            }

            return stack.Pop();
        }

        public void Clear()
        {
            operations.Clear();
            stack.Clear();
            vars.Clear();
        }
    }
}
