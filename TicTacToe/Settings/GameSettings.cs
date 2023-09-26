using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Enums;

namespace TicTacToe.Settings
{
    public static class GameSettings
    {
        public static Algorithm Algorithm { get; set; } 
        public static int WinLength { get; set; }
        //sätter defaultvärde på IsComputerOpponent så att man får "true" även om man inte ändrat på radioknapparna för motståndare.
        public static bool IsComputerOpponent { get; set; } = true;

    }
}
