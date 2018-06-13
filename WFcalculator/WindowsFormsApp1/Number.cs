 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Number:IElement
    {
        double asd;
        double rodipxam;
        double schetchik = 0;
        double num;
        public Number(string digit, int sign)
        {
            double some = 0;
            digit = digit.Replace('.', ',');
            bool result = double.TryParse(digit, out some);
            if (result)
            {
                num = sign * some;
            }
            else
            {
                throw new Exception("Invalid number");
            }
        }
        public Number(double digit, int sign)
        {
            num = digit * sign;
        }
        
        public double ReturnValue()
        {
            return num;
        }

        public void Tfest()
        {
            throw new NotImplementedException();
            Console.WriteLine("Uleti");
        }
       public void Useless()
        {
            Console.WriteLine("uletaem v mir ANALIZA");
        }
    }
}
