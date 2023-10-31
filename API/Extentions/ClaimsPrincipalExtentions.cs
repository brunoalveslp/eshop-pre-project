using System.Security.Claims;

namespace API.Extentions;

public static class ClaimsPrincipalExtentions
{
    public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
    {
        return user?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
    }
}
