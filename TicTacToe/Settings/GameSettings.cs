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
        public static Algorithm Algorithm { get; set; } = Algorithm.Easy;
        public static int WinLength { get; set; } = 3;
        public static bool IsComputerOpponent { get; set; } = true;

    }
}
