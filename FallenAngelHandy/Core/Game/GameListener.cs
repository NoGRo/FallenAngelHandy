using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Globalization;

namespace FallenAngelHandy
{
    public static class GameListener
    {
        public static async void Init() {
            StartListener();
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
        }
        public static event EventHandler<string> GameEventArrive;
        private static void OnGameEventArrive(string e)
        {
            GameEventArrive?.Invoke(null, e);
        }
        public static async void StartListener()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:5050/game/");
            listener.Start();
            
            while (true)
            {
                var context = await listener.GetContextAsync();
                var request = context.Request;
                var response = context.Response;


                
                parseEvent(request);
                response.ContentLength64 = 0;
                var output = response.OutputStream;
                output.Write(new byte[0], 0, 0);
                output.Close();
            }
        }


        private static void parseEvent(HttpListenerRequest request)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            var report = request.Url.GetLeftPart(UriPartial.Path).Split("/").Last();

            if (report != "state")
                OnGameEventArrive($"{DateTime.Now.ToString("mm:ss:ff")}: {request.Url.PathAndQuery}"); 

            switch (report)
            {
                case "state":
                    Game.Status.Pleasure = Math.Min(double.Parse(request.QueryString["pleasure"]),100);
                    Game.Status.Pain = Math.Min(double.Parse(request.QueryString["pain"]),100);
                    Game.Status.Head = Math.Min(double.Parse(request.QueryString["head"]),100);
                    Game.Status.Breasts = Math.Min(double.Parse(request.QueryString["breasts"]),100);
                    Game.Status.Penis = Math.Min(double.Parse(request.QueryString["penis"]),100);
                    Game.Status.Vagina = Math.Min(double.Parse(request.QueryString["vagina"]),100);
                    Game.Status.Anus = Math.Min(double.Parse(request.QueryString["anus"]),100);
                    break;
                case "gallery":
                    var code = request.QueryString["code"];
                    if (!string.IsNullOrEmpty(code))
                        Player.GameEventHandler("GalleryPlay " + code);
                    else if(Player.Mode == PlayerModeEnum.Gallery)
                        Player.GameEventHandler("GalleryStop");

                    break;
                case "hit_pain":
                case "hit_pleasure":
                    Player.GameEventHandler($"{report} {request.QueryString["strength"]}");
                    break;
                default:
                    Player.GameEventHandler(report);
                    break;
            }
        }


    }
}
