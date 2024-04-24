
namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int StatesCode {  get; set; }
        public string Message { get; set; }

        public ApiResponse(int statesCode , string? message = null)
        {
            StatesCode = statesCode;
            Message = message ?? GetDefaultMessageForStatesCode(statesCode);
        }

        private string? GetDefaultMessageForStatesCode(int statesCode)
        {
            return statesCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource was not found",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change",
                _ => null
            };
        }
    }
}
