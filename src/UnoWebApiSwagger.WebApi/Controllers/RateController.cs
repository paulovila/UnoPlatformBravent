using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UnoWebApiSwagger.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        [HttpGet]
        public Task<Rates> Get()
        {
            return Task.FromResult(new Rates
            {
                EffectiveDate = DateTime.UtcNow,
                Currencies = new[]
                {
                    new Currency {Code = "USD", SpotRate = 1M, SpotWeek = 1.0M},
                    new Currency {Code = "JPY", SpotRate = 0.23M, SpotWeek = 0.3M},
                    new Currency {Code = "EUR", SpotRate = 0.23M, SpotWeek = 0.1M}
                }
            });
        }
    }
}