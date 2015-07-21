using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using HappyFactory.Paint;

namespace HappyFactory.Component
{
    /// <summary>
    /// 
    /// </summary>
    public enum MachineState
    {
        /// <summary>
        /// The job buff of this machine is empty, and the machine
        /// is waiting for the coming one.
        /// </summary>
        IDLE,

        /// <summary>
        /// Machine is handling and processing a job.
        /// </summary>
        BUSY
    }

    /// <summary>
    /// A machine is to process jobs.
    /// Each machine has a job buffer implemented with queue.
    /// Jobs processed over by the last machine would be submitted to this one, 
    /// and stored temporarily in the buffer.
    /// </summary>
    public class Machine : IAmountListened, ISubmitListened
    {
        private const int QUEUE_MAX_CONTAIN = 5;
        private const int QUEUE_MIN_CONTAIN = 1;
        private static int num = 0;
        public readonly int id;

        /// <summary>
        /// The strength of this machine.The larger its strength is,
        /// the faster it process the jobs.
        /// The strength is supposed to be between 0 and 10.
        /// </summary>
        public readonly int strength;

        /// <summary>
        /// 
        /// </summary>
        public MachineState state { get; set; }
        Thread workingThread;

        /// <summary>
        /// 
        /// </summary>
        public Queue<Job> waitingJobs
        {
            get;
            private set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public Position Pos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public event AmountEventHandler Amount;
        
        /// <summary>
        /// 
        /// </summary>
        public event SubmitEventHandler Submit;

        public Machine(int strength, Position pos, ISubmitListened lastMachine)
        {
            this.id = num ++;
            if (strength > 10)
                this.strength = 10;
            else if (strength < 1)
                this.strength = 1;
            else
                this.strength = strength;
            this.state = MachineState.IDLE;
            this.waitingJobs = new Queue<Job>();
            waitingJobs.Clear();

            workingThread = new Thread(new ThreadStart(ProcessJobs));
            workingThread.Start();

            lastMachine.Submit += OnNewJobReach;
        }

        private void ProcessJobs()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                Job curJob;
                if (waitingJobs.Count > 0)
                {
                    if (waitingJobs.Count > QUEUE_MAX_CONTAIN
                        || waitingJobs.Count < QUEUE_MIN_CONTAIN)
                        ((IAmountListened)this).OnAmountAlert(new AmountEventArgs(waitingJobs.Count));
                    curJob = waitingJobs.Dequeue();
                    process(curJob);
                    ((ISubmitListened)this).OnSubmit(new SubmitEventArgs(curJob));
                }
            }
        }

        void IAmountListened.OnAmountAlert(AmountEventArgs args)
        {
            if (Amount != null)
            {
                Amount(this, args);
            }
        }

        void ISubmitListened.OnSubmit(SubmitEventArgs args)
        {
            if (Submit != null)
            {
                Submit(this, args);
            }
        }

        private void process(Job job)
        {
            this.state = MachineState.BUSY;
            Painter.Notif("Machine " + id + " is processing job " + job.id + " ...");
            Thread.Sleep(job.difficulty / strength);
            this.state = MachineState.IDLE;
        }

        /// <summary>
        /// Called when the job processed over by the last machine comes.
        /// The new job would be stored in the buffer queue.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnNewJobReach(Object sender, SubmitEventArgs args)
        {
            if (args.job != null)
            {
                waitingJobs.Enqueue(args.job);
            }
        }
    }
}
