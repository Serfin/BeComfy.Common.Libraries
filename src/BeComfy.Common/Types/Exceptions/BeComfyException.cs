using System;

namespace BeComfy.Common.Types.Exceptions
{
    public class BeComfyException : Exception
    {
        public BeComfyException()
        {

        }

        public BeComfyException(string message) : base(message)
        {

        }

        public BeComfyException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}