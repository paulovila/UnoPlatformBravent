using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UnoWebApiSwagger.WebApi.Controllers
{
    [Authorize]
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
                    CreateCurrency("USD"),
                    CreateCurrency("JPY"),
                    CreateCurrency("EUR")
                }
            });
        }

        private Currency CreateCurrency(string code)
        {
            var spot = new Random().Next(0, 1000000);
            return  new Currency
            {
                Code = code,
                SpotRate = spot,
                SpotWeek = spot * Random,
                SpotMonth = spot * Random
            };
        }

        private static decimal Random => new Random().Next(-100, 100) / 100M;
    }
}