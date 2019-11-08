using System;

namespace UnoWebApiSwagger.WebApi.Controllers
{
    public class Rates
    {
        public DateTime EffectiveDate { get; set; }
        public Currency[] Currencies { get; set; }
    }

    public class Currency
    {
        public string Code { get; set; }
        public decimal SpotRate { get; set; }
        public decimal SpotWeek { get; set; }
        public decimal SpotMonth { get; set; }
        public decimal SpotMonth3 { get; set; }
        public decimal SpotMonth6 { get; set; }
    }
}