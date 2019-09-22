using System;

namespace BeComfy.Common.Types.Exceptions
{
    public class BeComfyDomainException : Exception
    {
        public BeComfyDomainException()
        {

        }

        public BeComfyDomainException(string message) : base(message)
        {

        }

        public BeComfyDomainException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}