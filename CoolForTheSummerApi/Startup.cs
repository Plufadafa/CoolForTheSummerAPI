using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolForTheSummerApi.Mappers;
using CoolForTheSummerApi.Models;
using CoolForTheSummerApi.Services;
using CoolForTheSummerApi.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace CoolForTheSummerApi
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
            // requires using Microsoft.Extensions.Options
            services.Configure<CoolForTheSummerDatabaseSettings>(
                Configuration.GetSection(nameof(CoolForTheSummerDatabaseSettings)));

            services.AddSingleton<ICoolForTheSummerDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<CoolForTheSummerDatabaseSettings>>().Value);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cool for the summer api", Version = "v1" });
            });
            services.AddSingleton<CoolForTheSummerService>();
            services.AddSingleton<IWebScraperService, WebScraperService>();
            services.AddSingleton<IFourChanService, FourChanService>();
            services.AddSingleton<IBoardEnumValidator, BoardEnumValidator>();
            services.AddSingleton<IPostMapper, PostMapper>();
            services.AddSingleton<IImageDownloaderService, ImageDownloaderService>();
            services.AddCors(); // Make sure you call this previous to AddMvc
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(
                options => options.WithOrigins("https://coolforthesummerwebsite-xegdgjxxea-ew.a.run.app").AllowAnyMethod()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
