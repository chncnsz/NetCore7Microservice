using FreeCourse.Services.CatalogAPI.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.CatalogAPI.Services
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<CourseDto>> GetByIdAsnyc(string id);
        Task<Response<List<CourseDto>>> GetAllByUserIdAsnyc(string userId);
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
