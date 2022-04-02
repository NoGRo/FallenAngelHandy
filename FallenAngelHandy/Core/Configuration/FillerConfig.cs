using System;
using System.Collections.Generic;
using System.Text;

namespace FallenAngelHandy.Core.Configuration
{
    public class FillerConfig
    {
        public int MinSpeed { get; set; } = 50;
        public int MaxSpeed { get; set; } = 100;
        public int MinLength { get; set; } = 70;
        public int MaxLength { get; set; } = 95;


        public int CriticalSpeed { get; set; } = 190;
        public int MinDamage { get; set; } = 20;
        public int CriticalDamage { get; set; } =  85;
        
        public int Delay { get; set; } = 1000;
    }
}
