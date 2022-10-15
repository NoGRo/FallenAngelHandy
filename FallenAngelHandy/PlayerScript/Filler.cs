using Buttplug;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FallenAngelHandy.Core;

namespace FallenAngelHandy
{
    public static class FillerScript
    {

        public static async Task Play()
        {
            if (!Game.Config.Filler)
            {
                await HandyService.SendGallery($"filler{0:##}");
                return;
            }

            var values = new List<double> { Game.Status.Pleasure, Game.Status.Pain, Game.Status.Penis, Game.Status.Vagina, Game.Status.Breasts, Game.Status.Head, Game.Status.Anus, 1.0 };

            var value = values.Max();
            value = Math.Min(100, value);

            var fillerId = Math.Max(Math.Min(Convert.ToInt32((value / 100) * 10), 10), 1);


            await HandyService.SendGallery($"filler{fillerId:##}");

        }

        public static Dictionary<string,List<CmdLinear>> GenerateFillers()
        {
            var result  = new Dictionary<string,List<CmdLinear>>();
            var scriptBuilder =  new ScriptBuilder();
            var critialDamage = Game.Config.CriticalDamage / 10;

            while (scriptBuilder.TotalTime < 30000)
            {
                scriptBuilder.AddCommandMillis(2000, 0);
            }

            result.Add($"filler{0:##}", scriptBuilder.Generate());

            for (int i = 1; i == 10; i++)
            {
                var speed = Game.Config.FillerMinSpeed + (((i / 15.0)) * (Game.Config.FillerMaxSpeed - Game.Config.FillerMinSpeed));
                var value = Game.Config.FillerMinLength + (((i / critialDamage)) * (Game.Config.FillerMaxLength - Game.Config.FillerMinLength));
                if (i > critialDamage)
                {
                    speed = Game.Config.FillerMinSpeed + (((i / 10.0)) * (Game.Config.FillerMaxSpeed - Game.Config.FillerMinSpeed));
                    value = Game.Config.FillerMaxLength;
                }
                if(i >= Game.Config.ExtremeDamage)
                {

                }

                while (scriptBuilder.TotalTime < 30000)
                {
                    scriptBuilder.AddCommandSpeed(speed, value);
                    scriptBuilder.AddCommandSpeed(speed, 0);
                }
                result.Add($"filler{i:##}", scriptBuilder.Generate());
            }
            return result;

        }
        public static async Task RePlay()
        {
            await Play();
        }

    }
}

