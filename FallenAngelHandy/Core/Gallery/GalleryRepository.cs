using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Resources;
using System.Collections;
using System.Reflection;
using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using SharpDX.Win32;

namespace FallenAngelHandy.Core
{
    public static class GalleryRepository
    {

        public static Dictionary<string, List<GalleryIndex>> Galleries { get; set; } = new Dictionary<string, List<GalleryIndex>>();
        public static Dictionary<string, FileInfo> Assets { get; set; } = new Dictionary<string, FileInfo>(StringComparer.OrdinalIgnoreCase);
        public static List<GalleryDefinition> Definitions { get; set; }

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

        private static void LoadGalleryFromCsv(string variantCode)
        {

            var GalleryPath = $"{Game.Config.GalleryPath}\\" + (!string.IsNullOrEmpty(variantCode) ? variantCode + "\\" : "");

            if (!Directory.Exists($"{GalleryPath}"))
                return;


            using (var reader = File.OpenText(@".\GalleryDefinition.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                Definitions = csv.GetRecords<GalleryDefinition>().ToList();
            }

            var bundler = new GalleryBundler();
            var FunscriptCache = new Dictionary<string, FunScriptFile>(StringComparer.OrdinalIgnoreCase);

            foreach (var galleryDefinition in Definitions)
            {
                var filePath = $"{GalleryPath}\\{galleryDefinition.FileName}.funscript";
                FunScriptFile funscript = null;
                if (!FunscriptCache.ContainsKey(filePath))
                {
                    try
                    {
                        funscript = JsonSerializer.Deserialize<FunScriptFile>(File.ReadAllText(filePath));
                        funscript.actions = funscript.actions.OrderBy(x => x.at).ToList();
                    }
                    catch
                    {
                        continue;
                    } 
                    FunscriptCache.Add(filePath, funscript);
                }
                funscript = FunscriptCache[filePath];


                var gallery = new GalleryIndex
                {
                    Name = galleryDefinition.Name,
                    Duration = Convert.ToInt32(galleryDefinition.EndTime - galleryDefinition.StartTime)
                };

                var actions = funscript.actions
                    .Where(x => x.at > galleryDefinition.StartTime 
                             && x.at <= galleryDefinition.EndTime);

                var sb = new ScriptBuilder();

                foreach (var action in actions)
                {
                    sb.AddCommandMillis(Convert.ToInt32(action.at - sb.TotalTime), action.pos);
                }
                gallery.Commands = sb.Generate().TrimGalleryTimeTo(gallery.Duration);

                if (Galleries.ContainsKey(galleryDefinition.Name))
                    Galleries.Remove(galleryDefinition.Name);

                bundler.Add(gallery, galleryDefinition.Loop, true);
                Galleries.Add(gallery.Name, gallery);
            }

            Assets = bundler.GenerateBundle();
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
