using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lun2Code.Logging;
using Lun2Code.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
			
			services.AddScoped<IUsersRepository, UsersRepository>();
			services.AddDbContext<UsersContext>(settings
				=> settings.UseMySql(Configuration.GetConnectionString("DefaultConnection"))); // add connection name
						
			services.AddIdentity<User, IdentityRole>(settings => {
				settings.Password.RequiredLength         = 4;
				settings.Password.RequireDigit           = false;
				settings.Password.RequireLowercase       = false;
				settings.Password.RequireUppercase       = false;
				settings.Password.RequireNonAlphanumeric = false;
			}).AddEntityFrameworkStores<UsersContext>();
			

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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