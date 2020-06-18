using AutoMapper;
using DevEnvExam.Helpers;
using DevEnvExam.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevEnvExam
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
			services.AddControllers();
			services.AddDbContext<ProductContext>();

			// Register services
			services.AddTransient<ICategoryService, CategoryService>();
			services.AddTransient<IProductService, ProductService>();
			services.AddTransient<ISupplierService, SupplierService>();

			// Use AutoMapper to map between API resources and data models
			services.AddAutoMapper(typeof(Startup));

			// Use Swagger/OpenAPI to generate documentation
			services.AddSwagger();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
			app.UseRouting();
			app.UseAuthorization();

			// Configure SwaggerUI
			app.UseCustomSwagger();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
