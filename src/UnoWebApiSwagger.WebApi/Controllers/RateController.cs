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
                    new Currency {Code = "JPY", SpotRate = Random, SpotWeek = Random},
                    new Currency {Code = "EUR", SpotRate = Random, SpotWeek = Random}
                }
            });
        }
        private decimal Random => new Random().Next(0, 100) / 100M;
    }
}