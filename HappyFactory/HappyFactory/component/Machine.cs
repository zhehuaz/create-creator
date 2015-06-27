using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HappyFactory.Component
{
    public enum MachineState
    {
        IDLE,
        BUSY
    }

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
        public MachineState state { get; set; }
        Thread workingThread;
        Queue<Job> waitingJobs;

        public event AmountEventHandler Amount;
        public event SubmitEventHandler Submit;

        public Machine(int strength, ISubmitListened lastMachine)
        {
            this.id = num ++;
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
            Console.WriteLine("Machine " + id + " is processing job " + job.id + " ...");
            Thread.Sleep(job.difficulty / strength);
            //Console.WriteLine("Machine " + id + " process job " + job.id + " complete!");
            this.state = MachineState.IDLE;
        }

        public void OnNewJobReach(Object sender, SubmitEventArgs args)
        {
            if (args.job != null)
            {
                waitingJobs.Enqueue(args.job);
            }
        }
    }
}
