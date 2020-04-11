namespace Azakaw.Domain
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public CurrentUser User => WorkContext.CurrentUser;

        public static ResponseModel<T> GetSuccessResponse(T data, string message = "The request has been completed successfully.")
        {
            return new ResponseModel<T>()
            {
                Data = data,
                Message = message,
                Status = true,
            };
        }

        public static ResponseModel<T> GetNotFoundResponse(string message = "No data found.")
        {
            return new ResponseModel<T>()
            {
                Message = message,
                Status = false,
            };
        }

        public static ResponseModel<T> GetValidationFailureResponse(T data, string message = "The inconsistent data has been provided.")
        {
            return new ResponseModel<T>()
            {
                Status = false,
                Message = message,
            };
        }

        public static ResponseModel<T> GetFailureResponse(T data, string message = "The request has been failed.")
        {
            return new ResponseModel<T>()
            {
                Status = false,
                Data = data,
                Message = message,
            };
        }
    }
}