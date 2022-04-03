using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;


namespace FallenAngelHandy
{

    public class FunScriptFile
    {

        public string version { get; set; }
        public bool inverted { get; set; }
        public int range { get; set; }
        public List<FunScriptAction> actions { get; set; }

        public FunScriptFile()
        {
            inverted = false;
            version = "1.0";
            range = 99;
            actions = new List<FunScriptAction>();
        }

        public void Save(string filename)
        {
            string content = JsonConvert.SerializeObject(this);

            File.WriteAllText(filename, content, new UTF8Encoding(false));
        }
    }

}