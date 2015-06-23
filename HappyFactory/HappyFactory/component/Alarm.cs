using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyFactory.component
{
    class Alarm
    {
        public void Alert(int param)
        {
            Console.WriteLine("Opps ----" + param);
        }
    }
}
