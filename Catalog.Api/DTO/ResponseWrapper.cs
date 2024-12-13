namespace Catalog.Api.DTO
{
    public class ResponseWrapper
    {
        public ResponseWrapper(string statusCode, string error, object data)
        {
            StatusCode = statusCode;
            Error = error;
            Data = data;
        }
        public string StatusCode { get; set; }
        public string Error { get; set; }
        public object Data { get; set; }
    }
}
