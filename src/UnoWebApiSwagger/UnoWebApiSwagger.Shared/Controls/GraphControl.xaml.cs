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
            float spotFactor = 50f / (spot * Tolerance);
            float width = args.Info.Width - 2f;
            float height = args.Info.Height;

            float maxDaysFactor = width / points.Last().Item1;
            var translatedPoints = points.Skip(1).Select(t => (t.Item1 * maxDaysFactor, height * (t.Item2 * spotFactor + 0.5f)));

            (float startX, float startY) = (0f, height * 0.5f);
            
            var axisPath = new SKPath();
            axisPath.MoveTo(0, startY);
            axisPath.LineTo(width, startY);
            canvas.DrawPath(axisPath, Paints.Gray);

            foreach (var (x, y) in translatedPoints)
            {
                var dayPath = new SKPath();
                dayPath.MoveTo(x, 0);
                dayPath.LineTo(x, height);
                canvas.DrawPath(dayPath, Paints.Gray);
                
                var path = new SKPath();
                path.MoveTo(startX, startY);
                path.LineTo(x, y);
                canvas.DrawPath(path, startY > y ? Paints.Green : Paints.Red);

                (startX, startY) = (x, y);
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
            new PropertyMetadata(10f, (d, e) => (d as GraphControl).Invalidate()));


        public float Tolerance
        {
            get => (float)GetValue(ToleranceProperty);
            set => SetValue(ToleranceProperty, value);
        }
    }
}