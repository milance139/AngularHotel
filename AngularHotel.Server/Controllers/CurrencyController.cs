using AngularHotel.Shared.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularHotel.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        [Route("get-all-currencies")]
        public async Task<ActionResult<ServiceResponse<List<Currency>>>> GetAllCurrencies()
        {
            var result = await _currencyService.GetAllCurrencies();

            return Ok(result);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Currency>>> CreateCurrency(Currency currency)
        {
            var result = await _currencyService.CreateCurrency(currency);

            return Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Currency>>> UpdateRoom(Currency currency)
        {
            var result = await _currencyService.UpdateCurrency(currency);

            return Ok(result);
        }

        [HttpDelete("{currencyId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCurrency(int currencyId)
        {
            var result = await _currencyService.DeleteCurrency(currencyId);

            return Ok(result);
        }
    }
}
