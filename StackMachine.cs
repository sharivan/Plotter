using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Plotter
{
    public class StackMachine
    {
        // Enumeração contendo todos os códigos de instruções
        private enum Operator
        {
            LCONST = 1, // load const - carrega uma constante (numero)
            LVAR = 2, // load variable - carrega o valor de uma variavel
            ADD = 3, // soma
            SUB = 4, // subtração
            MUL = 5, // multiplicação
            DIV = 6, // divisão
            POW = 7, // potenciação
            SQRT = 8, // square root (raiz quadrada)
            CBRT = 9, // cubic root (raiz cubica)
            EXP = 10, // exponencial
            LN = 11, // logaritmo natural (base e)
            SIN = 12, // seno
            COS = 13, // cosseno
            TAN = 14, // tangente
            SEC = 15, // secante
            COSEC = 16, // cossecante
            COTAN = 17, // cotangente
            ASIN = 18, // arco seno
            ACOS = 19, // arco cosseno
            ATAN = 20, // arco tangente
            ABS = 21, // módulo (valor absoluto)
            NEG = 22, // negação (inversão de sinal)
            SINH = 23, // seno hiperbólico
            COSH = 24, // cosseno hiperbólico
            TANH = 25, // tangente hiperbólica
            ASINH = 26, // seno hiperbólico inverso
            ACOSH = 27, // cosseno hiperbólico inverso
            ATANH = 28 // tangente hiperbólica inversa
        }

        private bool generateExceptionObject; // indica se o método Eval deve ou não gerar uma exceção quando um erro aritimético ocorre durante a avaliação

        private MemoryStream operations; // stream contendo as operações
        private BinaryReader reader; // leitor da stream de operações
        private BinaryWriter writer; // escritor da stream de operações

        private Stack<float> stack; // pilha contendo o resultado das operações
        private List<float> vars; // lista de variáveis
        private Dictionary<string, int> varMap; // dicionário contendo o mapeamento dos nomes de variáveis para seus respectivos indices

        private bool exception; // indica se ocorreu um erro aritimético durante a avaliação
        private ArithmeticException exceptionObject; // se generateExceptionObject for true, indica a exceção relacionada ao erro aritimético ocorrido durante a avaliação

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

        public long Size
        {
            get
            {
                return operations.Length;
            }
        }

        public int VarCount
        {
            get
            {
                return vars.Count;
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
            operations = new MemoryStream();
            reader = new BinaryReader(operations);
            writer = new BinaryWriter(operations);

            stack = new Stack<float>();
            vars = new List<float>();
            varMap = new Dictionary<string, int>();

            generateExceptionObject = true;
        }

        public void PushLConst(float number)
        {
            writer.Write((byte)Operator.LCONST);
            writer.Write(number);
        }

        public void PushLVar(string name)
        {
            int varIndex = SetVar(name, 0);
            writer.Write((byte)Operator.LVAR);
            writer.Write(varIndex);
        }

        public void PushAdd()
        {
            writer.Write((byte)Operator.ADD);
        }

        public void PushSub()
        {
            writer.Write((byte)Operator.SUB);
        }

        public void PushMul()
        {
            writer.Write((byte)Operator.MUL);
        }

        public void PushDiv()
        {
            writer.Write((byte)Operator.DIV);
        }

        public void PushPow()
        {
            writer.Write((byte)Operator.POW);
        }

        public void PushSqrt()
        {
            writer.Write((byte)Operator.SQRT);
        }

        public void PushCbrt()
        {
            writer.Write((byte)Operator.CBRT);
        }

        public void PushExp()
        {
            writer.Write((byte)Operator.EXP);
        }

        public void PushLn()
        {
            writer.Write((byte)Operator.LN);
        }

        public void PushSin()
        {
            writer.Write((byte)Operator.SIN);
        }

        public void PushCos()
        {
            writer.Write((byte)Operator.COS);
        }

        public void PushTan()
        {
            writer.Write((byte)Operator.TAN);
        }

        public void PushSec()
        {
            writer.Write((byte)Operator.SEC);
        }

        public void PushCosec()
        {
            writer.Write((byte)Operator.COSEC);
        }

        public void PushCotan()
        {
            writer.Write((byte)Operator.COTAN);
        }

        public void PushASin()
        {
            writer.Write((byte)Operator.ASIN);
        }
        public void PushACos()
        {
            writer.Write((byte)Operator.ACOS);
        }

        public void PushATan()
        {
            writer.Write((byte)Operator.ATAN);
        }

        public void PushAbs()
        {
            writer.Write((byte)Operator.ABS);
        }

        public void PushSinH()
        {
            writer.Write((byte)Operator.SINH);
        }

        public void PushCosH()
        {
            writer.Write((byte)Operator.COSH);
        }

        public void PushTanH()
        {
            writer.Write((byte)Operator.TANH);
        }

        public void PushASinH()
        {
            writer.Write((byte)Operator.ASINH);
        }

        public void PushACosH()
        {
            writer.Write((byte)Operator.ACOSH);
        }

        public void PushATanH()
        {
            writer.Write((byte)Operator.ATANH);
        }

        public void PushNeg()
        {
            writer.Write((byte)Operator.NEG);
        }

        public int GetVarIndex(string name)
        {
            if (!varMap.ContainsKey(name))
                return -1;

            return varMap[name];
        }

        public int SetVar(string name, float value)
        {
            int varIndex;
            if (!varMap.ContainsKey(name))
            {
                vars.Add(value);
                varIndex = vars.Count - 1;
                varMap[name] = varIndex;
            }
            else
            {
                varIndex = varMap[name];
                vars[varIndex] = value;
            }

            return varIndex;
        }

        public void SetVar(int varIndex, float value)
        {
            vars[varIndex] = value;
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

            if (operations.Length == 0)
                throw new Exception("No operations.");

            long len = operations.Position;
            stack.Clear();
            operations.Position = 0;

            float operand1;
            float operand2;
            int varIndex;

            while (operations.Position < len)
            {
                Operator op = (Operator)reader.ReadByte();
                switch (op)
                {
                    case Operator.LCONST:
                        operand1 = reader.ReadSingle();
                        stack.Push(operand1);
                        break;

                    case Operator.LVAR:
                        varIndex = reader.ReadInt32();
                        stack.Push(vars[varIndex]);
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
                        stack.Push(CheckResult((float)Math.Pow(operand1, operand2)));
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

                        stack.Push(CheckResult((float)Math.Sqrt(operand1)));
                        break;

                    case Operator.CBRT:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)Math.Cbrt(operand1)));
                        break;

                    case Operator.EXP:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)Math.Exp(operand1)));
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

                        stack.Push(CheckResult((float)Math.Log(operand1)));
                        break;

                    case Operator.SIN:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)Math.Sin(operand1)));
                        break;

                    case Operator.COS:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)Math.Cos(operand1)));
                        break;

                    case Operator.TAN:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)Math.Tan(operand1)));
                        break;

                    case Operator.SEC:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)checked(1 / Math.Cos(operand1))));
                        break;

                    case Operator.COSEC:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)checked(1 / Math.Sin(operand1))));
                        break;

                    case Operator.COTAN:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)checked(1 / Math.Tan(operand1))));
                        break;

                    case Operator.ASIN:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)Math.Asin(operand1)));
                        break;

                    case Operator.ACOS:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)Math.Acos(operand1)));
                        break;

                    case Operator.ATAN:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)Math.Atan(operand1)));
                        break;

                    case Operator.ABS:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)Math.Abs(operand1)));
                        break;

                    case Operator.NEG:
                        operand1 = stack.Pop();
                        stack.Push(-operand1);
                        break;

                    case Operator.SINH:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)Math.Sinh(operand1)));
                        break;

                    case Operator.COSH:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)Math.Cosh(operand1)));
                        break;

                    case Operator.TANH:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)Math.Tanh(operand1)));
                        break;

                    case Operator.ASINH:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)Math.Asinh(operand1)));
                        break;

                    case Operator.ACOSH:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)Math.Acosh(operand1)));
                        break;

                    case Operator.ATANH:
                        operand1 = stack.Pop();
                        stack.Push(CheckResult((float)Math.Atanh(operand1)));
                        break;

                    default:
                        throw new InvalidOperationException("Invalid operation: " + op);
                }

                if (exception)
                    return float.NaN;
            }

            return stack.Pop();
        }

        public void Clear()
        {
            operations.Position = 0;
            stack.Clear();
            vars.Clear();
            varMap.Clear();
        }
    }
}
