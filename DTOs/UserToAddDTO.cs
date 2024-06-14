namespace BlogApi.DTOs
{
    public class UserToAddDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty ;
        public string Email { get; set; } = string .Empty ; 
        public bool Active { get; set; }
    }
}
