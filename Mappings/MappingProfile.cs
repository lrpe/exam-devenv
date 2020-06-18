using AutoMapper;
using DevEnvExam.Models;
using DevEnvExam.Resources;

namespace DevEnvExam.Mappings
{
	// Mapping profile for AutoMapper to convert between data models and API resources
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			// Domain to Resource mappings
			CreateMap<Category, CategoryResource>();
			CreateMap<Product, ProductResource>();
			CreateMap<Supplier, SupplierResource>();

			//	// Resource to Domain mappings
			CreateMap<CategoryResource, Category>();
			CreateMap<ProductResource, Product>();
			CreateMap<SupplierResource, Supplier>();

			// Save Resource to Domain mappings
			CreateMap<SaveCategoryResource, Category>();
			CreateMap<SaveProductResource, Product>();
			CreateMap<SaveSupplierResource, Supplier>();
		}
	}
}
