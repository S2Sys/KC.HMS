namespace KC.HMS.Core.Core
{
    public class Response<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }
}
