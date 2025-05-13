namespace Homework1.DTOs.Post
{
    public class PostReadDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
    }
}
