using System;
using System.Runtime.Serialization;

namespace AddressProcessing.Exceptions
{
    [Serializable]
    public class UnknownFileModeException : Exception
    {
        public UnknownFileModeException()
        {
        }

        public UnknownFileModeException(string message) : base(message)
        {
        }

        public UnknownFileModeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UnknownFileModeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}