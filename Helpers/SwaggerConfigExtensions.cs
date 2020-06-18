using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace DevEnvExam.Helpers
{
	// Contains extension methods for configuring Swagger/OpenAPI for use in Startup.cs
	public static class SwaggerConfigExtensions
	{
		public static void AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Varekatalog",
					Description = "Dette API håndterer varer for Rema 1000.",
					Version = "v1"
				});

				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				options.IncludeXmlComments(xmlPath);
			});
		}

		public static void UseCustomSwagger(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.RoutePrefix = string.Empty;
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Varekatalog");
			});
		}
	}
}
