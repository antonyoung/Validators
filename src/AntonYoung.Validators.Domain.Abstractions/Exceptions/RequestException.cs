namespace AntonYoung.Validators.Domain.Abstractions.Exceptions
{
    public class RequestException
        : Exception
    {
        public IEnumerable<string> ErrorMessages { get; private set; }

        public RequestException(string message, IEnumerable<string> errorMessages) 
            : base(message) 
            => ErrorMessages = errorMessages;

        public RequestException(string message, IEnumerable<string> errorMessages, Exception innerException) 
            : base(message, innerException)
            => ErrorMessages = errorMessages;
    }
}