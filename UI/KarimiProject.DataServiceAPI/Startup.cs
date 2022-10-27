using KarimiProject.DataServiceAPI.Classes;
using KarimiProject.ExcelManager;
using KarimiProject.Interfaces;
using KarimiProject.RedisService;
using KarimiProject.SqlService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KarimiProject.DataServiceAPI
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
            services.AddSingleton<IFileReader, ExcelReader>();
            services.AddScoped<IDataServiceFactory, DataServiceFactory>();
            services.AddScoped<RedisDataService, RedisDataService>();
            services.AddScoped<SqlDataService, SqlDataService>();

            services.AddDbContext<MainDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));

            

            services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = Configuration.GetConnectionString("LocalRedis");
                option.InstanceName = "KarimiProject-";
            });


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
