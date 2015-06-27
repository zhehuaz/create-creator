using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyFactory.component
{
    public delegate void SubmitEventHandler(Object sender, SubmitEventArgs args);

    public class SubmitEventArgs : EventArgs
    {
        public readonly Job job;
        public SubmitEventArgs(Job job) {
            this.job = job;
        }
    }

    public interface ISubmitListened
    {
        event SubmitEventHandler Submit;
        void OnSubmit(SubmitEventArgs args);
    }
}
