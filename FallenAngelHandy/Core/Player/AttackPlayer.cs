﻿using Buttplug;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using System.Threading.Tasks;
using System.Threading;
using System.Collections.Specialized;

namespace FallenAngelHandy
{
    public static class AttackPlayer
    {
        private static double hp;
        private static string acction;

        private static ScriptBuilder SB = new ScriptBuilder();



        public static async Task Play(string gameEvent, NameValueCollection Data)
        {

            SB.Clear();
            
            hp = Data.AllKeys.Contains("strength")
                ? string.IsNullOrEmpty(Data["strength"]) ? 0 : Convert.ToDouble(Data["strength"], CultureInfo.GetCultureInfo("en-US").NumberFormat)
                : 0;
            switch (gameEvent)

            {
                case "hit_pain":
                    HitPain();
                    break;
                case "hit_pleasure":
                    HitPleasure();
                    break;
                case "hit_fall":
                    await ButtplugService.SendCmd(GalleryRepository.Get("fall"));
                    break;
                case "hit_fall_ground":
                    await ButtplugService.SendCmd(CmdLinear.GetCommandSpeed(Game.Config.AttackSpeed, 0, ButtplugService.GetCurrentValue()));
                    break;
                case "jump_laser":
                case "laser":
                case "ledge_laser":
                    Laser();
                    break;

                case "uppercut_laser_prep":
                    await ButtplugService.SendCmd(GalleryRepository.Get("uppercut_prep"));
                    break;
                case "uppercut_laser":
                    LaserExtra();
                    break;
                case "crounch_laser":
                    laserCmds();
                    Laser();
                    break;
                case "stun":
                    await ButtplugService.SendCmd(GalleryRepository.Get("stun"));
                    break;
                case "reverse":
                    Reverse();
                    break;
                default:
                    break;
            }
        }

        private static async Task HitPain()
        {
            SB.AddCommandSpeed(Game.Config.HitSpeed, Convert.ToInt32(hp * 1.5), ButtplugService.GetCurrentValue());
            SB.AddCommandSpeed(Game.Config.HitSpeed, 0);
            await ButtplugService.SendCmd(SB.Generate());
        }
        private static async Task HitPleasure()
        {
            SB.AddCommandSpeed(Game.Config.HitSpeed, 100 - Convert.ToInt32(hp * 1.5), ButtplugService.GetCurrentValue());
            SB.AddCommandSpeed(Game.Config.HitSpeed, 100);
            await ButtplugService.SendCmd(SB.Generate());
        }

        private static async Task Laser()
        {
            laserCmds();

            await ButtplugService.InsertCmd(SB.Generate()); //inser over filler and continue 
        }
        private static async Task LaserExtra()
        {
            laserCmds(true);

            await ButtplugService.SendCmd(SB.Generate()); //inser over filler and continue 
        }

        private static void laserCmds(bool extra = false)
        {
            var laserLength = Convert.ToInt32(Game.Config.LaserLength * (extra ? 2 : 1));
            var curval = ButtplugService.GetCurrentValue();
            var maxLaser = curval > 50 ? curval : 100 - curval;
            
            laserLength = Math.Min(laserLength, maxLaser);
            
            var laserValue = curval + laserLength;
            if (laserValue > 100)
                laserValue = curval - laserLength;


            SB.AddCommandSpeed(Game.Config.LaserSpeedMin, laserValue, curval);
            SB.AddCommandSpeed(Game.Config.LaserSpeedMax, curval, laserValue);
        }

        private static void Reverse()
        {
            ButtplugService.Invert = true;
            Timer timer = null;
            timer = new Timer(o =>
            {
                ButtplugService.Invert = false;
                timer.Dispose();
            }, null, 6000, Timeout.Infinite);
        }




 
    }
}

