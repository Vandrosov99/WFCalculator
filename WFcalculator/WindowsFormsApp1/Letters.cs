using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    static class Letters
    {
        public static List<char> letters;
        static Letters()
        {
            letters = new List<char>();
            for (int i = 65; i < 91; i++)
            {
                letters.Add((char)i);
            }
            for (int i = 97; i < 123; i++)
            {
                letters.Add((char)i);
            }
        }
    }
}
