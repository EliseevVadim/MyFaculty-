using System;

namespace MyFaculty.Application.Common.Exceptions
{
    public class DestructiveActionException : Exception
    {
        public DestructiveActionException(string message)
            : base(message) { }
    }
}
