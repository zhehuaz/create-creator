using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyFactory.Exceptions
{
    /// <summary>
    /// Repo is supposed to set after all the machines.
    /// If not, the exception is thrown.
    /// </summary>
    class RepoAlreadySetException : Exception
    {
    }
}
