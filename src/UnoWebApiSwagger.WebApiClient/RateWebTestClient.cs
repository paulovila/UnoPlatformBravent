using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace UnoWebApiSwagger.WebApiClient
{
    public class RateWebTestClient : IRateWebClient
    {
        public Task<Rates> GetAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(new Rates
            {
                Currencies = new ObservableCollection<Currency>()
                {
                    new Currency()
                    {
                        Code = "TST1",
                        SpotRate = 12,
                        SpotWeek = 10,
                    },
                    new Currency()
                    {
                        Code = "TST2",
                        SpotRate = 4,
                        SpotWeek = 8,
                    },
                    new Currency()
                    {
                        Code = "TST3",
                        SpotRate = 14,
                        SpotWeek = 9,
                    },
                    new Currency()
                    {
                        Code = "TST4",
                        SpotRate = 12,
                        SpotWeek = 12,
                    },
                    new Currency()
                    {
                        Code = "TST5",
                        SpotRate = 8,
                        SpotWeek = 15,
                    },
                    new Currency()
                    {
                        Code = "TST6",
                        SpotRate = 14,
                        SpotWeek = 11,
                    },
                },
                EffectiveDate = DateTime.Now,
            });
        }
    }
}