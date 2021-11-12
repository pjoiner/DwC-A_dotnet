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

        public static implicit operator bool(ConvertResult error) => error.Value;

        public static ConvertResult Success => SuccessResult;

        public static ConvertResult Failed(string message) => new ConvertResult(false, message);

        public override bool Equals(object obj) => obj is ConvertResult result &&
                   Value == result.Value;

        public static bool operator ==(ConvertResult result1, ConvertResult result2) => result1.Equals(result2);

        public static bool operator !=(ConvertResult result1, ConvertResult result2) => !result1.Equals(result2);

        public override int GetHashCode() => Value.GetHashCode();

        private static readonly ConvertResult SuccessResult = new ConvertResult(true);

        public override string ToString()
        {
            return $"{Value}: {Message}";
        }
    }

}
