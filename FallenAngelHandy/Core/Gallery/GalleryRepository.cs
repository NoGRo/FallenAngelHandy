using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Resources;
using System.Collections;
using System.Reflection;

namespace FallenAngelHandy.Core
{
    public static class GalleryRepository
    {

        public static Dictionary<string, GalleryIndex> Galleries { get; set; } = new Dictionary<string, GalleryIndex>();
        public static Dictionary<string, FileInfo> Assets { get; set; } = new Dictionary<string, FileInfo>(StringComparer.OrdinalIgnoreCase);
        public static void SetVariant(string code)
            => GalleryFromFolder(code);

        public static void LoadGalleryFromFolder()
        {       
            GalleryFromFolder(null);

            if (!string.IsNullOrEmpty(Game.Config.GalleryUseVariant))
                GalleryFromFolder(Game.Config.GalleryUseVariant);
        }
        private static void GalleryFromFolder(string variantCode )
        {
            var GalleryPath = $"{Game.Config.GalleryPath}\\" + (!string.IsNullOrEmpty(variantCode) ? variantCode + "\\" : "");

            if (!Directory.Exists($"{GalleryPath}"))
                return;

            var files = Directory.GetFiles(GalleryPath, "*.funscript").Select(x => new FileInfo(x)).Where(x => x.Length > 0);


            foreach (var file in files)
            {
                FunScriptFile funscript = null;
                try
                {
                    funscript = JsonSerializer.Deserialize<FunScriptFile>(File.ReadAllText(file.FullName));
                    funscript.actions = funscript.actions.OrderBy(x => x.at).ToList();
                }
                catch
                {
                    continue;
                }
                var sb = new ScriptBuilder();

                foreach (var action in funscript.actions)
                {
                   sb.AddCommandMillis(Convert.ToInt32(action.at - sb.TotalTime), action.pos);
                }

                var name = file.Name.Replace(file.Extension, "");

                if (Galleries.ContainsKey(name))
                    Galleries.Remove(name);

                Galleries.Add(name, new GalleryIndex
                                        {
                                            Name = name,
                                            Commands = sb.Generate(),
                                            Assets = new Dictionary<string, FileInfo>(StringComparer.OrdinalIgnoreCase) { { file.Extension, file } }
                                        });

            }
        }
        public static List<string> GetNames()
            => Galleries.Keys.ToList();

        public static Gallery Get(string name)
        {
            var gallery = (Gallery)Galleries.GetValueOrDefault(name);
            return gallery;
;
        }
        public static GalleryIndex GetIndex(string name)
        {
            var gallery = Galleries.GetValueOrDefault(name);
            if (gallery == null)
                return null;
            return gallery;
            ;
        }


        public static List<CmdLinear> TrimGalleryTimeTo(this List<CmdLinear> gallery, int maxTime)
        {

            if (!gallery.Any())
                return gallery;

            var maxMillis = maxTime * 1000;

            gallery.AddAbsoluteTime();

            var final = gallery.Where(x => x.AbsoluteTime <= maxMillis);

            if (!final.Any())
                return final.ToList();

            var last = final.Last();
            if (last.AbsoluteTime != maxMillis)
                last.Millis += maxMillis - last.AbsoluteTime;

            return final.ToList();
        }
    }
}
