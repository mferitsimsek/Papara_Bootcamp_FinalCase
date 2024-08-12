using Microsoft.AspNetCore.Mvc;
using Papara.CaptainStore.ElasticSearch;
using Papara.CaptainStore.ElasticSearch.Models;

namespace Papara.CaptainStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ElasticSearchController : ControllerBase
    {
        private readonly IElasticSearch _elasticSearch;


        public ElasticSearchController(IElasticSearch elasticSearch)
        {
            _elasticSearch = elasticSearch;
        }


        [HttpGet]
        public IActionResult GetIndexList()
        {
            try
            {
                var indexList = _elasticSearch.GetIndexList();

                return Ok(indexList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewIndexAsync([FromBody] IndexModel indexModel)
        {
            try
            {
                var response = await _elasticSearch.CreateNewIndexAsync(indexModel);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteByElasticIdAsync([FromBody] ElasticSearchModel model)
        {
            try
            {
                var response = await _elasticSearch.DeleteByElasticIdAsync(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}