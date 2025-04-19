using Homework1.Data;
using Serilog;

namespace Homework1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			//builder.Services.AddDbContext<APIContext>(option => option.UseInMemoryDatabase("PostsDB"));

			builder.Services.AddHttpClient<JsonPlaceholderClient>((sp, client) =>
			{
				var config = sp.GetRequiredService<IConfiguration>();
				var baseUrl = config["JsonPlaceholder:BaseUrl"];
				client.BaseAddress = new Uri(baseUrl);
			});

			builder.Services.AddHttpClient<ReqResClient>((sp, client) =>
			{
				var config = sp.GetRequiredService<IConfiguration>();
				client.BaseAddress = new Uri(config["ReqRes:BaseUrl"]);
			});

			Log.Logger = new LoggerConfiguration()
				.WriteTo.File(
					"Logs/log-.txt",
					rollingInterval: RollingInterval.Day,
					retainedFileCountLimit: 7,
					outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level:u3}] {Message:lj}{NewLine}{Exception}"
				)
				.CreateLogger();

			builder.Host.UseSerilog();

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
