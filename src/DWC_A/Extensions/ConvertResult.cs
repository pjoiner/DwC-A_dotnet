namespace DwC_A.Extensions
{
    public struct ConvertResult
    {
        public readonly bool Value;

        public readonly string Message;

        public ConvertResult(bool value, string message = "")
        {
            Value = value;
            Message = message;
        }

        public static implicit operator bool(ConvertResult error)
        {
            return error.Value;
        }

        public static ConvertResult Success
        {
            get
            {
                return SuccessResult;
            }
        }

        public static ConvertResult Failed(string message)
        {
            return new ConvertResult(false, message);
        }

        private static ConvertResult SuccessResult = new ConvertResult(true);
    }

}
