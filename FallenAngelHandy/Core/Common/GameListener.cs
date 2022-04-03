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
            listener.Prefixes.Add(Game.Config.ListenerHost);
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


        private static async void parseEvent(HttpListenerRequest request)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            var report = request.Url.GetLeftPart(UriPartial.Path).Split("/").Last().Trim();

            if (report != "state")
                OnGameEventArrive($"{DateTime.Now.ToString("mm:ss:ff")}: {request.Url.PathAndQuery}");

            Player.GameEventHandler(report, request.QueryString);
       
        }

    }
}