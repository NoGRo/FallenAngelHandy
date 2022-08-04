using FallenAngelHandy.Core;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FallenAngelHandy
{
    public static class GalleryBuilder
    {
        public static GalleryBundler bundler = new GalleryBundler();
        public static ScriptBuilder scriptBuilder { get; set; } = new ScriptBuilder();
        public static void Init()
        {
            loadGalleries();
                
            GenerateFillers();
            GalleryRepository.Assets = bundler.GenerateBundle();
        }

        private static void loadGalleries()
        {
            GalleryRepository.LoadGalleryFromFolder();
            var names = GalleryRepository.GetNames();
            names.Remove("fall");
            names.Remove("uppercut_prep");
            names.Remove("stun");

            foreach (var name in names)
            {
                var gallery = GalleryRepository.GetIndex(name);
                gallery.Commands = gallery.Commands.TrimGalleryTimeTo(14);
                bundler.Add(gallery,1,true);
            }

            bundler.Add(GalleryRepository.GetIndex("stun"), 0 ,true);
        }

        public static void GenerateFillers()
        {
            for (int i = 0; i < 10; i++)
            {
                var speed = Convert.ToInt32(Game.Config.MinSpeed + ((i / 10.0) * (Game.Config.MaxSpeed - Game.Config.MinSpeed)));
                var value = Convert.ToInt32(Game.Config.MinLength + ((i / 10.0) * (Game.Config.MaxLength - Game.Config.MinLength)));

                while (scriptBuilder.TotalTime < 30000) 
                { 
                    scriptBuilder.AddCommandSpeed(speed, value);
                    scriptBuilder.AddCommandSpeed(speed, 0);
                }

                var gallery = new GalleryIndex { Name = $"filler{i+1:##}", Commands = scriptBuilder.Generate()};
                bundler.Add(gallery,0,false);
            }
        }
    }
}
