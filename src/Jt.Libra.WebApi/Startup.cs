using System.IO;
using Jt.Libra.Core.Dependency;
using Jt.Libra.Core.Logging.Log4Net;
using Jt.Libra.WebApi.Attrbutes;
using Jt.Libra.WebApi.Filters;
using Jt.Libra.WebApi.MiddleWare;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace Jt.Libra.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JsonSerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
            };
        }

        public IConfiguration Configuration { get; }

        private JsonSerializerSettings JsonSerializerSettings { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = JsonSerializerSettings.ContractResolver;
                    options.SerializerSettings.DateFormatString = JsonSerializerSettings.DateFormatString;
                })
                .AddMvcOptions(options =>
                {
                    options.Filters.Add(typeof(WrapResultFilter));
                });

            // cors
            services.AddCors(action => action.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowCredentials();
            }));

            // swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Jt.Libra", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), "Jt.Libra.WebApi.xml"));
                c.OperationFilter<SwaggerParameterFilter>();
            });

            // TODO: liftStyle config
            services.Init("Jt.Libra.Repository", "Jt.Libra.Application");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net("Config/log4net.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jt.Libra.API V1");
            });

            // middleware
            app.UseMiddleware<ExceptionMiddleware>(JsonSerializerSettings);

            // jwt
            app.UseAuthentication();

            //app.UseCors(options => {
            //    options.WithOrigins("*")
            //    .AllowAnyHeader
            //});
            app.UseCors("CorsPolicy");

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
