using System;

namespace MyFaculty.Application.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) not found") { }

        public EntityNotFoundException(string message)
            : base(message) { }
    }
}
