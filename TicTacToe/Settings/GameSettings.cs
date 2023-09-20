using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Enums;

namespace TicTacToe.Settings
{
    internal static class GameSettings
    {
        public static Algorithm  Algorithm { get; set; }
        public static int WinLength { get; set; }
        public static bool IsComputerOpponent { get; set; }

    }
}
