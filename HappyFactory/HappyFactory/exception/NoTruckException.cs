using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyFactory.exception
{
    /// <summary>
    /// Truck is supposed to be created before all the machines.
    /// If not, this exception is thrown.
    /// </summary>
    class NoTruckException : Exception
    {
    }
}
