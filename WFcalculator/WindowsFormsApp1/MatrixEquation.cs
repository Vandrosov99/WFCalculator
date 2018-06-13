using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class MatrixEquation:IElem<Matrix>
    {
        protected int sign = 1;
        protected List<IElem<Matrix>> elements;
        protected List<char> signs;
        public MatrixEquation(ref string equation, int sign)
        {
            elements = new List<IElem<Matrix>>();
            equation = equation.Trim();
            signs = new List<char>();
            signs.Add('+');
            this.sign = sign;
            Proceed(ref equation);
        }
        private void Proceed(ref string equation)
        {
            int sign = 1;
            bool inversed = false;
            bool one = false;
            string num = "";
            for (int i = 0; i < equation.Length; i++)
            {
                if (equation[i] == '-' && i == 0)
                {
                    sign *= -1;
                    continue;
                }
                if (equation[i] == '(')
                {
                    equation = equation.Remove(0, i + 1);
                    if (num.Contains("inv"))
                    {
                        inversed = true;
                    }
                    else if(num.Contains("E"))
                    {
                        one = true;
                    }
                    else
                    {
                        elements.Add(new MatrixEquation(ref equation, sign));
                    }
                    num = "";
                    
                    //equation = equation.Remove(0, 1);
                    i = -1;
                    if (equation[0] == '-' || equation[0] == '+' || equation[0] == '*')
                    {
                        signs.Add(equation[0]);
                        i = 0;
                    }
                    sign = 1;
                    continue;
                }
                if (equation[i] == '-' || equation[i] == '+' || equation[i] == '*'  || (i == equation.Length - 1 && equation[i] == '#'))
                {
                    if (equation[i] != '#')
                    {
                        signs.Add(equation[i]);
                    }

                    if (num != "")
                    {
                        equation = equation.Remove(0, i + 1);
                        if ((num.Contains('[') && num.Contains(']')) || num.Contains('E'))
                        {
                            AddElement(num, sign, ref inversed, ref one);
                        }
                        if (num == "#")
                        {
                            break;
                        }
                        i = -1;
                        sign = 1;
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
                            AddElement(num, sign, ref  inversed, ref one);
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
        private void AddElement(string num, int sign, ref bool inversed, ref bool one)
        {

            if (inversed)
            {
                elements.Add(new Matrix(2, num, sign));
                inversed = false;
            }
            else if (one)
            {
                int size = int.Parse(num);
                elements.Add(new Matrix(MatrixType.OneConstant, size));
            }
            else if (num.Trim() == "0")
            {
                elements.Add(new Matrix(0, num, sign));
            }
            else
            {
                elements.Add(new Matrix(num, sign));
            }
        }

        public Matrix ReturnValue()
        {
            Matrix m = elements[0].ReturnValue();
            for (int i = 1; i < signs.Count; i++)
            {
                switch (signs[i])
                {
                    case '+':
                        Matrix temp = elements[i].ReturnValue();
                        m += temp;
                        break;
                    case '-':
                        m -= elements[i].ReturnValue();
                        break;
                    case '*':
                        m *= elements[i].ReturnValue();
                        break;
                    default:
                        break;
                }
            }
            return m;
        }

        public void Tfest()
        {
            throw new NotImplementedException();
        }
    }
}
