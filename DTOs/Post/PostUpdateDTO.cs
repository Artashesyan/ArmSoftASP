namespace Homework1.DTOs.Post
{
	public class PostUpdateDTO
	{
		public required string Title { get; set; }
		public required string Body { get; set; }
		public int UserId { get; set; }
	}
}
