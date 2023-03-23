using Entries.Api.Models;
using Entries.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.WebRequestMethods;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Entries.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly IApiService _apiService;
        private readonly ILogger<EntriesController> _logger;
        public EntriesController(IApiService apiService, ILogger<EntriesController> logger)
        {
            _apiService = apiService;
            _logger = logger;
        }

       
        [HttpPost("GetHTTPS")]
        public async Task<IActionResult> Get( FilterHTTPSModel filter )
        {
            Response responseEntries = await _apiService.GetListEntriesAsync<EntryModel>();

            try
            {
                if (responseEntries == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, "Ocurrio un errro intentelo mas tarde.");
                }
                EntryModel res = (EntryModel)responseEntries.Result;
                List<EntriesItemModel> itemlist = res.entries;

                var item = itemlist.Where(x => x.HTTPS == filter.HTTPS).ToList();
                responseEntries.Result = item;
                return StatusCode(StatusCodes.Status200OK, responseEntries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                return BadRequest(ex.Message);
            }
        }

        
        [HttpGet("CategoryDistinct")]
        public async Task<IActionResult> Category()
        {
            Response responseEntries = await _apiService.GetListEntriesAsync<EntryModel>();
            try
            {

                if (responseEntries == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, "Ocurrio un errro intentelo mas tarde.");
                }

                EntryModel res = (EntryModel)responseEntries.Result;
                List<EntriesItemModel> itemlist = res.entries;

                var distinctCities = (from p in itemlist
                                      select p.Category).Distinct();

                responseEntries.Result = distinctCities;
                return StatusCode(StatusCodes.Status200OK, responseEntries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                return BadRequest(ex.Message);
            }
        }


    }
}
