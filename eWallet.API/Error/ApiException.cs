namespace eWallet.API.Error
{
    public class ApiException : ApiResponse
    {
        private readonly string? Details;

        public ApiException
            (int? statusCode, string? message = null, string? details = null)
            : base(statusCode, message)
        {
            Details = details;
        }
       
    }
}
