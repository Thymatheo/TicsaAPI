using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TicsaAPI.BLL.BS;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.Config;
using TicsaAPI.DAL.DataProvider;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;
using TicsaAPI.Securité;

namespace TicsaAPI
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin();
                                  });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                option.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            services.AddDbContext<TicsaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TicsaContext")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo());
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                var security = new Dictionary<string, IEnumerable<string>>
                    {
                        {
                        "ApiKeyAuth", new string[] { }
                        }
                    };
                c.AddSecurityDefinition("ApiKeyAuth", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name = "X-Api-Key",
                    Description = $"Standard Api Key header authentication."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "ApiKeyAuth"
                            },
                            Scheme = "API Key",
                            Name = "X-Api-Key",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
            services.Configure<AppSettingsSection>(Configuration.GetSection("AppSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthentificationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthentificationOptions.DefaultScheme;

            }).AddApiKeySupport(options => { });

            services.AddControllers();
            services.AddScoped<IBsOrder, BsOrder>();
            services.AddScoped<IDpOrder, DpOrder>();
            services.AddScoped<IBsGamme, BsGamme>();
            services.AddScoped<IDpGamme, DpGamme>();
            services.AddScoped<IBsClient, BsClient>();
            services.AddScoped<IDpClient, DpClient>();
            services.AddScoped<IBsProducer, BsProducer>();
            services.AddScoped<IDpProducer, DpProducer>();
            services.AddScoped<IBsGammeType, BsGammeType>();
            services.AddScoped<IDpGammeType, DpGammeType>();
            services.AddScoped<IBsCommentary, BsCommentary>();
            services.AddScoped<IDpCommentary, DpCommentary>();
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
            app.UseCors(MyAllowSpecificOrigins);
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseEndpoints(routes =>
            {
                routes.MapControllers();
            });
        }
    }
}
