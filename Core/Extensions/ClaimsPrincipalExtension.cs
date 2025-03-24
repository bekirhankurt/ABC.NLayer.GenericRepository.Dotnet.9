using System.Security.Claims;

namespace Core.Extensions;

public static class ClaimsPrincipalExtension
{
    public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        return claimsPrincipal.FindAll(claimType).Select(ct => ct.Value).ToList();
    }

    public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims(ClaimTypes.Role); 
    }
}