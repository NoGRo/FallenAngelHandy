using Buttplug;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FallenAngelHandy
{
    public static class Player
    {

        public static PlayerModeEnum Mode;


        public static event EventHandler<string> StatusChange;
        private static void OnStatusChange(string e)
        {
            StatusChange?.Invoke(null, e);
        }

        public static void Init() 
        {
            ButtplugService.QueueEnd += ButtplugService_QueueEnd;
        }
        public static async void GameEventHandler(string gameEvent,NameValueCollection Data)
        {
            if (!ButtplugService.isReady)
                return;

            switch (gameEvent)
            {
                case "gallery":

                    var gallery = Data["code"];
                    if (!string.IsNullOrEmpty(gallery)) //PlayGallery
                    {
                        Mode = PlayerModeEnum.Gallery;
                        await Gallery.Play(gallery);
                        OnStatusChange($"Gallery {gallery}");
                    }
                    else if (Mode == PlayerModeEnum.Gallery) //stop -> Filler
                    {
                        Mode = PlayerModeEnum.Filler;
                        await Gallery.StopAsync();
                        await Filler.Play();
                        OnStatusChange("Filler");
                    }
                    break;
                case "Pause":
                    await ButtplugService.Pause();
                    break;
                case "Resume":
                    await ButtplugService.Resume();
                    break;
                case "state":
                    ParseStatus(Data);
                    break;

                default:
                    Mode = PlayerModeEnum.Attack;
                    await Attack.Play(gameEvent,Data);
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

        private static async void ButtplugService_QueueEnd(object sender, CmdLinear e)
        {
            if (!ButtplugService.isReady)
                return;

            switch (Mode)
            {
                case PlayerModeEnum.Filler:
                    await Filler.RePlay();
                    break;
                case PlayerModeEnum.Attack:
                    Mode = PlayerModeEnum.Filler;
                    await Filler.Play();
                    OnStatusChange("Filler");
                    break;
                case PlayerModeEnum.Gallery:
                    //GameEventHandler("GalleryStop");
                    await Gallery.RePlay();
                    break;
                default:
                    break;
            }
        }
    }
    public enum PlayerModeEnum 
    { 
        Filler,
        Attack,
        Gallery
    }
}



