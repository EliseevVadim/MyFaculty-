using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Common.Exceptions
{
    public class DestructiveActionException : Exception
    {
        public DestructiveActionException(string message)
            : base(message) { }
    }
}
