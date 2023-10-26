using CleanMultitenantArchitecture.Aplication.DTO;
using CleanMultitenantArchitecture.Domain.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;

namespace CleanMultitenantArchitecture.API.Handlers
{
    public class AuthorizedRolesHandler : AuthorizationHandler<AuthorizedRolesRequirement>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizedRolesHandler(IHttpContextAccessor httpContextAccessor, IUserRepository userRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepo;
        }

        protected override  Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   AuthorizedRolesRequirement requirement)
        {
            try
            {
                var rolesStr = requirement.RoleName.Split(",").ToList();

             
                List<Claim> user = context.User.Claims.Where(claim => claim.Type == "memberof").ToList();
                user = user.Where(t => rolesStr.Contains(t.Value)).ToList();

                if (user.Count > 0)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    var data = new GenericResponseDTO<UserResponseDTO>();
                    data.Status = 403;
                    data.StatusText = "User Unathorized to this path";
                    context.Fail();
                    _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    _httpContextAccessor.HttpContext.Response.ContentType = "application/json";
                    return _httpContextAccessor.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception e)
            {
                context.Fail();
            }
            return Task.CompletedTask;

        }
    }
}
