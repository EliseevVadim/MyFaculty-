using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Common.Exceptions
{
    public class UnauthorizedActionException : Exception
    {
        public UnauthorizedActionException(string message)
            : base(message) { }
    }
}
