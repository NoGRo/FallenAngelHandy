using System;
using System.Collections.Generic;
using System.Text;

namespace FallenAngelHandy
{
    public static class Game
    {
        public static Status Status { get; set; } = new Status();
        public static Config Config { get; set; } = new Config();
        public static GamePad GamePad { get; set; } = new GamePad();
    }
}
