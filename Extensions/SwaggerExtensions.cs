using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Api.Extensions {
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SwaggerConfigOptions>(configuration.GetSection("swaggerConfig"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Covid API", Version = "v1" });
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			// services.AddSwaggerGen(c =>
			// {
			// 	c.SwaggerDoc("draft", new Info { Title = "Draft: Attachments Rest API", Version = "draft" });
			// 	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			// 	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			// 	c.IncludeXmlComments(xmlPath);
			// });

			return services;
        }

        // public static IApplicationBuilder UseSwaggerRedirection(this IApplicationBuilder app)
        // {
        //     // Or Use MVC Default controller
        //     app.Use(async (context, next) =>
        //     {
        //        if (context.Request.Path == "/")
        //        {
        //            context.Response.Redirect("/swagger");
        //        }
        //        else
        //        {
        //            await next.Invoke();
        //        }
        //    });
        //    return app;
        // }

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
        {
            var swaggerConfig = app.ApplicationServices.GetService<IOptions<SwaggerConfigOptions>>();
            var virtualPath = swaggerConfig.Value?.VirtualPath;

            app
                .UseSwagger(c =>
                {
                    c.RouteTemplate = "{documentName}/swagger.json";
                })
                .UseSwaggerUI(c =>
                {
                    var jsonPath = "/v1/swagger.json";
                    if (!string.IsNullOrEmpty(virtualPath))
                        jsonPath = $"/{virtualPath}{jsonPath}";
                    c.SwaggerEndpoint(jsonPath, "Attachments API v1");
					
					jsonPath = "/draft/swagger.json";
					if (!string.IsNullOrEmpty(virtualPath))
						jsonPath = $"/{virtualPath}{jsonPath}";
					c.SwaggerEndpoint(jsonPath, "Attachments API draft");
				});

            return app;
        }
    }

    public class SwaggerConfigOptions
    {
        public string VirtualPath { get; set; }
        public string AppIdentifier { get; set; }
        public string ApplicationHash { get; set; }
    }
    
}
