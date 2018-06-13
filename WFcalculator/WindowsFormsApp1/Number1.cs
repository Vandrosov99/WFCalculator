using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Number1 : IElem<List<int>>
    {
        int number;
        public Number1(string digit, int sign)
        {
            int dig = 0;
            digit = digit.Replace(" ", string.Empty);
            bool result = int.TryParse(digit, out dig);
            if (result)
            {
                number = sign * dig;
            }
            else
            {
                throw new Exception("Invalid number");
            }
        }
        public Number1(int digit, int sign)
        {
            number = digit * sign;
        }

        public List<int> ReturnValue()
        {
            List<int> a = new List<int>();
            a.Add(number);
            return a;
        }

        public void Tfest()
        {
            throw new NotImplementedException();
        }
    }
}
