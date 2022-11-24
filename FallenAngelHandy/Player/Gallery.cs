using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FallenAngelHandy.Core;

namespace FallenAngelHandy
{
    public static class GalleryPlayer
    {
        
        private static string? gallery;
        public static async Task Play(string galleryName)
        {
            if (!Game.Config.SexScenes 
                || gallery == galleryName)
                return;

            gallery = galleryName;
            await ButtplugService.SendGallery(galleryName);
        }
        public static async Task RePlay()
        {
            await ButtplugService.SendGallery(gallery);
        }

        internal static async Task StopAsync()
        {
            gallery = null;
            await ButtplugService.StopClear();
        }
    }
}
