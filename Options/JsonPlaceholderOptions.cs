using System.ComponentModel.DataAnnotations;

namespace Homework1.Options
{
	public class JsonPlaceholderOptions
	{
		[Required]
		public string BaseUrl { get; set; } = string.Empty;
	}
}
