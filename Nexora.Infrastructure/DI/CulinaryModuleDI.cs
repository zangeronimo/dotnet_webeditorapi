using Microsoft.Extensions.DependencyInjection;

using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Application.Requests.UseCases.Culinary.Categories;
using Nexora.Application.Requests.UseCases.Culinary.Tags;
using Nexora.Application.UseCases.Culinary.Categories;
using Nexora.Application.UseCases.Culinary.Tags;
using Nexora.Domain.Interfaces.Repository.Culinary;
using Nexora.Infrastructure.Repositories.Culinary;

namespace Nexora.Infrastructure.DI;

public static class CulinaryModuleDI
{
    public static IServiceCollection AddCulinaryModule(this IServiceCollection services)
    {
        services.AddScoped<IUseCase<GetAllCategoriesFilterRequest, PaginationResult<CategoryDto>>, GetAllCategoriesUC>();
        services.AddScoped<IUseCase<GetByIdRequest, CategoryDto>, GetCategoryByIdUC>();
        services.AddScoped<IUseCase<CreateCategoryRequest, CategoryDto>, CreateCategoryUC>();
        services.AddScoped<IUseCase<UpdateCategoryRequest, CategoryDto>, UpdateCategoryUC>();
        services.AddScoped<IUseCase<DeleteRequest, CategoryDto>, DeleteCategoryUC>();
        services.AddScoped<IUseCase<CategoryFeaturedImageRequest, CategoryDto>, CategoryFeaturedImageUC>();

        services.AddScoped<IUseCase<GetAllTagsFilterRequest, PaginationResult<TagDto>>, GetAllTagsUC>();
        services.AddScoped<IUseCase<GetByIdRequest, TagDto>, GetTagByIdUC>();
        services.AddScoped<IUseCase<CreateTagRequest, TagDto>, CreateTagUC>();
        services.AddScoped<IUseCase<UpdateTagRequest, TagDto>, UpdateTagUC>();
        services.AddScoped<IUseCase<DeleteRequest, TagDto>, DeleteTagUC>();

        // Repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        return services;
    }
}
