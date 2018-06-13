using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Variable:IElement
    {
        string variable;
        public Variable(string variable)
        {
            this.variable = variable;
        }

        public double ReturnValue()
        {
            return Form1.GetValue(variable);
        }

        public void Tfest()
        {
            throw new NotImplementedException();
        }
    }
}
