﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TicsaAPI.BLL;
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
            services.AddSwaggerGen();
            services.AddScoped<IBsGamme, BsGamme>();
            services.AddScoped<IDpGamme, DpGamme>();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi Routage");
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
