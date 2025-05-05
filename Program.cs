
using Homework1.Clients;
using Homework1.Options;

namespace Homework1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			//builder.Services.AddDbContext<APIContext>(option => option.UseInMemoryDatabase("PostsDB"));

			builder.Services.AddOptions<JsonPlaceholderOptions>()
				.Bind(builder.Configuration.GetSection("JsonPlaceholder"))
				.ValidateDataAnnotations()
				.ValidateOnStart();
			builder.Services.AddHttpClient<JsonPlaceholderClient>();


			builder.Services.AddOptions<ReqResOptions>()
				.Bind(builder.Configuration.GetSection("ReqRes"))
				.ValidateDataAnnotations()
				.ValidateOnStart();
			builder.Services.AddHttpClient<ReqResClient>();


			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
