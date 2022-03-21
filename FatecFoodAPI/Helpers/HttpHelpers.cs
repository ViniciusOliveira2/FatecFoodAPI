namespace FatecFoodAPI.Helpers
{
    public class DefaultResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public object Error { get; set; }
        
    }
}