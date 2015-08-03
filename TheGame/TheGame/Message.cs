using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheGame
{
    class Message
    {
        public class MessageBox
        {
            static public int numOfFrames = 6;
            static public int[] X = new int[6] { 0, 500, 0, 500, 0, 0 };
            static public int[] Y = new int[6] { 20, 20, 175, 175, 313, 471 };
            static public int[] Width = new int[6] { 485, 485, 485, 485, 518, 518 };
            static public int[] Height = new int[6] { 105, 105, 105, 105, 142, 142 };
        };
        public class Player1_WinCountBox
        {
            static public int numOfFrames = 4;
            static public int[] X = new int[4] { 0, 350, 0, 350 };
            static public int[] Y = new int[4] { 4, 4, 56, 56 };
            static public int[] Width = new int[4] { 325, 325, 325, 325 };
            static public int[] Height = new int[4] { 42, 42, 42, 42 };
        };
        public class Player2_WinCountBox
        {
            static public int numOfFrames = 4;
            static public int[] X = new int[4] { 0, 350, 0, 350 };
            static public int[] Y = new int[4] { 110, 110, 165, 165 };
            static public int[] Width = new int[4] { 325, 325, 325, 325 };
            static public int[] Height = new int[4] { 42, 42, 42, 42 };
        };
        public class RoundNumber
        {
            static public int numOfFrames = 3;
            static public int[] X = new int[3] { 0, 274, 538 };
            static public int[] Y = new int[3] { 0, 0, 0 };
            static public int[] Width = new int[3] { 262, 262, 262 };
            static public int[] Height = new int[3] { 210, 210, 210 };
        };

    };
};
