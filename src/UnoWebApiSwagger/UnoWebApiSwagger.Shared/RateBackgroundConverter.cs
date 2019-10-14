using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using UnoWebApiSwagger.WebApiClient;

namespace ButchersQA.Uwp
{
    public class RateBackgroundConverter : IValueConverter
    {
        private readonly SolidColorBrush _stable = new SolidColorBrush(Colors.DodgerBlue);
        private readonly SolidColorBrush _down = new SolidColorBrush(Colors.Firebrick);
        private readonly SolidColorBrush _up = new SolidColorBrush(Colors.DarkGreen);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var currency = value as Currency;
            if (currency == null || currency.SpotRate == currency.SpotWeek) return _stable;
            return currency.SpotRate > currency.SpotWeek ? _down : _up;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}