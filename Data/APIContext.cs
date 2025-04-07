using Homework1.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework1.Data
{
	public class APIContext(DbContextOptions<APIContext> options) : DbContext(options)
	{
		public DbSet<Post> Posts { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
