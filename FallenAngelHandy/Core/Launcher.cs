using System;
using System.Collections.Generic;
using System.Text;

namespace FallenAngelHandy
{
    public static class Launcher
    {
        public static Configuration Config { get; set; } = new Configuration();
        public static GameStatus Status { get; set; } = new GameStatus();
    }
}
