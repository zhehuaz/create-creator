using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyFactory.component
{
    class Alarm
    {
        public void AmountAlert(Object sender, AmountEventArgs args)
        {
            Console.WriteLine("Opps ---- Machine " + ((Machine) sender).id + " is too buuusy!");
            Console.WriteLine("There are " + args.amount + " jobs in the machine!");
        }
    }
}
