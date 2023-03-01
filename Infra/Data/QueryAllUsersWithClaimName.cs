using Dapper;
using IWantApp.Endpoints.Employees;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public IEnumerable<EmployeeResponse> Execute(int page, int rows)
    {
        var db = new SqlConnection(configuration["ConnectionStrings:IWantDb"]);
        var query = @"select 
                u.Email,
                c.ClaimValue as Name
                from AspNetUserClaims as c
                join AspNetUsers as u
                ON c.UserId = u.Id
                WHERE c.ClaimType = 'Name'
                order by name
                OFFSET (@page -1 ) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
        return db.Query<EmployeeResponse>(
            query,
            new { page, rows }
            );
    }
}
