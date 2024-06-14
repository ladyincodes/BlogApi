namespace BlogApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty ;
        public required string Email { get; set; }
        public bool Active { get; set; }
    }
}
