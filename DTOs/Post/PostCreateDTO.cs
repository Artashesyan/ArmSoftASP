namespace Homework1.DTOs.Post
{
	public class PostCreateDTO
	{
		public required string Title { get; set; }
		public required	string Body { get; set; }
		public int UserId { get; set; }
	}
}
