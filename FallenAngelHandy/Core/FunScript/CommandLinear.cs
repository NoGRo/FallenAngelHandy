﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace FallenAngelHandy
{
    public class CmdLinear 
    {
        const int SpeedLimit = 450;

        public static CmdLinear GetCommandSpeed(int speed, int value, int initialValue)
        {
            speed = speed > SpeedLimit ? SpeedLimit : speed;


            var millis =Convert.ToInt32(Math.Abs(initialValue - value) * ((double)1000 / speed));
            return new CmdLinear {
                Millis = millis,
                InitialValue = Convert.ToByte(initialValue),
                Value = Convert.ToByte(value),
                Speed = speed,
            };
        }
        public static CmdLinear GetCommandMillis(int millis, int value)
        {
            
            return new CmdLinear 
            { 
                Millis = millis,
                Value = Convert.ToByte(value)
            };
        }

        public static CmdLinear GetCommand(uint millis, byte value)
        {
            return new CmdLinear
            {
                Millis = Convert.ToInt32(millis),
                Value = value
            };
        }

        
        public int Millis { get; set; }
        public int Speed { get; set; } //TODO calculate Speed

        public bool Direction => Value > InitialValue;
        public byte Value { get; set; }
        public byte InitialValue { get; set; }
        
        public double LinearValue => Math.Min(1.0, Math.Max(0, Value / (double)100));

        public uint ButtplugMillis => (uint)Millis;


        public DateTime? Sent { get; set; }
        public DateTime? Stoped { get; set; }

        public bool Ended => (Sent?.AddMilliseconds(Millis + 50) ?? DateTime.Now) <= DateTime.Now;
        public int AbsoluteTime { get; internal set; }
    }

    public static class CmdLinearExtend
    {
        public static List<CmdLinear> Clone(this IEnumerable<CmdLinear> cmds) 
            => cmds.Select(x => CmdLinear.GetCommandMillis(x.Millis, x.Value)).ToList();

        public static void AddAbsoluteTime(this List<CmdLinear> cmds)
        {
            var at = 0; 
            foreach (var cmd in cmds)
            {
                at += cmd.Millis;
                cmd.AbsoluteTime = at;
            }
        }
    }
}
