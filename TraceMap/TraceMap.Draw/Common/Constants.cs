using ImageMagick;
using TraceMap.Draw.Enums;

namespace TraceMap.Draw.Common
{
    public static class Constants
    {
        public static MagickColor TextColor = MagickColors.Black;

        public static int CircleRadius = 25;
     
        public static int FontSize = 25;
        
        public static string DefaultFileName = "tracemap.jpg";

        public static MagickColor HostColor = MagickColors.Red;

        public static MagickColor TargetColor = MagickColors.Blue;

        public static MagickColor CommonColor = MagickColors.GreenYellow;

        public static MagickColor GetPointColor(PointType pointType)
        {
            switch (pointType)
            {
                
                case PointType.Host:
                    return HostColor;
                case PointType.Target:
                    return TargetColor;
                default:
                    return CommonColor;
            }
        }
    }
}
