using System;
using System.Runtime.Serialization;

namespace SWE1HttpServer
{
    [Serializable]
    internal class DuplicateUserException : Exception
    {
        public DuplicateUserException()
        {
        }

        public DuplicateUserException(string message) : base(message)
        {
        }

        public DuplicateUserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}