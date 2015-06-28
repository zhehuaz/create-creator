using HappyFactory.Paint;
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
                    .AddMachine(3)
                    .AddMachine(2)
                    .AddMachine(2)
                    .AddMachine(2)
                    .SetRepo()
                    .Build();
                Painter painter = new Painter(factory);
                factory.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
