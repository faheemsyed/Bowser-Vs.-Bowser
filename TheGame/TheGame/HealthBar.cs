using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheGame
{
    class HealthBar
    {
        public class Bar
        {
            static public int numOfFrames = 11;
            static public int[] X = new int[11] { 0, 300, 600, 900, 0, 300, 600, 900, 0, 300, 600 };
            static public int[] Y = new int[11] { 0, 0, 0, 0, 126, 126, 126, 126, 252, 252, 252 };
            static public int[] Width = new int[11] { 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300 };
            static public int[] Height = new int[11] { 126, 126, 126, 126, 126, 126, 126, 126, 126, 126, 126 };
        }
    }
}
