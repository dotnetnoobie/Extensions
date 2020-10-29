namespace System.Drawing
{
    public static class ColorExtensions
    {
        public static string ToHex(this Color color) => $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";

        public static string ToRGB(this Color color) => $"RGB({color.R},{color.G},{color.B})";

        public static string ToARGB(this Color color) => $"ARGB({color.A}{color.R},{color.G},{color.B})";
    }
}