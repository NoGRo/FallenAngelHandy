using Buttplug;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FallenAngelHandy
{
    public static class FillerPlayer
    {

        private static ScriptBuilder SB =  new ScriptBuilder();



        public static async Task Play()
        {

            SB.Clear();
            GoHome();
            addDelay();
            GenerateSicle();

            await ButtplugService.SendCmd(SB.GenerateSecuence());
        }

        public static async Task RePlay()
        {
            SB.Clear();
            GenerateSicle();

            await ButtplugService.SendCmd(SB.GenerateSecuence());
        }


        private static int Speed
            => Convert.ToInt32(
                Game.Config.MinSpeed +
                ((Game.Config.MaxSpeed - Game.Config.MinSpeed) * Math.Min(Game.Status.Pleasure / 90, 1)));
        private static int Lenght
            => Convert.ToInt32(
                Game.Config.MinLength +
                ((Game.Config.MaxLength - Game.Config.MinLength) * Math.Min(Game.Status.Pain / 90, 1)));

        private static int SegmentLenght
            => Convert.ToInt32(Lenght * 0.333);

        private static void GenerateSicle()
        {
            //Up
            SB.AddCommandSpeed(Speed, SegmentLenght);
            AddPartEffect(Game.Status.Anus, true);
            SB.AddCommandSpeed(Speed, SegmentLenght * 2);
            AddPartEffect(Game.Status.Vagina, true);
            SB.AddCommandSpeed(Speed, Lenght);
            AddPartEffect(Game.Status.Penis, true);

            //Down
            SB.AddCommandSpeed(Speed, SegmentLenght * 2);
            AddPartEffect(Game.Status.Breasts, false);
            SB.AddCommandSpeed(Speed, SegmentLenght);
            AddPartEffect(Game.Status.Head, false);
            SB.AddCommandSpeed(Speed, 0);

            SB.MergeCommands();

        }

        private static void GoHome()
        {
            if (ButtplugService.GetCurrentValue() != 0)
                SB.AddCommandSpeed(Speed, 0, ButtplugService.GetCurrentValue());
        }
        private static void addDelay()
        {
            if(Game.Config.Delay != 0 )
                SB.AddCommandMillis(Game.Config.Delay, 1);
        }

        private static void AddPartEffect(double damage, bool direction)
        {
            int initial = SB.lastValue;

            if (damage <= Game.Config.MinDamage)
                return;

            if (damage <= Game.Config.CriticalDamage)
            {
                SB.AddCommandMillis(Convert.ToInt32(300 * (damage / Game.Config.CriticalDamage)), initial);
            }
            else
            {
                var critical = (damage - Game.Config.CriticalDamage) / ((double)100 - Game.Config.CriticalDamage);
                var strokeLength = Convert.ToInt32((SegmentLenght *0.5) + (SegmentLenght * 0.4 * critical));

                var strokeSpeed = Game.Config.CriticalSpeed;

                SB.AddCommandSpeed(strokeSpeed, direction ? initial - strokeLength : initial + strokeLength);
                SB.AddCommandSpeed(strokeSpeed, initial);
            }
        }
    }
}

