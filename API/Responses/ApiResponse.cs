namespace API.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
       
        public static ApiResponse<T> SuccessResponse(T data)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Error = null
            };
        }

        public static ApiResponse<T> Failure(string error)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Data = default,
                Error = error
            };
        }
    }
}
