using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lun2Code.Logging;
using Lun2Code.Models;
using Lun2Code.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lun2Code
{
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
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddLocalization(settings => settings.ResourcesPath = "Resources");
			
			services.AddScoped<IUsersRepository, UsersRepository>();
			/*services.AddDbContext<UsersContext>(settings
				=> settings.UseMySql(Configuration.GetConnectionString("DefaultConnection"))); // add connection namespace*/
			
			services.AddDbContext<UsersContext>(settings
				=> settings.UseMySql(Configuration.GetConnectionString("DefaultConnection2")));
						
			services.AddIdentity<User, IdentityRole>(settings => {
				settings.Password.RequiredLength         = 4;
				settings.Password.RequireDigit           = false;
				settings.Password.RequireLowercase       = false;
				settings.Password.RequireUppercase       = false;
				settings.Password.RequireNonAlphanumeric = false;
			}).AddEntityFrameworkStores<UsersContext>();

			services.Configure<RequestLocalizationOptions>(settings =>
			{
				var supportedCultures = new[]
				{
					new CultureInfo("en"),
					new CultureInfo("ru")
				};
				
				settings.DefaultRequestCulture = new RequestCulture("ru");
				settings.SupportedCultures = supportedCultures;
				settings.SupportedUICultures = supportedCultures;

			});

			services.AddTransient<IEmailService, EmailService>();
			
			services.AddMvc()
				.AddViewLocalization().
				AddDataAnnotationsLocalization().
				SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			var settings = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization(settings.Value);
			
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseAuthentication();
			app.UseStatusCodePages();
			app.UseHttpsRedirection();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		
			var path = Configuration.GetSection("Logging").GetSection("LogFile").Value;
			
			File.WriteAllText(path, string.Empty);
			
			loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), path));
			
		}
	}
}