using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UnoWebApiSwagger.WebApiClient;

namespace ButchersQA.Uwp
{
    public class IconTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is Currency c)
            {
                if (c.SpotRate == c.SpotWeek)
                {
                    return StableTemplate;
                }

                return c.SpotRate > c.SpotWeek ? DownTemplate : UpTemplate;
            }

            return base.SelectTemplateCore(item, container);
        }

        public DataTemplate StableTemplate { get; set; }
        public DataTemplate UpTemplate { get; set; }
        public DataTemplate DownTemplate { get; set; }
    }
}