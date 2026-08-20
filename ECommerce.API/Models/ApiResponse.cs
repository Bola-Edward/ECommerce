namespace ECommerce.API.Models
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        private ApiResponse(bool isSuccess, int statusCode, T? data, string? message, List<string>? errors)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Data = data;
            Message = message;
            Errors = errors;
        }

        public static ApiResponse<T> Success(T data, int statusCode = 200, string? message = null)
        {
            return new ApiResponse<T>(true, statusCode, data, message, null);
        }


        public static ApiResponse<T> Failure(int statusCode, string message, List<string>? errors = null)
        {
            return new ApiResponse<T>(false, statusCode, default, message, errors);
        }

    }
}
