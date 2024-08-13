namespace ecommerce_backend.Dto
{
    public class ResponseDto
    {
        public bool Success { get; set; }
        public object? Data { get; set; }
        public List<string>? Errors { get; set; }
        public string ValidationMessage { get; set; }

        public ResponseDto(bool success, object? data, List<string>? errors, string validationMessage)
        {
            Success = success;
            Data = data;
            Errors = errors;
            ValidationMessage = validationMessage;
        }
    }
}