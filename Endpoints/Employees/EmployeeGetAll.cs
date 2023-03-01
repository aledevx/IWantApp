using IWantApp.Infra.Data;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {

        if(page == null)
            return Results.BadRequest("Informe o numero da página");

        if (rows == null)
            return Results.BadRequest("Informe a quantidade de linhas");

        if (rows > 5)
            return Results.BadRequest("A quantidade de linhas não pode ser maior que 5");
        if (page < 1)
            return Results.BadRequest("A página precisa ser maior que 0");

       

        return Results.Ok(query.Execute(page.Value, rows.Value));
    }
}
