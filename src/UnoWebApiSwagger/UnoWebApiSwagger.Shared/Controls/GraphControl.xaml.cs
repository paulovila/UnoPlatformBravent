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
#if __WASM__
            DataContextChanged += (s, e) => Invalidate();
#endif
        }

        private void Invalidate() => SkXamlCanvas.Invalidate();

        void OnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            var canvas = args.Surface.Canvas;
            canvas.Clear();
            var points = GetPoints();
            var spot = points.First().Item2;
            float spotFactor = 0.5f / (spot * Tolerance);
            float width = args.Info.Width - 2f;
            float height = args.Info.Height;

            float maxDaysFactor = width / points.Last().Item1;
            var translatedPoints = points.Skip(1).Select(t => (t.Item1 * maxDaysFactor, height * (t.Item2 * spotFactor + 0.5f)));

            float previousY = height * 0.5f;
            float previousX = 0f;
            var xAxisPath = new SKPath();
            xAxisPath.MoveTo(0, height * 0.5f);
            xAxisPath.LineTo(width, height * 0.5f);
            canvas.DrawPath(xAxisPath, Paints.Gray);

            foreach (var (x, y) in translatedPoints)
            {
                var path = new SKPath();
                path.MoveTo(previousX, previousY);
                path.LineTo(x, y);
                canvas.DrawPath(path, previousY > y ? Paints.Green : Paints.Red);

                var dayPath = new SKPath();
                dayPath.MoveTo(x, 0);
                dayPath.LineTo(x, height);
                canvas.DrawPath(dayPath, Paints.Gray);

                (previousX, previousY) = (x, y);
            }
        }

        private (float, float)[] GetPoints() => new[]
        {
            (0f, (float)Currency.SpotRate),
            (7f, (float)Currency.SpotWeek),
            (30f, (float)Currency.SpotMonth),
            (90f, (float)Currency.SpotMonth3)
        };

        public static readonly DependencyProperty CurrencyProperty = DependencyProperty.Register(
            "Currency", typeof(Currency), typeof(GraphControl), new PropertyMetadata(default(Currency)));

        public Currency Currency
        {
            get => (Currency)GetValue(CurrencyProperty);
            set => SetValue(CurrencyProperty, value);
        }

        public static readonly DependencyProperty ToleranceProperty = DependencyProperty.Register(
            "Tolerance", typeof(float), typeof(GraphControl),
            new PropertyMetadata(0.1f, (d, e) => (d as GraphControl).Invalidate()));


        public float Tolerance
        {
            get => (float)GetValue(ToleranceProperty);
            set => SetValue(ToleranceProperty, value);
        }
    }
}