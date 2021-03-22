using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using TicsaAPI.BLL.BS;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL;
using TicsaAPI.DAL.DataProvider;
using TicsaAPI.DAL.DataProvider.Interface;

namespace TicsaAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers();
            services.AddDbContext<TicsaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TicsaContext")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo());
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddScoped<IBsOrder, BsOrder>();
            services.AddScoped<IDpOrder, DpOrder>();
            services.AddScoped<IBsGamme, BsGamme>();
            services.AddScoped<IDpGamme, DpGamme>();
            services.AddScoped<IBsClient, BsClient>();
            services.AddScoped<IDpClient, DpClient>();
            services.AddScoped<IBsGammeType, BsGammeType>();
            services.AddScoped<IDpGammeType, DpGammeType>();
            services.AddScoped<IBsOrderContent, BsOrderContent>();
            services.AddScoped<IDpOrderContent, DpOrderContent>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ticsa API");
                c.RoutePrefix = "doc";
            });
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseEndpoints(routes =>
            {
                routes.MapControllers();
            });
        }
    }
}
