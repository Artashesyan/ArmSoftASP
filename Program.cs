
using Homework1.Clients;
using Homework1.Clients.Interfaces;
using Homework1.Options;
using Homework1.Services;
using Homework1.Services.Interfaces;
using Serilog;

namespace Homework1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.Configure<JsonPlaceholderOptions>(builder.Configuration.GetSection("JsonPlaceholder"));
			builder.Services.AddHttpClient<IJsonPlaceholderClient, JsonPlaceholderClient>();
			builder.Services.AddScoped<IPostService, PostService>();

			builder.Services.Configure<ReqResOptions>(builder.Configuration.GetSection("ReqRes"));
			builder.Services.AddHttpClient<IReqResClient, ReqResClient>();
			builder.Services.AddScoped<IUserService, UserService>();

			builder.Services.AddSingleton<IEntityService, EntityService>();

			Log.Logger = new LoggerConfiguration()
				.WriteTo.File(
					"Logs/log-.txt",
					rollingInterval: RollingInterval.Day,
					retainedFileCountLimit: 7,
					outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level:u3}] {Message:lj}{NewLine}{Exception}"
				)
				.CreateLogger();

			builder.Host.UseSerilog();

			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
