using ImageMagick;
using TraceMap.Draw.Enums;

namespace TraceMap.Draw.Common
{
    public static class Constants
    {
        public static readonly MagickColor TextColor = MagickColors.Black;

        public const int CircleRadius = 30;
     
        public const int FontSize = 35;

        public const double ScalingRadiusConstant = 250;

        public const int ImageScalingConstant = 600;
        
        public const string DefaultFileName = "tracemap";
        
        public const string DefaultFileExtension = "jpg";

        private static readonly MagickColor HostColor = MagickColors.Red;

        private static readonly MagickColor TargetColor = MagickColors.LightSkyBlue;

        private static readonly MagickColor CommonColor = MagickColors.GreenYellow;

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
