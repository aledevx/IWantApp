using System.Security.Claims;
using IWantApp.Endpoints.Products;
using IWantApp.Infra.Data;
using Microsoft.Data.SqlClient;

namespace IWantApp.Endpoints.Products;

public class ProductSoldGet
{
    public static string Template => "/products/best-sellers";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(QuerySelectBestSellersProducts query)
    {
        var result = await query.Execute();

        return Results.Ok(result);
    }
}
