using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Equation:IElement
    {
        protected int sign = 1;
        protected List<IElement> elements;
        protected List<char> signs;
        public Equation(ref string equation,int sign)
        {
            elements = new List<IElement>();
            equation=equation.Replace(" ", string.Empty);
            signs = new List<char>();
            signs.Add('+');
            this.sign = sign;
            Proceed(ref equation);
        }
        private void Proceed(ref string equation)
        {
            int sign = 1;
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
                    if (num == "sin" || num == "cos" || num == "tan")
                    {
                        elements.Add(new Function(num, ref equation, sign));
                    }
                    else
                    {
                        elements.Add(new Equation(ref equation, sign));
                    }
                    num = "";
                    if (equation[0] == '-' || equation[0] == '+' || equation[0] == '*' || equation[0] == '/')
                    {
                        signs.Add(equation[0]);
                    }
                    //equation = equation.Remove(0, 1);
                    i = -1;
                    sign = 1;
                    continue;
                }
                if (equation[i] == '-' || equation[i] == '+' || equation[i] == '*' || equation[i] == '/' || ( i == equation.Length - 1 && equation[i] == '#'))
                {
                    if (equation[i] != '#')
                    {
                        signs.Add(equation[i]);
                    }
                    
                    if (num != "")
                    {
                        equation = equation.Remove(0, i + 1);
                        if (num != "#")
                        {
                            AddElement(num, sign);
                        }
                        else
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
                            AddElement(num, sign);
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
        private void AddElement(string num, int sign)
        {
            
            if (num == "pi")
            {
                elements.Add(new Number(Math.PI, sign));
            }
            else if (num == "e")
            {
                elements.Add(new Number(Math.E, sign));
            }
            else if(num.Length == 1 && Letters.letters.Contains(num[0]))
            {
                elements.Add(new Variable(num));
            }
            else
            {
                elements.Add(new Number(num, sign));
            }
        }
        public virtual double ReturnValue()
        {
            double value = 0;
            
            for (int i = 0; i < elements.Count; i++)
            {
                char prevSign = signs[i];
                double var = elements[i].ReturnValue();
                if (i+ 1 < signs.Count && (signs[i+1] == '*' || signs[i+1] == '/'))
                {
                    i++;
                    double tempVar = var;
                    bool goOn = true;
                    for (; i < elements.Count && goOn; i++)
                    {
                        if (signs[i] == '*')
                        {
                            tempVar *= elements[i].ReturnValue();
                        }
                        else if (signs[i] == '/')
                        {
                            tempVar /= elements[i].ReturnValue();
                        }
                        else
                        {
                            i-=2;
                            goOn = false;
                        }
                    }
                    var = tempVar;
                }
                switch (prevSign)
                {
                    case '+':
                        value += var;
                        break;
                    case '-':
                        value -= var;
                        break;
                    default:
                        break;
                }

            }
            return sign*value;
        }

        public void Tfest()
        {
            throw new NotImplementedException();
        }
    }
}
