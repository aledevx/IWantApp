using System.Security.Claims;
using IWantApp.Endpoints.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Employees;

public class ProductPut
{
    public static string Template => "/products/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(
        [FromRoute] Guid id,
        ProductRequest productRequest,
        HttpContext http,
        ApplicationDbContext context)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var product = context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);

        if (product is null)
        {
            return Results.NotFound(new { message = "Produto não encontrado!" });
        }

        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == productRequest.CategoryId);

        product.EditInfo(productRequest.Name,
         category,
         productRequest.Description,
         productRequest.HasStock,
         productRequest.Active,
         productRequest.Price,
         userId
         );

        if (!product.IsValid)
        {
            return Results.ValidationProblem(product.Notifications.ConvertToProblemDetails());
        }

        context.Products.Update(product);
        await context.SaveChangesAsync();

        return Results.Ok();
    }
}
