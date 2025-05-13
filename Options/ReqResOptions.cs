using System.ComponentModel.DataAnnotations;

namespace Homework1.Options
{
	public class ReqResOptions
	{
		public required string BaseUrl { get; set; }
		public required string ApiKey { get; set; }
	}
}
