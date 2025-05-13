namespace Homework1.DTOs.User
{
    public class UserReadDTO
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Avatar { get; set; }
    }
}
