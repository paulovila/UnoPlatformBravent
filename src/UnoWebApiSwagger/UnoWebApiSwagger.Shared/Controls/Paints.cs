using SkiaSharp;

namespace UnoWebApiSwagger.Shared
{
    public static class Paints
    {
        public static SKPaint Black = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Black,
            StrokeWidth = 1,
            FilterQuality = SKFilterQuality.High,
            IsAntialias = true,
        };
        public static SKPaint Green = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.DarkGreen,
            StrokeWidth = 2,
            FilterQuality = SKFilterQuality.High,
            IsAntialias = true,
        };

        public static SKPaint Red = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Red,
            StrokeWidth = 2,
            FilterQuality = SKFilterQuality.High,
            IsAntialias = true,
        };
    }
}