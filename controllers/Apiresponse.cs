namespace asp_net_ecommerce_web_api
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        public List<string>? Errors { get; set; }

        public int StatusCode { get; set; }

        public DateTime Timestamp { get; set; }

        public ApiResponse(T data, int statusCode, string message = "")
        {
            Success = true;
            Message = message;
            Data = data;
            Errors = null;
            StatusCode = statusCode;
            Timestamp = DateTime.UtcNow; // Initialize timestamp with the current UTC time
        }
    }
}
