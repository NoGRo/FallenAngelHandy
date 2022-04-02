using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FallenAngelHandy
{
    public static class GalleryPlayer
    {
        private static string currentGallery;
        private static List<CmdLinear> gallery;
        public static void Play(string galleryName)
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

            ButtplugService.SendCmd(gallery);
        }
        public static void RePlay()
        {
            ButtplugService.SendCmd(gallery);
        }

        internal static void Stop()
        {
            gallery = null;
            currentGallery = null;
            ButtplugService.StopClear();
        }
    }
}
