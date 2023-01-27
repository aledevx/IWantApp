using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace IWantApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        [FromRoute] Guid id,
        CategoryRequest categoryRequest,
        ApplicationDbContext context) {

        var category = context.Categories.Where(c => c.Id == id).FirstOrDefault();

        if (category == null)
            return Results.NotFound();

        category.Name = categoryRequest.Name;
        category.Active = categoryRequest.Active;

        context.Categories.Update(category);
        context.SaveChanges();

        return Results.Ok();


    }
}
