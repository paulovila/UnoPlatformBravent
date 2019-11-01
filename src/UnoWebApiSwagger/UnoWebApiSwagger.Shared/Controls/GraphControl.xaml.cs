using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using SkiaSharp;
using SkiaSharp.Views.UWP;
using Uno.Extensions;

namespace UnoWebApiSwagger.Shared
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

            var points = ToViewPort(GetPoints().ToList(), info.Width, info.Height).ToList();

            var path = new SKPath();
            var translatedPoints = points.Select(tuple => ((float)tuple.Item1, (float)(info.Height - tuple.Item2))).ToList();

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

        private IEnumerable<(double, double)> ToViewPort(IList<(double, double)> points, int viewportWidth, int viewportHeight)
        {
            var maxY = points.Max(tuple => tuple.Item2);
            var minY = points.Min(tuple => tuple.Item2);
            var maxX = points.Max(tuple => tuple.Item1);
            var minX = points.Min(tuple => tuple.Item1);

            var diffX = maxX - minX;
            var diffY = maxY - minY;

            var translated = points.Select(tuple =>
            {
                var (origX, origY) = tuple;
                var x = origX * viewportWidth / diffX;
                var y = (origY - minY) * viewportHeight / diffY;
                return (x, y);
            });

            return translated;
        }

        private IEnumerable<(double, double)> GetPoints()
        {
            if (Values == null)
            {
                return Enumerable.Range(0, PreviewPointCount)
                    .Select(x => ((double)x / (PreviewPointCount - 1), Rnd.NextDouble())).ToList();
            }

            return Values.Cast<(double, double)>().ToList();
        }

        public static readonly DependencyProperty ValuesProperty = DependencyProperty.Register(
            "Values", typeof(IEnumerable), typeof(GraphControl), new PropertyMetadata(default(IEnumerable)));

        public IEnumerable Values
        {
            get { return (IEnumerable)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }
    }
}
