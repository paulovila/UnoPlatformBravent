using System;
using System.Collections.ObjectModel;
using UnoWebApiSwagger.WebApiClient;

namespace UnoWebApiSwagger.ViewModels
{
    public class MainPageViewModelDesign : MainPageViewModel
    {
        public MainPageViewModelDesign() : base(null)
        {
            Rates = new Rates
            {
                EffectiveDate = DateTime.Now,
                Currencies = new ObservableCollection<Currency>(new[]
                {
                    new Currency { Code = "USD", SpotRate = 1M , SpotWeek = 1.0M},
                    new Currency { Code = "JPY", SpotRate = 0.23M,SpotWeek = 0.3M },
                    new Currency { Code = "EUR", SpotRate = 0.23M ,SpotWeek = 0.1M},
                })
            };
        }
    }
}