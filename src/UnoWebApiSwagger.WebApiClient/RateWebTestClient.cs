using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace UnoWebApiSwagger.WebApiClient
{
    public class RateWebTestClient : IRateWebClient
    {
        public Task<Rates> GetAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new Rates
            {
                Currencies = new ObservableCollection<Currency>
                {
                    CreateCurrency("TST"),
                    CreateCurrency("TST"),
                    CreateCurrency("TST")
                },
                EffectiveDate = DateTime.Now,
            });
        }

        private Currency CreateCurrency(string code)
        {
            var spot = new Random().Next(0, 1000000);
            return new Currency
            {
                Code = code,
                SpotRate = spot,
                SpotWeek = spot * Random,
                SpotMonth = spot * Random,
                SpotMonth3 = spot * Random
            };
        }

        private static decimal Random => new Random().Next(-100, 100) / 1000M;
    }
}