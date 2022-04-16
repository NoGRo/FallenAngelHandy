using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Resources;
using System.Collections;
using System.Reflection;

namespace FallenAngelHandy
{
    public static class GalleryRepository
    {
        
        private static Dictionary<string, IEnumerable<(uint, byte)>> dicGallery = new Dictionary<string, IEnumerable<(uint, byte)>>();

        public static void Init()
        {
            GalleryFromFolder();
            if(!string.IsNullOrEmpty(Game.Config.GalleryUseVariant))
                GalleryFromFolder(Game.Config.GalleryUseVariant);
        }

        public static void SetVariant(string code)
            => GalleryFromFolder(code);

        private static void GalleryFromFolder(string variantCode = null) {
            var GalleryPath = $"{Game.Config.GalleryPath}\\" + (!string.IsNullOrEmpty(variantCode) ? variantCode+"\\" : "");

            if (!Directory.Exists($"{GalleryPath}"))
                return;

            var files = Directory.GetFiles(GalleryPath , "*.funscript").Select(x => new FileInfo(x)).Where(x => x.Length > 0);
            

            foreach (var file in files)
            {
                FunScriptFile funscript = null;
                try
                {
                    funscript = JsonSerializer.Deserialize<FunScriptFile>(File.ReadAllText(file.FullName));
                }
                catch
                {
                    continue;
                }
                var gallery = new List<(uint, byte)>();

                long lastInit = 0;
                foreach (var action in funscript.actions)
                {
                    var cmd = (Convert.ToUInt32(action.at - lastInit), action.pos);
                    gallery.Add(cmd);
                    lastInit = action.at;
                }

                var name = file.Name.Replace(file.Extension, "");

                if (dicGallery.ContainsKey(name))
                    dicGallery.Remove(name);

                dicGallery.Add(name, gallery);
            }
        }
        public static List<string> GetNames()
            => dicGallery.Keys.ToList(); 

        public static List<CmdLinear> Get(string name)
        {
            var gallery = dicGallery.GetValueOrDefault(name)?.ToCmdLinear();

            return gallery;
        }

        public static List<CmdLinear> GetRandom()
            =>  dicGallery.Values.ElementAt(new Random().Next(0, dicGallery.Values.Count())).ToCmdLinear();
            
        
        private static List<CmdLinear> ToCmdLinear(this IEnumerable<(uint, byte)> lst)
            => lst
                .Select(x => CmdLinear.GetCommand(x.Item1, x.Item2))
                .Where(x=> x.Millis != 0)
                .ToList();
        
        public static List<CmdLinear> TrimGalleryTimeTo(this List<CmdLinear> gallery, int maxTime)
        {
            if (!gallery.Any())
                return gallery;

            var countTime = 0;
            var cropAt = 0;
            CmdLinear lastItem = null;
            foreach (var item in gallery)
            {
                countTime += item.Millis;
                if (countTime > maxTime)
                {
                    lastItem = item;
                    cropAt = gallery.IndexOf(item);
                    lastItem.Millis -= (countTime - maxTime);
                    break;
                }
            }
            if (cropAt != 0 && cropAt != gallery.Count())
            {
                gallery.RemoveRange(cropAt, gallery.Count() - cropAt);
                gallery.Add(lastItem);
            }
            else if (countTime < maxTime)
            {
                gallery.Last().Millis += (maxTime - countTime);
            }

            return gallery;
        }
    }
}
