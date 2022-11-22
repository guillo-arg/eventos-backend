namespace eventos_backend.DTOs.Responses
{
    public class ResponseDTO
    {
        public bool Result { get; set; }
        public object Data { get; set; }
        public List<string> Errors { get; set; }
    }
}
