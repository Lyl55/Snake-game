using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilan
{
    class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static string directions;

        public Settings()
        {
            Width = 12;
            Height = 12;
            Speed = 10;

            directions = "left";
        }
    }
}
