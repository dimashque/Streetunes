using System.Security.Claims;

namespace Streetunes
{
    public static class ClaimsPrincipalExtension
    {

        /*
       * When a user logs in, ASP.NET Core generates a ClaimsPrincipal object, which consists of one or more ClaimsIdentity objects. These identities contain claims that store user-related information.

          A claim is simply a piece of information about the user. Some common claim types include:

          ClaimTypes.Name → Stores the username
           ClaimTypes.Email → Stores the email
          ClaimTypes.NameIdentifier → Stores the unique user ID
           ClaimTypes.Role → Stores the role of the user (e.g., Admin, User)
       */
        public static string GetUserID(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
