using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test3.GlobalErrorHandling.CustomExceptionMiddleware;
using Test3.GlobalErrorHandling.Extensions;
using Test3.Models;

namespace Test3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PageContext>(options => options.UseSqlServer(connection));
            services.AddMvc((options =>
            {
                options.Filters.Add(typeof(ValidatorActionFilter));
            })).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

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

            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
