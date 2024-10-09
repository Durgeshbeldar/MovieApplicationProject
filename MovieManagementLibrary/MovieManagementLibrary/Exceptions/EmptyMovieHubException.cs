using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagementLibrary.Exceptions
{
    public class EmptyMovieHubException : Exception
    {
        public EmptyMovieHubException(string message) : base(message) { }
    }
}
