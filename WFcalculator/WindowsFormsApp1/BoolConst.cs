using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class BoolConst:IElem<bool>
    {
        bool value;
        public BoolConst(bool value, bool sign)
        {
            this.value = BoolEquation.Conjunction(value,sign);
        }
        public bool ReturnValue()
        {
            return value;
        }

        public void Tfest()
        {
            throw new NotImplementedException();
        }
    }
}
