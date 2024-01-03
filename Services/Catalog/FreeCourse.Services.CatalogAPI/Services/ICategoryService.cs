using FreeCourse.Services.CatalogAPI.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.CatalogAPI.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
