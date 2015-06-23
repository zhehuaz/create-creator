using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyFactory.component
{
    public delegate void AmountExceededEventHandler(Object sender, AmountExceededEventArgs args);

    public class AmountExceededEventArgs : EventArgs 
    {
        public readonly int amount;

        public AmountExceededEventArgs(int amount)
        {
            this.amount = amount;
        }
    }

    interface IAmountListened
    {
        event AmountExceededEventHandler AmountExceeded;
        void OnAmountExceeded(AmountExceededEventArgs args);
    }
}
