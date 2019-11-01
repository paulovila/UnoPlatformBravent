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
                var values = new[] {(0d, (double)c.SpotRate), (7d, (double)c.SpotWeek), (30d, (double)c.SpotMonth)};
                //var offset = values.Select(x => x.Item2).Min();

                //var deOffset = values.Select(tuple => (tuple.Item1, tuple.Item2 - offset)).ToList();
                //var max = deOffset.Max();

                //return deOffset.Select(x =>
                //{
                //    var d = x / max;
                //    return double.IsNaN(d) ? 0d : d;
                //}).ToList();

                return values.ToList();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
