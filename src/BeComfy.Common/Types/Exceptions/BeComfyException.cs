using System;

namespace BeComfy.Common.Types.Exceptions
{
    public class BeComfyException : Exception
    {
        public string Code { get; }

        public BeComfyException()
        {
        }

        public BeComfyException(string code)
        {
            Code = code;
        }

        public BeComfyException(string message, params object[] args) 
            : this(string.Empty, message, args)
        {
        }

        public BeComfyException(string code, string message, params object[] args) 
            : this(null, code, message, args)
        {
        }

        public BeComfyException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public BeComfyException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        } 
    }
}