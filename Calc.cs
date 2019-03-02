using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Writer;
using dnlib.DotNet;

namespace NumCalculator
{
    class Program
    {
        public static int Calc;
        public static int subCalc;
        public static int subSubCalc;
        public static int Final;
        public static string tmp_;
        public static Random rnd = new Random();

        public static void Start(MethodDef method)
        {
                    for (int i = 0; i < method.Body.Instructions.Count; i++)
                    {
                        if (method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4)
                        {
                            int orig = (int)method.Body.Instructions[i].Operand;
                            Calc = rnd.Next(25, 50);
                            subCalc += Calc;
                            method.Body.Instructions.Insert(i, Instruction.Create(OpCodes.Ldc_I4, Calc));
                            Calc = rnd.Next(50, 75);
                            subSubCalc += Calc;
                            method.Body.Instructions.Insert(i + 1, Instruction.Create(OpCodes.Ldc_I4, Calc));
                            method.Body.Instructions.Insert(i + 2, Instruction.Create(GerarOpcode()));
                            Final += CalcHelper();
                            method.Body.Instructions.Insert(i + 3, Instruction.Create(OpCodes.Ldc_I4, orig - Final));
                            method.Body.Instructions.Insert(i + 4, Instruction.Create(OpCodes.Add));
                            i += 4;
                            Final = 0;
                            Calc = 0;
                            subCalc = 0;
                            subSubCalc = 0;
                        }

                    }
        }
        public static int CalcHelper()
        {
            if (tmp_ == OpCodes.Add.ToString())
            {
                return subCalc + subSubCalc;
            }

            if (tmp_ == OpCodes.Sub.ToString())
            {
                return subCalc - subSubCalc;
            }
            if (tmp_ == OpCodes.Xor.ToString())
            {
                return subCalc ^ subSubCalc;
            }
            if (tmp_ == OpCodes.Mul.ToString())
            {
                return subCalc * subSubCalc;
            }
            if (tmp_ == OpCodes.Div.ToString())
            {
                return subCalc / subSubCalc;
            }
            return 0;
        }
        public static OpCode GerarOpcode()
        {
            int random = rnd.Next(1, 5);
            if (random == 1)
            {
                tmp_ = OpCodes.Sub.ToString();
                return OpCodes.Sub;
            }
            if (random == 2)
            {
                tmp_ = OpCodes.Add.ToString();
                return OpCodes.Add;
            }
            if (random == 3)
            {
                tmp_ = OpCodes.Xor.ToString();
                return OpCodes.Xor;
            }
            if (random == 4)
            {
                tmp_ = OpCodes.Mul.ToString();
                return OpCodes.Mul;
            }
            if (random == 5)
            {
                tmp_ = OpCodes.Div.ToString();
                return OpCodes.Div;
            }
            return OpCodes.Nop;
        }
    }
}
