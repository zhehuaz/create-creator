using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace HappyFactory.component
{
    public class Job
    {
        static int num = 0;
        public readonly int id;

        /// <summary>
        /// The more difficulty of a job is larger, the more it takes 
        /// a machine to process the job.
        /// The difficulty is supposed to be between 1000 and 5000.
        /// </summary>
        public readonly int difficulty;

        public Job(int difficulty)
        {
            this.id = num ++;
            this.difficulty = difficulty;
        }
    }
}
