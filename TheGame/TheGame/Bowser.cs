using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheGame
{
    class Bowser
    {
        public class standing
        {
            static public float delay = 80f;
            static public int numOfFrames = 7;
            static public int[] X = new int[7] { 9, 75, 143, 211, 279, 347, 415 };
            static public int[] Y = new int[7] { 114, 114, 116, 116, 118, 118, 118 };
            static public int[] Width = new int[7] { 65, 66, 66, 66, 67, 68, 69 };
            static public int[] Height = new int[7] { 67, 67, 65, 65, 63, 63, 63 };
        }
        public class walking
        {
            static public float delay = 40f;    //is how much time delay before the next frame starts
            static public int speed = 7;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 6;
            static public int[] X = new int[6] { 552, 639, 733, 832, 934, 1030 };
            static public int[] Y = new int[6] { 116, 117, 120, 121, 121, 118 };
            static public int[] Width = new int[6] { 64, 62, 60, 60, 60, 62 };
            static public int[] Height = new int[6] { 64, 64, 60, 59, 60, 64 };
        }
        public class running
        {
            static public float delay = 40f;
            static public int speed = 10;
            static public int numOfFrames = 5;
            static public int[] X = new int[5] { 1177, 1283, 1399, 1517, 1636 };
            static public int[] Y = new int[5] { 123, 123, 123, 125, 125 };
            static public int[] Width = new int[5] { 72, 73, 74, 74, 73 };
            static public int[] Height = new int[5] { 55, 55, 56, 54, 57 };
        }
        public class jumping
        {
            static public float delay = 60f;
            static public int speed = 7;
            static public int numOfFrames = 11;
            static public int[] X = new int[11] { 11, 90, 186, 282, 380, 471, 549, 638, 713, 784, 856 };
            static public int[] Y = new int[11] { 661, 665, 666, 665, 661, 657, 667, 664, 665, 665, 664 };
            static public int[] Width = new int[11] { 70, 74, 73, 72, 70, 65, 68, 46, 60, 62, 60 };
            static public int[] Height = new int[11] { 61, 57, 56, 57, 64, 70, 59, 60, 62, 60, 62 };
        }
        public class blocking
        {
            static public float delay = 40f;
            static public int numOfFrames = 1;
            static public int[] X = new int[1] { 561 };
            static public int[] Y = new int[1] { 526 };
            static public int[] Width = new int[1] { 62 };
            static public int[] Height = new int[1] { 63 };
        }
        public class punching
        {
            static public float delay = 40f;
            static public int numOfFrames = 6;
            static public int[] X = new int[6] { 473, 568, 664, 761, 878, 992 };
            static public int[] Y = new int[6] { 205, 213, 213, 214, 215, 214 };
            static public int[] Width = new int[6] { 58, 67, 70, 93, 89, 90 };
            static public int[] Height = new int[6] { 70, 61, 62, 61, 60, 61 };
        }
        public class firing
        {
            static public float delay = 50f;
            static public int numOfFrames = 5;
            static public int[] X = new int[5] { 1265, 1338, 1411, 1484, 1567 };
            static public int[] Y = new int[5] { 894, 893, 894, 897, 902 };
            static public int[] Width = new int[5] { 59, 60, 62, 68, 69 };
            static public int[] Height = new int[5] { 68, 68, 68, 66, 58 };
        }
        public class fireBall
        {
            static public float delay = 40f;
            static public int speed = 7;
            static public int numOfFrames = 3;
            static public int[] X = new int[3] { 1715, 1805, 1890 };
            static public int[] Y = new int[3] { 903, 902, 901 };
            static public int[] Width = new int[3] { 65, 61, 69 };
            static public int[] Height = new int[3] { 43, 44, 45 };
        }
        public class hurt
        {
            static public float delay = 60f;
            static public int speed = 3;
            static public int numOfFrames = 3;
            static public int[] X = new int[3] { 27, 132, 27 };
            static public int[] Y = new int[3] { 767, 767, 767 };
            static public int[] Width = new int[3] { 65, 59, 65 };
            static public int[] Height = new int[3] { 67, 65, 67 };
        }
        public class dead
        {
            static public float delay = 100f;
            static public int speed = 3;
            static public int numOfFrames = 4;
            static public int[] X = new int[4] { 699, 786, 886, 990 };
            static public int[] Y = new int[4] { 1025, 1022, 1025, 1022 };
            static public int[] Width = new int[4] { 60, 67, 71, 67 };
            static public int[] Height = new int[4] { 65, 69, 69, 66 };
        }
    }
}
