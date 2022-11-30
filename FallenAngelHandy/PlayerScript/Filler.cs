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
        public static async Task RePlay()
        {
            await Play();
        }

    }
}

