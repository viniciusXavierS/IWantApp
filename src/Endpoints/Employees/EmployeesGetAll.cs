namespace IWantApp.Endpoints.Employees;

public class EmployeesGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "Employee005Policy")]
    public static async Task<IResult> Action(int ?page, int ?rows, QueryAllUsersWithClaimName query)
    {
        if (page == null)
            page = 1;
        if (rows == null || rows > 100)
            rows = 100;

        var result = await query.Execute(page.Value, rows.Value);
        return Results.Ok(result);
    }
}