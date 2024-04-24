namespace Talabat.APIs.Errors
{
    public class ApiExeptionResponse : ApiResponse
    {
        public string? Details { get; set; }

        public ApiExeptionResponse (int StatesCode, string? message = null, string? details = null) 
                                  : base(StatesCode , message)
        {
            Details = details;
        }
    }
}
