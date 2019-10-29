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


        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            // the the canvas and properties
            var canvas = e.Surface.Canvas;

            // get the screen density for scaling
            var display = DisplayInformation.GetForCurrentView();
            var scale = display.LogicalDpi / 96.0f;
            var scaledSize = new SKSize(e.Info.Width / scale, e.Info.Height / scale);

            // handle the device screen density
            canvas.Scale(scale);

            // make sure the canvas is blank
            canvas.Clear(SKColors.Yellow);

            // draw some text
            var paint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                TextSize = 24
            };
            var coord = new SKPoint(scaledSize.Width / 2, (scaledSize.Height + paint.TextSize) / 2);
            canvas.DrawText("SkiaSharp", coord, paint);

         }
        //void OnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        //{
        //    SKImageInfo info = args.Info;
        //    SKSurface surface = args.Surface;
        //    SKCanvas canvas = surface.Canvas;

        //    canvas.Clear();
            
        //    var points = GetPoints();
        //    var path = new SKPath();
        //    var translatedPoints = points.Select(tuple => ((float)(tuple.Item1 * info.Width), (float)(info.Height - tuple.Item2 * info.Height))).ToList();

        //    var (startX, startY) = translatedPoints.First();

        //    path.MoveTo(startX, startY);
        //    foreach (var (x, y) in translatedPoints)
        //    {
        //        path.LineTo(x, y);
        //    }

        //    var strokePaint = new SKPaint
        //    {
        //        Style = SKPaintStyle.Stroke,
        //        Color = SKColors.Red,
        //        StrokeWidth = 1,
        //        FilterQuality = SKFilterQuality.High,
        //        IsAntialias = true,
        //    };

        //    canvas.DrawPath(path, strokePaint);
        //}

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
