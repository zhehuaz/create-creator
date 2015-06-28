using HappyFactory.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HappyFactory.Exceptions;
using HappyFactory.Paint;

namespace HappyFactory
{
    /// <summary>
    /// The factory contains a truck, several machines and a repo.
    /// 
    /// The factory is set using builder pattern.
    /// Builder is prepared to initialize the factory.
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// Machines in the factory.
        /// </summary>
        public List<Machine> Machines { get; set; }

        /// <summary>
        /// Truck to deliver job resources.
        /// </summary>
        Truck _deliveryTruck = null;
        Truck DeliveryTruck 
        { 
            get { return _deliveryTruck; }
            set { _deliveryTruck = value; }
        }

        /// <summary>
        /// Repo to store jobs.
        /// </summary>
        Repo _storeRepo = null;
        Repo StoreRepo
        {
            get { return _storeRepo; }
            set { _storeRepo = value; }
        }

        /// <summary>
        /// Show alarm when amount is out of boundary.
        /// </summary>
        Alarm alarm;

        /// <summary>
        /// To draw the elements in factory.
        /// </summary>
        Painter Display { get; set; }

        public delegate void OnStartEventHandler(Object sender, EventArgs args);
        public event OnStartEventHandler OnStart;

        private Factory() { }

        /// <summary>
        /// Make the factory run.
        /// </summary>
        public void Run()
        {
            OnStart(this, null);
            DeliveryTruck.StartToDeliver();
        }

        /// <summary>
        /// Factory builder.
        /// </summary>
        public class Builder 
        {
            private Factory factory;

            public Builder()
            {
                factory = new Factory();
                factory.alarm = new Alarm();
                factory.Display = null;
            }

            /// <summary>
            /// Add a machine into factory.
            /// The process of newing a machine should be hidden
            /// because one machine need to know its last one
            /// where the jobs come from.
            /// </summary>
            /// <param name="strength"> The strength of the machine.</param>
            /// <returns> Builder itself.</returns>
            public Builder AddMachine(int strength)
            {
                if (factory.StoreRepo != null)
                    throw new RepoAlreadySetException();
                Machine newMachine = null;
                if (factory.Machines == null)
                {
                    factory.Machines = new List<Machine>();
                    if (factory.DeliveryTruck != null)
                    {
                        newMachine = new Machine(strength, new Position(10,20), factory.DeliveryTruck);
                    }
                    else
                        throw new NoTruckException();
                }
                else
                {
                    newMachine = new Machine(strength, new Position(5 * factory.Machines.Count, 20), factory.Machines.Last());
                }

                if(newMachine != null) 
                {
                    newMachine.Amount += factory.alarm.AmountAlert;
                    factory.Machines.Add(newMachine);
                }
                return this;
            }

            /// <summary>
            /// Set a truck to deliver resources in the factory.<br>
            /// <b>ATTENTION</b> : There should only be one truck in the factory, 
            /// which means this function is supposed to be called no
            /// more than once.If more, only the first one is set.
            /// </summary>
            /// <returns> Builder itself.</returns>
            public Builder SetTruck()
            {
                if (factory.DeliveryTruck == null)
                {
                    factory.DeliveryTruck = new Truck();
                }
                return this;
            }

            /// <summary>
            /// Set a repository to store proceesed jobs in the factory.
            /// 
            /// <b>ATTENTION</b> : There should only be one truck in the factory,
            /// which means this function is supposed to be called no
            /// more than once.If more, only the first one is set.<br>
            /// <b>ATTENTION</b> : Repo should be set at last (after all )
            /// </summary>
            /// <returns> Builder itself.</returns>
            public Builder SetRepo()
            {
                if(factory.StoreRepo == null)
                {
                    factory.StoreRepo = new Repo(factory.Machines.Last());
                }
                return this;
            }

            /// <summary>
            /// Build the Factory with params set.
            /// </summary>
            /// <returns></returns>
            public Factory Build()
            {
                return factory;
            }
        }
    }
}
