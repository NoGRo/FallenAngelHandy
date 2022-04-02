using Buttplug;
using System;
using System.Collections.Generic;
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

            GalleryPlayer.Init();

            if (!ButtplugService.isReady)
                return;
        }
        public static void GameEventHandler(string gameEvent)
        {
            if (!ButtplugService.isReady)
                return;

            var acction = gameEvent.Split(" ")[0];
            
            switch (acction)
            {
                case "GalleryPlay":
                    Mode = PlayerModeEnum.Gallery;
                    var gallery = gameEvent.Split(" ")[1];
                    GalleryPlayer.Play(gallery);
                    OnStatusChange($"Gallery {gallery}");
                    break;

                case "GalleryStop":
                    Mode = PlayerModeEnum.Filler;
                    GalleryPlayer.Stop();
                    FillerPlayer.Play();
                    OnStatusChange("Filler");
                    break;

                case "Pause":
                    ButtplugService.Pause();
                    break;
                case "Resume":
                    ButtplugService.Resume();
                    break;
                default:
                    Mode = PlayerModeEnum.Attack;
                    AttackPlayer.Play(gameEvent);
                    OnStatusChange($"Attack!");
                    break;
            }
        }
        private static void ButtplugService_QueueEnd(object sender, CmdLinear e)
        {
            switch (Mode)
            {
                case PlayerModeEnum.Filler:
                    FillerPlayer.RePlay();
                    break;
                case PlayerModeEnum.Attack:
                    Mode = PlayerModeEnum.Filler;
                    FillerPlayer.Play();
                    OnStatusChange("Filler");
                    break;
                case PlayerModeEnum.Gallery:
                    //GameEventHandler("GalleryStop");
                    GalleryPlayer.RePlay();
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



