using Dapper;
using IWantApp.Endpoints.Products;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data;

public class QuerySelectBestSellersProducts
{
    private readonly IConfiguration configuration;

    public QuerySelectBestSellersProducts(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public async Task<IEnumerable<ProductSoldResponse>> Execute()
    {
        var db = new SqlConnection(configuration["ConnectionStrings:IWantDb"]);
        var query = @"SELECT OP.ProductsId as Id,
                    P.Name as Name,
                    COUNT(OP.ProductsId) as Quantidade
                    FROM OrderProducts as OP JOIN Products as P
                    ON P.Id = OP.ProductsId
                    GROUP BY P.Name, OP.ProductsId
                    ORDER BY OP.ProductsId DESC";

        var result = db.QueryAsync<ProductSoldResponse>(query);
        return await result;
    }
}
