using System.Security.Claims;

namespace HospitalManagement.Utility;

public static class ClaimsUtility
{
    public static int ReadCurrentUserId(IEnumerable<Claim> claims)
    {
        int userId = Convert.ToInt32(claims.First(c => c.Type == "Id").Value);
        return userId;
    }

    public static string ReadCurrentUserRole(IEnumerable<Claim> claims)
    {
        string userRole = claims.First(c => c.Type == ClaimTypes.Role).Value;
        return userRole;
    }

    public static string ReadCurrentUserEmail(IEnumerable<Claim> claims)
    {
        string userEmail = claims.First(c => c.Type == "Email").Value;
        return userEmail;
    }
}
