using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagementLibrary.Exceptions
{
    public class MovieHubCapacityFullException : Exception
    {
        public MovieHubCapacityFullException(string message) : base(message) { }
    }
}
