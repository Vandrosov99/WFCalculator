using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class BoolEquation:IElem<bool>
    {
        protected bool rodipxam;
        protected List<IElem<bool>> elements;
        protected List<string> signs;
        public BoolEquation(ref string equation, bool sign)
        {
            elements = new List<IElem<bool>>();
            equation = equation.Replace(" ", string.Empty);
            signs = new List<string>();
            signs.Add(@"\/");
            this.rodipxam = sign;
            Proceed(ref equation);
        }
        private void Proceed(ref string equation)
        {
            rodipxam = true;
            string num = "";
            for (int i = 0; i < equation.Length; i++)
            {
                if (equation[i] == '-' && i == 0)
                {
                    rodipxam = !(rodipxam^false);
                    continue;
                }
                if (equation[i] == '(')
                {
                    equation = equation.Remove(0, i + 1);
                    elements.Add(new BoolEquation(ref equation, rodipxam));
                    num = "";
                    if (equation[0] != '#' && (equation[0] == '/' || equation[0] == '\\'))
                    {
                        string sign = "";
                        for (; i < equation.Length; i++)
                        {
                            sign += equation[i];
                            if (sign == "/\\" || sign == "\\/")
                            {
                                break;
                            }
                        }
                        signs.Add(sign);
                    }
                    i = -1;
                    rodipxam = true;
                    continue;
                }
                if ((i == equation.Length - 1 && equation[i] == '#') || equation[i] == '/' || equation[i] == '\\')
                {
                    if (equation[i] != '#')
                    {
                        string sign = "";
                        for (; i < equation.Length; i++)
                        {
                            sign += equation[i];
                            if (sign == "/\\" || sign == "\\/")
                            {
                                break;
                            }
                        }
                        signs.Add(sign);
                    }
                    if (num != "")
                    {
                        equation = equation.Remove(0, i + 1);
                        if (num != "#")
                        {
                            AddElement(num, rodipxam);
                        }
                        else
                        {
                            break;
                        }
                        i = -1;
                        rodipxam = true;
                        num = "";
                    }
                    continue;
                }
                if (equation[i] == ')')
                {
                    equation = equation.Remove(0, i + 1);
                    if (num != "")
                    {
                        if (num != "#")
                        {
                            AddElement(num, rodipxam);
                        }
                        else
                        {
                            break;
                        }
                    }
                    i = -1;
                    break;
                }
                num += equation[i].ToString();

            }
        }
        private void AddElement(string num, bool sign)
        {
            if (num == "1")
            {
                elements.Add(new BoolConst(true, sign));
            }
            else
            {
                elements.Add(new BoolConst(false, sign));
            }
        }
        public static bool Disjunction(bool a, bool b)
        {
            return a | b;
        }
        public static bool Conjunction(bool a, bool b)
        {
            return a & b;
        }

        public bool ReturnValue()
        {
            bool value = true;

            for (int i = 0; i < elements.Count; i++)
            {
                string prevSign = signs[i];
                bool var = elements[i].ReturnValue();
                if (i + 1 < signs.Count && (signs[i + 1][0] == '/' || signs[i + 1][0] == '\\'))
                {
                    i++;
                    bool tempVar = var;
                    bool goOn = true;
                    for (; i < elements.Count && goOn; i++)
                    {
                        if (signs[i] == "\\/")
                        {
                            tempVar = Disjunction(elements[i].ReturnValue(), tempVar);
                        }
                        else if (signs[i] == "/\\")
                        {
                            tempVar = Conjunction(elements[i].ReturnValue(), tempVar);
                        }
                        else
                        {
                            i -= 2;
                            goOn = false;
                        }
                    }
                    var = tempVar;
                }
                value = var;
            }
            return Conjunction(rodipxam,value);
        }

        public void Tfest()
        {
            throw new NotImplementedException();
        }
    }
}
