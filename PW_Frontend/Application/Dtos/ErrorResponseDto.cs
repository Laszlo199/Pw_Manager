namespace Pw_Frontend.Application.Dtos {
    public class ErrorResponseDto {
        public string Type { get; }
        public int Status { get; }
        public string TraceId { get; }
        public string Error { get; }

        public ErrorResponseDto(string type, int status, string traceId, string error) {
            Type = type;
            Status = status;
            TraceId = traceId;
            Error = error;
        }
    }
}
