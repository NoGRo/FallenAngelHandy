using Buttplug;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using static Buttplug.ServerMessage.Types;
using FallenAngelHandy.Core;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;

namespace FallenAngelHandy
{
    public static class HandyService
    {
        private static Timer timerReconnect = new Timer(20000);
        private static int pauseTime;

        public static bool isReady { get; set; }
        private static Timer timerCmdEnd = new Timer();
        
        public static event EventHandler QueueEnd;
        public static event EventHandler<string> StatusChange;

        private static int CurrentTime; //=> (DateTime.Now - SyncSend).TotalMilliseconds;

        private static long timeSyncAvrageOffset;
        private static long timeSyncInitialOffset;
        private static HttpClient Client = new HttpClient() { BaseAddress = new Uri("https://www.handyfeeling.com/api/handy/v2/") };


        public static void init() 
        {
            
            Client.DefaultRequestHeaders.Add("accept", "application/json");

            timerCmdEnd.Elapsed += TimerCmdEnd_Elapsed;
        }

        private static void TimerCmdEnd_Elapsed(object sender, ElapsedEventArgs e)
        {
            timerCmdEnd.Stop();
            QueueEnd.Invoke(null,e);
            
        }

        public static async Task Connect()
        {
            string Key = Game.Config.HandyKey;
            OnStatusChange("Connecting Handy");
            isReady = false;
            Client.DefaultRequestHeaders.Remove("X-Connection-Key");
            Client.DefaultRequestHeaders.Add("X-Connection-Key", Key);

            var resp =  await Client.GetAsync("connected");
            if (resp.StatusCode != System.Net.HttpStatusCode.OK) {
                OnStatusChange("Can't Connect to Handy");
                return;
            }
                

            var status = JsonConvert.DeserializeObject<ConnectedResponse>(await resp.Content.ReadAsStringAsync());

            if (!status.connected)
            {
                OnStatusChange("Handy is not Conected");
                return;
            }


            OnStatusChange("Uploading & Sync");
            var blob = uploadBlob(GalleryRepository.Assets["csv"]);

            resp =  await Client.PutAsync("mode", new StringContent(JsonConvert.SerializeObject(new ModeRequest(1)), Encoding.UTF8, "application/json"));


            if (resp.StatusCode != System.Net.HttpStatusCode.OK)
            {
                OnStatusChange("Server fail Response");
                return;
            }
                

            var upload = UploadHandy(await blob);
            await updateServerTime();
            OnStatusChange("Uploading");
            await upload;
            isReady = true;
            OnStatusChange("Connected");
            QueueEnd.Invoke(null,new EventArgs());
        }

        private static void OnStatusChange(string e)
        {
            StatusChange?.Invoke(null, e);
        }

        public static async Task Resume()
        {
            await Seek(pauseTime);
        }

        public static async Task Pause()
        {
            pauseTime = CurrentTime;
        }

        public static async Task SendGallery(string GalleryName)
        {
            var gallery = GalleryRepository.Get(GalleryName);
            if (gallery == null)
            {
                Debug.Write($"Not Gallery Found: {GalleryName} ");
                gallery = GalleryRepository.Get("masturbate_1");
            }

            timerCmdEnd.Interval = gallery.Duration;
            timerCmdEnd.Start();
            await Seek(gallery.StartTime);

        }
        public static async Task Seek(int time)
        {
            var req = new SyncPlayRequest(ServerTime, time);
            var resp  = await Client.PutAsync("hssp/play", new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json"));
        }


        private static long ServerTime => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + timeSyncInitialOffset + timeSyncAvrageOffset;
        private static async Task updateServerTime()
        {
            var totalCalls = 30;
            var discardTopBotom = 2;
            //warm up
            _ = await getServerOfsset();


            timeSyncInitialOffset = await getServerOfsset();

            var offsets = new List<long>();
            for (int i = 0; i < 30; i++)
            {
                offsets.Add(await getServerOfsset() - timeSyncInitialOffset);
            }
            timeSyncAvrageOffset = Convert.ToInt64(
                                        offsets.OrderBy(x => x)
                                            .Take(totalCalls-discardTopBotom).TakeLast(totalCalls - (discardTopBotom*2)) //discard TopBotom Extreme cases
                                            .Average()
                                    );

        }
        private static async Task<long> getServerOfsset() {
            var sendTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var result = await Client.GetAsync("servertime");
            var receiveTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var resp = JsonConvert.DeserializeObject<ServerTimeResponse>(await result.Content.ReadAsStringAsync());
            var estimatedServerTimeNow = resp.serverTime + (receiveTime - sendTime) / 2;
            return estimatedServerTimeNow - receiveTime;
        }

        public static async Task UploadHandy(string scriptUrl)
        {
            var resp = await Client.PutAsync("hssp/setup", new StringContent(JsonConvert.SerializeObject(new SyncUpload(scriptUrl)), Encoding.UTF8, "application/json"));
        }
        private static async Task<string> uploadBlob(FileInfo file)
        {

            using (var blobClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://www.handyfeeling.com/api/sync/upload");

                var content = new MultipartFormDataContent
                {
                    { new StreamContent(file.OpenRead()), "syncFile", "FalenAngelAssets.csv" }
                };

                request.Content = content;

                var resp = await blobClient.SendAsync(request);

                return JsonConvert.DeserializeObject<SyncUpload>(await resp.Content.ReadAsStringAsync()).url;
            }
        }


    }
    public record ServerTimeResponse(long serverTime);
    public record SyncPlayRequest(long estimatedServerTime,long startTime);
    public record SyncUpload(string url);
    public record ConnectedResponse(bool connected);
    public record ModeRequest(int mode);
}
