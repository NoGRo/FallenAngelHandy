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

        public static Dictionary<string, List<GalleryIndex>> Galleries { get; set; } = new Dictionary<string, List<GalleryIndex>>();
        public static Dictionary<string, FileInfo> Assets { get; set; } = new Dictionary<string, FileInfo>(StringComparer.OrdinalIgnoreCase);

        public static string CurrentVariant { get; set; }
        public static void LoadGalleryFromFolder()
        {       
            GalleryFromFolder();
        }
        private static void GalleryFromFolder()
        {
            var variants = Directory.GetDirectories($"{Game.Config.GalleryPath}\\");
            foreach (var variant in variants)
            {
                var GalleryPath = $"{Game.Config.GalleryPath}\\" + (!string.IsNullOrEmpty(variant) ? variant + "\\" : "");

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

                    if (!Galleries.ContainsKey(name))
                        Galleries.Add(name, new List<GalleryIndex>());

                    Galleries[name].Add(new GalleryIndex
                    {
                        Name = name,
                        Variant = variant,
                        Commands = sb.Generate(),
                        Assets = new Dictionary<string, FileInfo>(StringComparer.OrdinalIgnoreCase) { { file.Extension, file } }
                    });

                }
            }
        }
        public static List<string> GetNames()
            => Galleries.Keys.ToList();

        public static GalleryIndex Get(string name, string variant = null)
        {
            variant = variant ?? CurrentVariant;
            var gallery = Galleries.GetValueOrDefault(name).FirstOrDefault(x => x.Variant == variant)
                        ?? Galleries.GetValueOrDefault(name).FirstOrDefault(x => x.Variant == CurrentVariant)
                        ?? Galleries.GetValueOrDefault(name).FirstOrDefault();
            return gallery;
            
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
