using System.Linq;
using Windows.UI.Xaml;
using SkiaSharp;
using SkiaSharp.Views.UWP;
using UnoWebApiSwagger.WebApiClient;

namespace UnoWebApiSwagger.Shared
{
    public sealed partial class GraphControl
    {
        public GraphControl()
        {
            InitializeComponent();
        }

        void OnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            var points = GetPoints();
            var path = new SKPath();

            var (startX, startY) = points.First();

            path.MoveTo(startX, startY);
            foreach (var (x, y) in points)
                path.LineTo(x, y);

            var strokePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Red,
                StrokeWidth = 1,
                FilterQuality = SKFilterQuality.High,
                IsAntialias = true,
            };

            canvas.DrawPath(path, strokePaint);
        }

        private (float, float)[] GetPoints() => new[]
        {
            (0f, (float)Currency.SpotRate),
            (7f, (float)Currency.SpotWeek),
            (30f, (float)Currency.SpotMonth)
        };


        public static readonly DependencyProperty CurrencyProperty = DependencyProperty.Register(
            "Currency", typeof(Currency), typeof(GraphControl), new PropertyMetadata(default(Currency)));

        public Currency Currency
        {
            get => (Currency)GetValue(CurrencyProperty);
            set => SetValue(CurrencyProperty, value);
        }
    }
}
