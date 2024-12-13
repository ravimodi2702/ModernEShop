namespace BasketApi.DTO
{
    public class ResponseWrapper
    {
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public object Data { get; set; }
    }
}
