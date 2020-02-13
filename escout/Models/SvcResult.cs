namespace escout.Models
{
    public class SvcResult
    {
        public int errorCode { get; set; }
        public string errorMessage { get; set; }

        public SvcResult(int errorCode, string errorMessage)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;
        }

        public static SvcResult Get(int error, string message)
        {
            return new SvcResult(error, message);
        }
    }
}
