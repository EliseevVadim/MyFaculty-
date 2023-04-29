using System;

namespace MyFaculty.Application.Common.Exceptions
{
    public class UnauthorizedActionException : Exception
    {
        public UnauthorizedActionException(string message)
            : base(message) { }
    }
}
