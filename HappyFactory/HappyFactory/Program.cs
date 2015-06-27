using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Factory factory = new Factory.Builder()
                    .SetTruck()
                    .AddMachine(10)
                    .AddMachine(10)
                    .AddMachine(1)
                    .SetRepo()
                    .Build();
                factory.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
