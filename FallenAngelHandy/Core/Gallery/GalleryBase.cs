using System.Collections.Generic;
using System.IO;

namespace FallenAngelHandy.Core
{
    public class Gallery
    {
        public string Name { get; set; }

        public virtual List<CmdLinear> Commands { get; set; }
        public Dictionary<string, FileInfo> Assets;
    }
}