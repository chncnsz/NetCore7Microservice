using FreeCourse.Services.BasketAPI.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.BasketAPI.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> Delete(string userId);
    }
}
