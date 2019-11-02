using SkiaSharp;

namespace UnoWebApiSwagger.Shared
{
    public static class Paints
    {
        public static SKPaint Gray = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Gray,
            StrokeWidth = 1,
            FilterQuality = SKFilterQuality.High,
            IsAntialias = false,
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