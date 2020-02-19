using System.Drawing;

namespace CGG
{
    public class Theme
    {
        public Color Background { get; set; }
        
        public Color Axis { get; set; }
        
        public Color Function { get; set; }
    }

    public static class Themes
    {
        public static Theme Default => new Theme
        {
            Background = Color.White,
            Axis = Color.Black,
            Function = Color.Red
        };

        public static Theme Dark => new Theme
        {
            Background = Color.Black,
            Axis = Color.White,
            Function = Color.DodgerBlue
        };
        
        public static Theme School => new Theme
        {
            Background = Color.SeaGreen,
            Axis = Color.White,
            Function = Color.White
        };
    }
}