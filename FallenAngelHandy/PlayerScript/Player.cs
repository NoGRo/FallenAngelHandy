using Buttplug;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FallenAngelHandy
{
    //Capture All mesages from GameListener, parse status and cordinate the diferents game modes with the others players
    public static class PlayerScript
    {

        public static PlayerModeEnum Mode;


        public static event EventHandler<string> StatusChange;
        private static void OnStatusChange(string e)
        {
            StatusChange?.Invoke(null, e);
        }

        public static void Init()
        {
            HandyService.QueueEnd += HandyService_QueueEnd;
            Mode = PlayerModeEnum.Filler;

        }



        public static async void GameEventHandler(string gameEvent, NameValueCollection Data)
        {
            if (!HandyService.isReady)
                return;

            switch (gameEvent)
            {
                case "gallery":
                    if (!Game.Config.SexScenes)
                        break;

                    var gallery = Data["code"];
                    if (!string.IsNullOrEmpty(gallery)) //Play Gallery
                    {
                        Mode = PlayerModeEnum.Gallery;
                        await GalleryScriptPlayer.Play(gallery);
                        OnStatusChange($"Gallery {gallery}");
                    }
                    else if (Mode == PlayerModeEnum.Gallery) //stop Gallery -> Filler
                    {
                        Mode = PlayerModeEnum.Filler;
                        await GalleryScriptPlayer.StopAsync();
                        await FillerScript.Play();
                        OnStatusChange("Filler");
                    }
                    break;
                case "Pause":
                    await HandyService.Pause();
                    break;
                case "Resume":
                    await HandyService.Resume();
                    break;
                case "state":
                    ParseStatus(Data);
                    break;

                default:
                    if (!Game.Config.Attacks)
                        break;
                    Mode = PlayerModeEnum.Attack;
                    await AttackScript.Play(gameEvent, Data);
                    OnStatusChange($"Attack!");
                    break;
            }
        }

        private static void ParseStatus(NameValueCollection Data)
        {
            Game.Status.Pleasure = Math.Min(double.Parse(Data["pleasure"]), 100);
            Game.Status.Pain = Math.Min(double.Parse(Data["pain"]), 100);
            Game.Status.Head = Math.Min(double.Parse(Data["head"]), 100);
            Game.Status.Breasts = Math.Min(double.Parse(Data["breasts"]), 100);
            Game.Status.Penis = Math.Min(double.Parse(Data["penis"]), 100);
            Game.Status.Vagina = Math.Min(double.Parse(Data["vagina"]), 100);
            Game.Status.Anus = Math.Min(double.Parse(Data["anus"]), 100);
        }

        private static async void HandyService_QueueEnd(object sender, EventArgs e)
        {
            {
                if (!HandyService.isReady)
                    return;

                switch (Mode)
                {
                    case PlayerModeEnum.Filler:
                        await FillerScript.RePlay();
                        break;
                    case PlayerModeEnum.Attack:
                        Mode = PlayerModeEnum.Filler;
                        await FillerScript.Play();
                        OnStatusChange("Filler");
                        break;
                    case PlayerModeEnum.Gallery:
                        //GameEventHandler("GalleryStop");
                        await GalleryScriptPlayer.RePlay();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}



