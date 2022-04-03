using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenAngelHandy
{
    public class ScriptBuilder
    {

        private  List<CmdLinear> Sequence { get; set; } = new List<CmdLinear>();

        public List<CmdLinear> Generate()
        {
            var resul = Sequence.ToList();
            Clear();
            return resul;
        }
        public int lastValue => Sequence.LastOrDefault()?.Value ?? Convert.ToInt32(ButtplugService.GetCurrentValue());
        public void Clear()
            => Sequence.Clear();

        //go to a value at speed (Use starting point to calculate speed)
        public void AddCommandSpeed(int speed, int value, int? currentValue = null)
        {
            var cmd = CmdLinear.GetCommandSpeed(speed, value, currentValue ?? lastValue);
            Sequence.Add(cmd);
        }
        //go to a value in Milliseconds 
        public void AddCommandMillis(int millis, int value)
        {
            var cmd = CmdLinear.GetCommandMillis(millis, value);
            Sequence.Add(cmd);
        }

        public void AddGallery(string galleryName, int? TrimTimeMs = null)
        {
            var gallery = GalleryRepository.Get(galleryName);

            if (gallery == null)
                return;

            if (TrimTimeMs != null)
                gallery = gallery.TrimGalleryTimeTo(TrimTimeMs.Value);

            Sequence.AddRange(gallery);
        }


        public void MergeCommands() //remove redundant commands from Sequence
        {
            var final = new List<CmdLinear>() { Sequence.First() };
            for (int i = 1; i < Sequence.Count(); i++)
            {
                var last = final.Last();
                var comNext = Sequence[i];

                if (last.Speed == comNext?.Speed && last.Direction == comNext?.Direction)
                {
                    last.Value = comNext.Value;
                    last.Millis += comNext.Millis;
                }
                else
                    final.Add(comNext);
            }

            Sequence = final.Where(x => x.Millis > 0).ToList();
        }
    }
}
