namespace eventos_backend.DTOs.Account
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Enabled { get; set; }
    }
}
