using System.Security.Claims;
using IWantApp.Endpoints.Products;
using IWantApp.Infra.Data;

namespace IWantApp.Endpoints.Employees;

public class ProductGetById
{
    public static string Template => "/products/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action(ApplicationDbContext context, Guid id)
    {
        var product = context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);

        if (product is null)
        {
            return Results.NotFound(new { message = "Produto não encontrado!" });
        }

        var result = new ProductResponse(product.Name, product.Category.Name, product.Description, product.HasStock, product.Price, product.Active);

        return Results.Ok(result);
    }
}
