using Buttplug;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using System.Threading.Tasks;
using System.Threading;
using System.Collections.Specialized;

namespace FallenAngelHandy
{
    public static class AttackScript
    {
        private static double hp;
        private static ScriptBuilder SB = new ScriptBuilder();



        public static async Task Play(string gameEvent, NameValueCollection Data)
        {
            if (!Game.Config.Attacks)
                return;

            /*
            hp = Data.AllKeys.Contains("strength")
                ? string.IsNullOrEmpty(Data["strength"]) ? 0 : Convert.ToDouble(Data["strength"], CultureInfo.GetCultureInfo("en-US").NumberFormat)
                : 0;
            */
            switch (gameEvent)

            {
                case "hit_pain":
                    break;
                case "hit_pleasure":
                    break;
                case "hit_fall":
                    break;
                case "hit_fall_ground":
                    break;
                case "jump_laser":
                case "laser":
                case "ledge_laser":
                    break;

                case "uppercut_laser_prep":
                    break;
                case "uppercut_laser":
                    break;
                case "crounch_laser":
                    break;
                case "stun":
                    await HandyService.SendGallery("stun");
                    break;
                case "reverse":
                    break;
                default:
                    break;
            }
        }
    }
}

