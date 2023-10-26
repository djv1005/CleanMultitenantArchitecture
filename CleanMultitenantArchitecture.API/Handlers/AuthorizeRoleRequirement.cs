using Microsoft.AspNetCore.Authorization;

namespace CleanMultitenantArchitecture.API.Handlers
{
    public class AuthorizedRolesRequirement : IAuthorizationRequirement
    {
        public string RoleName { get; }

        public AuthorizedRolesRequirement(string roleName)
        {
            RoleName = roleName;
        }
    }
}
