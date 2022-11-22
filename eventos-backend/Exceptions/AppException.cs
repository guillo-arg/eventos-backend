namespace eventos_backend.Exceptions
{
    public class AppException : Exception
    {
        public AppException(List<string> errors, int statusCode = 500) : base("")
        {
            StatusCode = statusCode;
            Errors = errors;
        }
        public int StatusCode { get; }
        public List<String> Errors { get; set; }
    }
}
