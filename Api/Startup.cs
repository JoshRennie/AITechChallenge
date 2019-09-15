namespace Api
{
	using Core.Entities;
	using Core.Utilities;
	using DataLayer.DAOs;
	using DataLayer.Interfaces;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Middleware;
	using ServiceLayer.Interfaces;
	using ServiceLayer.ServiceObjects;

	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddSingleton<IBaseService<Job>, JobsService>();
			services.AddSingleton<IBaseService<Item>, ItemsService>();
			services.AddSingleton<IBaseDao<Job>, JobsDao>();
			services.AddSingleton<IBaseDao<Item>, ItemsDao>();

			//Since the website will be requesting from a different port/domain, we need to enable CORS
			services.AddCors(c => c.AddDefaultPolicy(builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

			PopulateAppSettings();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseCors();

			app.UseMiddleware<BasicAuthMiddleware>();

			app.UseHttpsRedirection();
			app.UseMvc();
		}

		/// <summary>
		/// Populates the static application settings class so that it can be accessed anywhere.
		/// </summary>
		private void PopulateAppSettings()
		{
			//Usually this would come from the DB but to save time, I've put it in the config'
			AppSettings.LabourCost = decimal.Parse(Configuration.GetSection("AppSettings")["LabourCost"]);
		}
	}
}