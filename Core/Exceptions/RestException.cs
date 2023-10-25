using System.Net;
namespace Pw_WebApi.Exceptions {
    public class RestException : Exception{
        public HttpStatusCode Status { get; }
        public string Error { get; }
        public string Code { get; set; }

        public RestException(HttpStatusCode status, string error = null):base(error) {
            Status = status;
            Error = error;
        }

    }
}
