using System;
using System.Runtime.Serialization;

namespace AtTheFront
{
    internal class NotInputHotKeyDialogException : Exception
    {
        public NotInputHotKeyDialogException()
        {
        }

        public NotInputHotKeyDialogException(string message) : base(message)
        {
        }

        public NotInputHotKeyDialogException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotInputHotKeyDialogException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
