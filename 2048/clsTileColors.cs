using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class clsTileColors
    {
        private static clsTileColors _instance;
        public static clsTileColors Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new clsTileColors();
                return _instance;
            }
        }

        private Dictionary<int, Color> Colors;

        //constructor
        private clsTileColors()
        {
            Colors = new Dictionary<int, Color>();
            Colors.Add(2, Color.FromArgb(238, 228, 218));
            Colors.Add(4, Color.FromArgb(236, 224, 200));
            Colors.Add(8, Color.FromArgb(238, 178, 126));
            Colors.Add(16, Color.FromArgb(244, 148, 98));
            Colors.Add(32, Color.FromArgb(247, 123, 97));
            Colors.Add(64, Color.FromArgb(223, 104, 82));
            Colors.Add(128, Color.FromArgb(228, 205, 129));
            Colors.Add(256, Color.FromArgb(242, 205, 114));
            Colors.Add(512, Color.FromArgb(234, 200, 77));
            Colors.Add(1024, Color.FromArgb(233, 193, 80));
            Colors.Add(2048, Color.FromArgb(234, 189, 74));
        }

        public Color GetBackColorFromValue(int value)
        {
            if(Colors.ContainsKey(value))
                return Colors[value];
            else
                return Color.FromArgb(0, 0, 0);
        }

        public Color GetForeColorFromValue(int value)
        {
            if (value > 4)
                return Color.White;
            else
                return Color.Black;
        }
    }
}
