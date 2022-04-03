using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FallenAngelHandy
{
    public static class Gallery
    {
        private static string currentGallery;
        private static List<CmdLinear> gallery;
        public static async Task Play(string galleryName)
        {
            if (!Game.Config.SexScenes 
                || currentGallery == galleryName)
                return;

            currentGallery = galleryName;
            gallery = GalleryRepository.Get(galleryName);
                        //?? GalleryRepository.GetRandom();

            if (gallery == null)
                return;

            gallery = gallery.TrimGalleryTimeTo(14000);

            await ButtplugService.SendCmd(gallery);
        }
        public static async Task RePlay()
        {
            await ButtplugService.SendCmd(gallery);
        }

        internal static async Task StopAsync()
        {
            gallery = null;
            currentGallery = null;
            await ButtplugService.StopClear();
        }
    }
}
