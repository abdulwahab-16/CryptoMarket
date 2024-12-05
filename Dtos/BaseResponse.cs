namespace RealEstate_Mvc_.Dtos
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }
    }
}
