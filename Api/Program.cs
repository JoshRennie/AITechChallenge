namespace Api
{
	using Microsoft.AspNetCore;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;

	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
			              .ConfigureAppConfiguration((hostingContext, config) => { config.AddJsonFile("appsettings.json", false, true); })
			              .UseStartup<Startup>();
		}
	}
}