using Azure;
using CleanMultitenantArchitecture.Aplication.DTO;
using CleanMultitenantArchitecture.Domain.Entities;
using CleanMultitenantArchitecture.Domain.IRepositories;
using CleanMultitenantArchitecture.Infraestructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;


namespace CleanMultitenantArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMigrationTenantScriptService _migrationTenantScriptService;

        public OrganizationController(IOrganizationRepository organizationRepository,
             IMigrationTenantScriptService migrationTenantScriptService)
        {
            _migrationTenantScriptService = migrationTenantScriptService;
            _organizationRepository = organizationRepository;
        }

        [Authorize(Policy = "admin")]
        [HttpPost]
        public async Task<IActionResult> createOrganization(CreateOrganizationDTO data)
        {
            var response = new GenericResponseDTO<Organization>();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.StatusText = "Bad Request";
                    response.Status = 400;
                    return BadRequest(response);
                }
                var org = new Organization()
                {
                    Name = data.Name,
                    Server = data.Server,
                    Password = data.Password,
                    User = data.User
                };
                _migrationTenantScriptService.runScript(org);
                //IT SHOULD HANDLE ERRORS 
                await _organizationRepository.AddAsync(org);


            }
            catch (Exception ex)
            {
                response.StatusText = "Unexpected error has occurred";
                response.Status = 500;
                response.DetailedStatus = $"Exception : {ex.InnerException?.StackTrace} " +
                 $"{ex.InnerException.Message} {ex.InnerException?.InnerException} {ex.Source}";

                return StatusCode(500, response);

            }
            return Ok(response);
        }


    }
}
