using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Devices.PointOfService;

namespace FallenAngelHandy.Core
{
    public class GalleryBundler
    {

        public Dictionary<string, GalleryIndex> Galleries => GalleryRepository.Galleries;

        private List<CmdLinear> cmds;
        public List<CmdLinear> Cmds { get => cmds; private set => cmds = value; }

        private ScriptBuilder sb = new ScriptBuilder();
       

        public void Add(GalleryIndex gallery, bool repeats, bool hasSpacer)
        {


            gallery.Repeats = repeats;
            gallery.HasSpacer = hasSpacer;


            var Index = gallery;

            var spacerDuration = 5000;
            var repearDuration = 10000;

            var startTime = sb.TotalTime;

            sb.addCommands(gallery.Commands);

            Index.Duration = sb.TotalTime - startTime;
            Index.StartTime = startTime;
            Index.EndTime = sb.TotalTime;

            //6 seconds repear in script bundle for loop msg delay
            if (gallery.Repeats)
                sb.addCommands(gallery.Commands.Clone().TrimGalleryTimeTo(6));

            if (Index.HasSpacer) // extra, no movement
                sb.AddCommandMillis(spacerDuration, sb.lastValue);

            if(!Galleries.ContainsKey(Index.Name))
                Galleries.Add(Index.Name, Index);

        }


        public Dictionary<string, FileInfo> GenerateBundle()
        {
            cmds = sb.Generate();
            
            var final = new Dictionary< string ,FileInfo>();

            //Cmds.AddAbsoluteTime();
            var funscript = new FunScriptFile(cmds);
            var filePath = Game.Config.UserDataPath + "bundle.funscript";
            funscript.Save(filePath);
            final.Add("funscript", new FileInfo(filePath));


            var csv = new FunScriptCsv(cmds);

            var csvPath = Game.Config.UserDataPath + "bundle.csv";
            csv.Save(csvPath);
            final.Add("csv", new FileInfo(csvPath));

            Galleries.Values.ToList().ForEach(x => x.Assets = final);

            return final;

        }

    }
}
