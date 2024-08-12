using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Papara.CaptainStore.Application.Services.ElasticSearchProductService;

namespace Papara.CaptainStore.API.Controllers
{
    [ApiController]
    [Route("api/elasticsearch/products/[action]")]
    public class ElasticsearchProductController : ControllerBase
    {
        private readonly IElasticsearchProductService _elasticsearchProductService;

        public ElasticsearchProductController(IElasticsearchProductService elasticsearchProductService)
        {
            _elasticsearchProductService = elasticsearchProductService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> IndexAllProductsAsync()
        {
            try
            {
                var result = await _elasticsearchProductService.IndexAllProductsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(int from = 0, int size = 100)
        {
            try
            {
                var products = await _elasticsearchProductService.GetAllProductsAsync(from, size);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByFieldName(string fieldName, string fieldValue, int from = 0, int size = 10)
        {
            try
            {
                var products = await _elasticsearchProductService.GetProductsByFieldNameAsync(fieldName, fieldValue, from, size);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
