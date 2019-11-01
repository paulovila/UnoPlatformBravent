using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using SkiaSharp;
using SkiaSharp.Views.UWP;
using UnoWebApiSwagger.Controls;

namespace UnoWebApiSwagger.Shared
{
    public sealed partial class GraphControl
    {
        private const int PreviewPointCount = 3;

        private static readonly Random Rnd = new Random();

        public static readonly DependencyProperty ValuesProperty = DependencyProperty.Register(
            "Values", typeof(IEnumerable), typeof(GraphControl), new PropertyMetadata(default(IEnumerable)));

        private readonly SKPaint _strokePaint;

        private readonly SKPaint _textPaint;

        public GraphControl()
        {
            InitializeComponent();

            _textPaint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                TextSize = 12
            };

            _strokePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Red,
                StrokeWidth = 1,
                FilterQuality = SKFilterQuality.High,
                IsAntialias = true
            };
        }


        public IEnumerable Values
        {
            get => (IEnumerable)GetValue(ValuesProperty);
            set => SetValue(ValuesProperty, value);
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            var info = args.Info;
            var surface = args.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            var points = ToViewPort(GetPoints().ToList(), info.Width, info.Height).ToList();

            PlotPoints(points, canvas);
            DrawLabels(points, canvas, args.Info.Width);
        }

        private void DrawLabels(IEnumerable<PlotPoint> points, SKCanvas canvas, int width)
        {
            foreach (var plotPoint in points)
            {
                DrawLabel(canvas, plotPoint, width);
            }
        }

        private void PlotPoints(IReadOnlyCollection<PlotPoint> points, SKCanvas canvas)
        {
            var path = new SKPath();

            var first = points.First();

            path.MoveTo((float)first.Translated.X, (float)first.Translated.Y);
            foreach (var plotPoint in points)
            {
                Plot(path, plotPoint);
            }

            canvas.DrawPath(path, _strokePaint);
        }

        private void DrawLabel(SKCanvas canvas, PlotPoint plotPoint, int width)
        {
            var text = plotPoint.Original.Y.ToString("F0");
            var vertOffset = plotPoint.Translated.Y < _textPaint.TextSize ? _textPaint.TextSize : 0;

            var horzOffset = GetHorzOffset(text, plotPoint, width);

            var point = new SKPoint((float)(plotPoint.Translated.X + horzOffset), (float)plotPoint.Translated.Y + vertOffset);
            canvas.DrawText(text, point, _textPaint);
        }

        private double GetHorzOffset(string text, PlotPoint plotPoint, int viewportWidth)
        {
            var width = _textPaint.MeasureText(text);
            var halfWidth = width / 2;

            if (plotPoint.Translated.X < halfWidth)
            {
                return halfWidth;
            }

            if (plotPoint.Translated.X + halfWidth > viewportWidth)
            {
                return -halfWidth;
            }

            return 0;

        }

        private void Plot(SKPath path, PlotPoint plotPoint)
        {
            var x = (float)plotPoint.Translated.X;
            var y = (float)plotPoint.Translated.Y;
            path.LineTo(x, y);
        }

        private IEnumerable<PlotPoint> ToViewPort(IList<(double, double)> points, int viewportWidth, int viewportHeight)
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
                return new PlotPoint(new Point(origX, origY), new Point(x, viewportHeight - y));
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
    }
}