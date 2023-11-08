using Dapper;
using IWantApp.Endpoints.Employees;
using Microsoft.Data.SqlClient;

namespace IWantApp.infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public IEnumerable<EmployeeResponse> Execute(int page, int rows)
    {
        var db = new SqlConnection(configuration["ConnectionString:IWantDb"]);
        var query = @"SELECT Email, ClaimValue as Name
                from AspNetUsers u inner 
                Join AspNetUserClaims c 
                on u.Id = c.UserId and ClaimType = 'Name'
                order by name
                OFFSET (@page -1 ) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
       return db.Query<EmployeeResponse>(
          query,
          new { page, rows }
          );
    }
}
