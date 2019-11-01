using Windows.Foundation;

namespace UnoWebApiSwagger.Controls
{
    internal class PlotPoint
    {
        public PlotPoint(Point original, Point translated)
        {
            Original = original;
            Translated = translated;
        }

        public Point Original { get; }

        public Point Translated { get; }
    }
}