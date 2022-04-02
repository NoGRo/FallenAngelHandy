using System;
using System.Collections.Generic;
using System.Text;

namespace FallenAngelHandy
{
    public static class Game
    {
        public static GameStatus Status { get; set; } = new GameStatus();
        public static Config Config { get; set; } = new Config();
    }
}
