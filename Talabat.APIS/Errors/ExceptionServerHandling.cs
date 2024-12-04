namespace Talabat.APIS.Errors
{
    public class ExceptionServerHandling : ApiResponse
    {
        public string Details { get; set; }

        public ExceptionServerHandling(int status = 500, string? message = null, string details = "")
            : base(status, message)
        {
            Details = details;
        }
    }
}
