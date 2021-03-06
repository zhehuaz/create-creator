﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using HappyFactory.Paint;

namespace HappyFactory.Component
{
    class Truck : ISubmitListened
    {
        Thread generator;
        Random rand;
        public event SubmitEventHandler Submit;
        
        public Truck() 
        {
            generator = new Thread(new ThreadStart(DeliverJobs));
            rand = new Random();
        }

        private void DeliverJobs()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                Thread.Sleep(rand.Next(700, 2000));
                OnSubmit(new SubmitEventArgs(new Job(rand.Next(1000, 3000))));
            }
        }

        public void OnSubmit(SubmitEventArgs args)
        {
            object obLock = new object();
            lock (obLock)
            {
                Painter.Notif("Dee~~ New job " + args.job.id + " comes!");
            }
            if (Submit != null)
            {
                Submit(this, args);
            }
        }

        public void StartToDeliver()
        {
            generator.Start();
        }
    }
}
