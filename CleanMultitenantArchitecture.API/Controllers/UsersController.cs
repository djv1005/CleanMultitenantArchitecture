using CleanMultitenantArchitecture.Aplication.DTO;
using CleanMultitenantArchitecture.Aplication.Jwt;
using CleanMultitenantArchitecture.Domain.IRepositories;
using CleanMultitenantArchitecture.Infraestructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanMultitenantArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IJwtService _jwtService;
        private IUserRepository _userRepository;

        public UsersController(IJwtService jwtService,
            IUserRepository userRepo)
        {
            _jwtService = jwtService;
            _userRepository = userRepo;
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserDTO user)
        {
            var response = new GenericResponseDTO<UserResponseDTO>();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.StatusText = "BadRequest";
                    response.Status = 400;
                    return BadRequest(response);
                }
                var result = await _userRepository.validateLoggingAsync(user.email, user.password);
                

                if(result != null)
                {
                    response.Data = new UserResponseDTO();

                    var tenants = result.UserOrganizations.Select(t => new TenantDTO()
                    {
                        SlugTenant = t.Organization.Name
                    });
                    response.Data.Tenant = tenants.ToList();
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, result.Email),
                        new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
                        new Claim("memberof", "admin")
                        //it should have the different rolename of the user relationated with
                        //an entity Role

                    };
                    response.Data.AccessToken =  _jwtService.GenerateToken(claims);
                }
                else
                {
                    response.StatusText = "BadRequest";
                    response.DetailedStatus = "Username or Password incorrect";
                    response.Status = 400;
                    return BadRequest(response);
                }

            }
            catch(Exception ex)
            {
                response.StatusText = "Unexpected error has occurred";
                response.Status = 500;
                response.DetailedStatus = $"Exception : {ex.InnerException?.StackTrace} " +
                 $"{ex?.InnerException?.Message} {ex.InnerException?.InnerException} {ex?.Source}";

                return StatusCode(500,response);
            }

            return Ok(response);

        }


       /* [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserDTO user)
        {

        }*/

    }

}
