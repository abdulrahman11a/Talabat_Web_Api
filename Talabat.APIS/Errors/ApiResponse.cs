namespace Talabat.APIS.Errors
{
    public class ApiResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int status, string? message = null)
        {
            Status = status;
            Message = message ?? GetDefaultMessageForStatusCode(status);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request was made.",
                401 => "You are not authorized.",
                404 => "The requested resource was not found.",
                500 => "An unexpected error occurred on the server.",
                _ => "An error occurred."
            };
        }
    }
}
