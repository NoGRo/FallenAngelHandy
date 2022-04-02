using Buttplug;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using System.Threading.Tasks;
using System.Threading;

namespace FallenAngelHandy
{
    public static class AttackPlayer
    {
        private static double hp;
        private static string acction;

        private static ScriptBuilder SB = new ScriptBuilder();

        private static async Task HitPain()
        {
            SB.AddCommandSpeed(Launcher.Config.HitSpeed, Convert.ToInt32(hp * 1.5), ButtplugService.GetCurrentValue());
            SB.AddCommandSpeed(Launcher.Config.HitSpeed, 0);
            await ButtplugService.SendCmd(SB.GenerateSecuence());
        }
        private static async Task HitPleasure()
        {
            SB.AddCommandSpeed(Launcher.Config.HitSpeed, 100 - Convert.ToInt32(hp * 1.5), ButtplugService.GetCurrentValue());
            SB.AddCommandSpeed(Launcher.Config.HitSpeed, 100);
            await ButtplugService.SendCmd(SB.GenerateSecuence());
        }

        private static async Task Laser()
        {
            laserCmds();

            await ButtplugService.InsertCmd(SB.GenerateSecuence()); //inser over filler and continue 
        }
        private static async Task LaserExtra()
        {
            laserCmds(true);

            await ButtplugService.SendCmd(SB.GenerateSecuence()); //inser over filler and continue 
        }

        private static void laserCmds(bool extra = false)
        {
            var laserLength = Convert.ToInt32(Launcher.Config.LaserLength * (extra ? 2 : 1));
            var curval = ButtplugService.GetCurrentValue();
            var maxLaser = curval > 50 ? curval : 100 - curval;
            
            laserLength = Math.Min(laserLength, maxLaser);
            
            var laserValue = curval + laserLength;
            if (laserValue > 100)
                laserValue = curval - laserLength;


            SB.AddCommandSpeed(Launcher.Config.LaserSpeedMin, laserValue, curval);
            SB.AddCommandSpeed(Launcher.Config.LaserSpeedMax, curval, laserValue);
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

        public static async Task Play(string gameEvent)
        {
            if (!ButtplugService.isReady || !Launcher.Config.Attacks)
                return;

            SB.Clear();

            var eventSplit = gameEvent.Split(" ");

            acction = eventSplit[0];
            hp = eventSplit.Length > 1
                ? string.IsNullOrEmpty(eventSplit[1]) ? 0 : Convert.ToDouble(gameEvent.Split(" ")?[1],CultureInfo.GetCultureInfo("en-US").NumberFormat)
                : 0; switch (acction)

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
                    await ButtplugService.SendCmd(CmdLinear.GetCommandSpeed(Launcher.Config.AttackSpeed, 0, ButtplugService.GetCurrentValue()));
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


 
    }
}

