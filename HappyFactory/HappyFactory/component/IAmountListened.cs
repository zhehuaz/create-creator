using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyFactory.component
{
    public delegate void AmountEventHandler(Object sender, AmountEventArgs args);

    public class AmountEventArgs : EventArgs 
    {
        public readonly int amount;

        public AmountEventArgs(int amount)
        {
            this.amount = amount;
        }
    }

    public interface IAmountListened
    {
        event AmountEventHandler Amount;
        void OnAmountAlert(AmountEventArgs args);
    }
}
