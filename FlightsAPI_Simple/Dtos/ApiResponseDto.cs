using System.Net;

namespace FlightsAPI_Simple.Dtos
{
    public class ApiResponseDto<T>
    {
        public bool RequestFailed { get; set; } = false;
        public HttpStatusCode ResponseCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
