using System;

namespace Yellow.Assets.JSON
{
    public class JSONException : Exception
    {
        public enum ExceptionReason
        {
            UnexpectedSymbol,
            UnexpectedToken,
        };

        public ExceptionReason Reason { get; private set; }
        
        public JSONException(ExceptionReason reason, string message) : base(message)
        {
            Reason = reason;
        }

        public override string ToString()
        {
            return Enum.GetName(typeof(ExceptionReason), Reason) + ": " + base.ToString();
        }
    }
}
