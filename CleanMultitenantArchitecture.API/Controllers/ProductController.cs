using Azure;
using CleanMultitenantArchitecture.Aplication.DTO;
using CleanMultitenantArchitecture.Domain.Entities;
using CleanMultitenantArchitecture.Domain.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;


namespace CleanMultitenantArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [Authorize(Policy = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var response = new GenericResponseDTO<Product>();

            try
            {
                if (!ModelState.IsValid)
                {
                    response.StatusText = "Bad Request";
                    response.Status = 400;
                    return BadRequest(response);
                }

                var data = await _productRepository.GetAllAsync();
                response.DataList = data;
                response.StatusText = "OK";
                response.Status = 200;

            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.StatusText = "Unexpected error has occurred";
                response.DetailedStatus = $"Exception : {ex.InnerException?.StackTrace} " +
                    $"{ex.InnerException.Message} {ex.InnerException?.InnerException} {ex.Source}";

                return StatusCode(500, response);
            }
            return Ok(response);

        }
        [Authorize(Policy = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto data)
        {
            var response = new GenericResponseDTO<Product>();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.StatusText = "Bad Request";
                    response.Status = 400;
                    return BadRequest(response);
                }

                var product = new Product()
                {
                    Name = data.Name,
                    Description = data.Description,
                    Price = data.Price,
                };

                await _productRepository.AddAsync(product);
                response.StatusText = "OK";
                response.Status = 200;
                response.Data = product;// it could be a DTO
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

        [Authorize(Policy = "admin")]
        [HttpPut]
        public async Task<IActionResult> EditProduct(CreateProductDto product)
        {
            var response = new GenericResponseDTO<Product>();
            try
            {
                var exists = await _productRepository.GetByIdAsync(product?.Id ?? 0);

                if (!ModelState.IsValid || exists == null)
                {
                    response.StatusText = "Bad Request";
                    response.Status = 400;
                    response.DetailedStatus = exists  == null? "The product doesnt exists" : "";
                    return BadRequest(response);
                }
                exists.Name = product.Name;
                exists.Description = product.Description;   
                exists.Price = product.Price;

                await _productRepository.UpdateAsync(exists);

                response.StatusText = "OK";
                response.Status = 200;
                response.Data = exists; // it could be a DTO

            }
            catch(Exception ex)
            {
                response.StatusText = "Unexpected error has occurred";
                response.Status = 500;
                response.DetailedStatus = $"Exception : {ex?.InnerException?.StackTrace} " +
                 $"{ex?.InnerException?.Message} {ex?.InnerException?.InnerException} {ex?.Source}";

                return StatusCode(500, response);
            }

            return Ok(response);


        }

        [Authorize(Policy = "admin")]
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var response = new GenericResponseDTO<Product>();
            try
            {
                await _productRepository.DeleteAsync(id);

                response.Status = 200;
                response.StatusText= "OK";

            }
            catch (Exception ex)
            {
                response.StatusText = "Unexpected error has occurred";
                response.Status = 500;
                response.DetailedStatus = $"Exception : {ex.InnerException?.StackTrace} " +
                 $"{ex.InnerException.Message} {ex.InnerException?.InnerException} {ex?.Source}";

                return StatusCode(500, response);
            }

            return Ok(response);

        }

    }
}
