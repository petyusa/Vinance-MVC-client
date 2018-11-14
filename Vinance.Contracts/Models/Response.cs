namespace Vinance.Contracts.Models
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public object Error { get; set; }
        public T Data { get; set; }
    }
}