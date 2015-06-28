using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyFactory.Component
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void SubmitEventHandler(Object sender, SubmitEventArgs args);

    /// <summary>
    /// 
    /// </summary>
    public class SubmitEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly Job job;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        public SubmitEventArgs(Job job) {
            this.job = job;
        }
    }

    /// <summary>
    /// A ISubmitListened is a general abstraction for "something who holds 
    /// something that is wanted and waited by others".
    /// In other words, A ISubmitListened is doing something with some shared
    /// resources and there are other object(s) waiting for the resource so 
    /// the object(s) listens its submitting.
    /// After the ISubmitListened finishes its work, it submit the resources
    /// it occupied to the listener(s).
    /// </summary>
    public interface ISubmitListened
    {
        /// <summary>
        /// Submit the resource.
        /// </summary>
        event SubmitEventHandler Submit;

        /// <summary>
        /// Called when the object finshes its work.
        /// </summary>
        /// <param name="args"> The resource submit lies in args.</param>
        void OnSubmit(SubmitEventArgs args);
    }
}
