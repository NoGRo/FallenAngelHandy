using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenAngelHandy
{
    public class ControlsConfig
    {
        public List<Root2> Root { get; set; }
    }

    public class Root2 :IEqualityComparer<Root2>
    {
        public double force_fucking { get; set; }
        public double control_zoomplus { get; set; }
        public double control_left { get; set; }
        public double control_attack { get; set; }
        public string zoomdefault { get; set; }
        public double taunt { get; set; }
        public double control_pause { get; set; }
        public double control_interact { get; set; }
        public double control_jump { get; set; }
        public double control_run { get; set; }
        public double control_down { get; set; }
        public double control_zoomminus { get; set; }
        public double invincibility { get; set; }
        public double control_zoomreset { get; set; }
        public double music { get; set; }
        public double control_right { get; set; }
        public double control_up { get; set; }
        public double free_icon { get; set; }

        public bool Equals(Root2 x, Root2 y)
        => x.control_zoomminus == y.control_zoomminus &&
            x.control_zoomplus == y.control_zoomplus &&
            x.control_pause == y.control_pause &&
            x.control_attack == y.control_attack &&
            x.control_interact == y.control_interact &&
            x.control_jump == y.control_jump &&
            x.control_run == y.control_run &&
            x.control_left == y.control_left &&
            x.control_right == y.control_right &&
            x.control_up == y.control_up &&
            x.control_down == y.control_down;
  

        public int GetHashCode([DisallowNull] Root2 obj)
        {
            throw new NotImplementedException();
        }
    }

}
