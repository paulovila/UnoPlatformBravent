using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using SkiaSharp;
using SkiaSharp.Views.UWP;

namespace SkiaSharpTest.Shared
{
    public sealed partial class GraphControl
    {
        private const int PreviewPointCount = 3;


        public GraphControl()
        {
            this.InitializeComponent();
        }

        private static readonly Random Rnd = new Random();

        void OnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            var points = GetPoints();
            var path = new SKPath();
            var translatedPoints = points.Select(tuple => ((float)(tuple.Item1 * info.Width), (float)(info.Height - tuple.Item2 * info.Height))).ToList();

            var (startX, startY) = translatedPoints.First();

            path.MoveTo(startX, startY);
            foreach (var (x, y) in translatedPoints)
            {
                path.LineTo(x, y);
            }

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

        private IEnumerable<(double, double)> GetPoints()
        {
            if (Values == null)
            {
                return Enumerable.Range(0, PreviewPointCount)
                    .Select(x => ((double) x / (PreviewPointCount - 1), Rnd.NextDouble())).ToList();
            }

            var doubles = Values.Cast<double>().ToList();
            var count = doubles.Count;
            return Enumerable.Range(0, count).Zip(doubles, (n, y) => ((double) n / (count - 1), y))
                .ToList();
        }

        public static readonly DependencyProperty ValuesProperty = DependencyProperty.Register(
            "Values", typeof(IEnumerable), typeof(GraphControl), new PropertyMetadata(default(IEnumerable)));

        public IEnumerable Values
        {
            get { return (IEnumerable) GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }
    }
}
