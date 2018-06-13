using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Function:Equation
    {
        protected string function;
        public Function(string function, ref string equation, int sign) :base(ref equation, sign)
        {
            this.function = function;
        }
        public override double ReturnValue()
        {
            double value = base.ReturnValue();
            double ret = 0;
            switch (function)
            {
                case "sin":
                    ret = Math.Sin(value);
                    break;
                case "cos":
                    ret = Math.Cos(value);
                    break;
                case "tan":
                    ret = Math.Tan(value);
                    break;
                default:
                    break;
            }
            return sign * ret;
        }
    }
}
