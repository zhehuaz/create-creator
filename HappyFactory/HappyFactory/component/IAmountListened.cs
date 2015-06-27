using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyFactory.Component
{
    /// <summary>
    /// Handle amount events including amount exceeded and amount underflow.<br>
    /// Amount exceeded stands for element amount in the sender is too large, over 
    /// its contain perhaps.<br>
    /// Underflow stands for element amount in the sender is too small, like 0, 
    /// so that sender has to wait.
    /// </summary>
    /// <param name="sender"> The object that sends the amount alert signal.</param>
    /// <param name="args"> Arguements of amount information.</param>
    public delegate void AmountEventHandler(Object sender, AmountEventArgs args);
    
    /// <summary>
    /// Arguments of amount information sent via delegate.
    /// </summary>
    public class AmountEventArgs : EventArgs 
    {
        /// <summary>
        /// Amount of elements.
        /// </summary>
        public readonly int amount;

        /// <summary>
        /// Load amount number in the EventArgs class.
        /// </summary>
        /// <param name="amount"></param>
        public AmountEventArgs(int amount)
        {
            this.amount = amount;
        }
    }

    /// <summary>
    /// A IAmountListened is a general abstraction for "something whose amount is listened."
    /// If amount is too large or too small, there should be an Alarm triggered
    /// to alert the amount exception.
    /// </summary>
    public interface IAmountListened
    {
        /// <summary>
        /// Amount event.
        /// </summary>
        event AmountEventHandler Amount;

        /// <summary>
        /// Called in amount errors.
        /// </summary>
        /// <param name="args"> Set amount information in it.</param>
        void OnAmountAlert(AmountEventArgs args);
    }
}
