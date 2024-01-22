using Dapper;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQL"));
        }

        private readonly IDbConnection _dbConnection;

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("Select *from discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }
        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("select *from discount where id = @id", new { Id = id })).SingleOrDefault();
            if (discount == null)
                return Response<Models.Discount>.Fail("Discount not found", 404);
            return Response<Models.Discount>.Success(discount,200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userid)
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("select *from discount where userid=@UserId and code=@Code", new { UserId = userid, Code = code });
            var hasDiscount = discounts.FirstOrDefault();
            if (hasDiscount == null)
                return Response<Models.Discount>.Fail("Discount not found", 404);
            return Response<Models.Discount>.Success(hasDiscount,200);
        }


        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount(userid,rate,code) VALUES(@UserId,@Rate,@Code)", discount);
            if (saveStatus > 0)
                return Response<NoContent>.Success(204);
            return Response<NoContent>.Fail("an error accured while adding", 500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            return await _dbConnection.ExecuteAsync("update discont set userid=@UserId, code=@Code, rate=@Rate where id=@Id",
                new { Id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Rate }) > 0 ?
                Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", 500);
        }
        public async Task<Response<NoContent>> Delete(int id)
        {
            return await _dbConnection.ExecuteAsync("DELETE discount where id=@Id", new { Id = id }) > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found",404);
        }

    }
}
