using System;
using System.Runtime.Serialization;

namespace SWE1HttpServer.app.DAL
{
    class DataAccessFailedException : Exception
    {
        public DataAccessFailedException()
        {
        }

        public DataAccessFailedException(string message) : base(message)
        {
        }

        public DataAccessFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
