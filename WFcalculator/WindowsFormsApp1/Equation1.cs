using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
        class Equation1 : IElem<List<int>>
        {
            protected int sign = 1;
            protected List<IElem<List<int>>> elements;
            protected List<char> signs;
            public Equation1(ref string Equation1, int sign)
            {
                elements = new List<IElem<List<int>>>();
                Equation1 = Equation1.Replace(" ", string.Empty);
                signs = new List<char>();
                signs.Add('+');
                this.sign = sign;
                Proceed(ref Equation1);
            }
            private void Proceed(ref string Equation1)
            {
                int sign = 1;
                string num = "";
                for (int i = 0; i < Equation1.Length; i++)
                {
                    if (Equation1[i] == '-' && i == 0)
                    {
                        sign *= -1;
                        continue;
                    }
                    if (Equation1[i] == '(')
                    {
                        Equation1 = Equation1.Remove(0, i + 1);
                        elements.Add(new Equation1(ref Equation1, sign));
                        num = "";
                        if (Equation1[0] == '-' || Equation1[0] == '+' || Equation1[0] == '*' || Equation1[0] == '/')
                        {
                            signs.Add(Equation1[0]);
                        }
                        //Equation1 = Equation1.Remove(0, 1);
                        i = -1;
                        sign = 1;
                        continue;
                    }
                    if (Equation1[i] == '-' || Equation1[i] == '+' || Equation1[i] == '*' || Equation1[i] == '/' || (i == Equation1.Length - 1 && Equation1[i] == '#'))
                    {
                        if (Equation1[i] != '#')
                        {
                            signs.Add(Equation1[i]);
                        }

                        if (num != "")
                        {
                            Equation1 = Equation1.Remove(0, i + 1);
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
                    if (Equation1[i] == ')')
                    {
                        Equation1 = Equation1.Remove(0, i + 1);
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
                    num += Equation1[i].ToString();

                }
            }
        private void AddElement(string num, int sign)
        {
            if (num.Contains("^"))
            {
                elements.Add(new Var(num, sign));
            }
            else
            {
                elements.Add(new Number1(num, sign));
            }
            
        }
            public virtual List<int> ReturnValue()
            {
                List<int> value = new List<int>();

                for (int i = 0; i < elements.Count; i++)
                {
                    char prevSign = signs[i];
                    List<int> var = elements[i].ReturnValue();
                    if (i + 1 < signs.Count && (signs[i + 1] == '*' || signs[i + 1] == '/'))
                    {
                        i++;
                        List<int> tempVar = var;
                        bool goOn = true;
                        for (; i < elements.Count && goOn; i++)
                        {
                            if (signs[i] == '*')
                            {
                                tempVar = Mult(tempVar, elements[i].ReturnValue());
                            }
                            else if (signs[i] == '/')
                            {
                                 tempVar = Divide(tempVar, elements[i].ReturnValue());
                            }
                            else
                            {
                                i -= 2;
                                goOn = false;
                            }
                        }
                        var = tempVar;
                    }
                    switch (prevSign)
                    {
                        case '+':
                            value = Sum(value, var);
                            break;
                        case '-':
                            value = Sub(value, var);
                            break;
                        default:
                            break;
                    }

                }
            for (int k = 0; k < value.Count; k++)
            {
                value[k] *= sign;
            }
            int index = value.Count - 1;
            while (index > 0 && value[index] == 0)
            {
                value.Remove(index);
                index--;
            }
            return value;
        }
        List<int> Sum(List<int> a, List<int> newA)
        {
            List<int> result = new List<int>();
            List<int> bigger = new List<int>();
            List<int> smaller = new List<int>();
            if (a.Count > newA.Count)
            {
                bigger = a;
                smaller = newA;
            }
            else
            {
                smaller = a;
                bigger = newA;
            }
            int i = 0;
            for (; i < smaller.Count; i++)
            {
                result.Add(bigger[i] + smaller[i]);
            }
            for (; i < bigger.Count; i++)
            {
                result.Add(bigger[i]);
            }
            
            return result;
        }
        List<int> Sub(List<int> a, List<int> newA)
        {
            List<int> result = new List<int>();
            List<int> bigger = new List<int>();
            List<int> smaller = new List<int>();
            int sign = 1;
            if (a.Count > newA.Count)
            {
                bigger = a;
                smaller = newA;
            }
            else
            {
                smaller = a;
                bigger = newA;
                sign = -1;
            }
            int i = 0;
            for (; i < smaller.Count; i++)
            {
                result.Add((bigger[i] - smaller[i])*sign);
            }
            for (; i < bigger.Count; i++)
            {
                result.Add(sign*bigger[i]);
            }
            return result;
        }
        List<int> Mult(List<int> a, List<int> newA)
        {
            List<int> result = new List<int>();            
            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < newA.Count; j++)
                {
                    int newDigit = a[i] * newA[j];
                    InsertIntoList(ref result, newDigit, i + j);
                }
            }
            return result;

        }
        List<int> Divide(List<int> a, List<int> newA)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < newA.Count; j++)
                {
                    if (newA[j] != 0 && a[i] != 0)
                    {
                        int newDigit = a[i] / newA[j];
                        InsertIntoList(ref result, newDigit, i - j);
                    }
                }
            }
            return result;

        }
        void InsertIntoList(ref List<int> list, int digit, int pos)
        {
            while (list.Count <= pos + 1)
            {
                list.Add(0);
            }
            list[pos] += digit;
        }

        public void Tfest()
        {
            throw new NotImplementedException();
        }
    }
}
