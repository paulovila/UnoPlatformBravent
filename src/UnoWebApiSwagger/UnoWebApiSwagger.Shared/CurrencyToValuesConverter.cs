using System;
using System.Linq;
using Windows.UI.Xaml.Data;
using UnoWebApiSwagger.WebApiClient;

namespace UnoWebApiSwagger.Shared
{
    class CurrencyToValuesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Currency c)
            {
                var values = new[] {c.SpotRate, c.SpotWeek}.Select(x => (double)x).ToList();
                var offset = values.Min();

                var deOffset = values.Select(x => x - offset).ToList();
                var max = deOffset.Max();

                return deOffset.Select(x =>
                {
                    var d = x / max;
                    return double.IsNaN(d) ? 0d : d;
                }).ToList();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
