using IWantApp.infra.Data;

namespace IWantApp.Endpoints.Employees;

public class EmployeesGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(int ?page, int ?rows, QueryAllUsersWithClaimName query)
    {
        if (page == null)
            page = 1;
        if (rows == null || rows > 100)
            rows = 100;

        return Results.Ok(query.Execute(page.Value, rows.Value));
    }
}