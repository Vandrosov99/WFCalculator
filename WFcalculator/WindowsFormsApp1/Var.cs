using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Var:IElem<List<int>>
    {
        double rodipxam;
        int power, sign;
        public Var(string num, int sign)
        {
            this.sign = sign;
            num = num.Replace(" ", string.Empty);
            num = num.Remove(0, 2);
            power = int.Parse(num);
        }

        public List<int> ReturnValue()
        {
            List<int> a = new List<int>();
            for (int i = 0; i < power; i++)
            {
                a.Add(0);
            }
            a.Add(sign);
            return a;
        }

        public void Tfest()
        {
            throw new NotImplementedException();
        }
    }
}
