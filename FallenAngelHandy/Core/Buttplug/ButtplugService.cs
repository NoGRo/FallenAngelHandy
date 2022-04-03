using Buttplug;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace FallenAngelHandy
{
    public static class ButtplugService
    {
        private static Timer timerReconnect = new Timer(20000);
        public static ButtplugClient client { get; set; }
        public static ButtplugClientDevice device { get; set; }

        #region Device And Connection



        public static async Task init()
        {
            timerReconnect.Elapsed += timerReconnectevent;
            timerCmdEnd.Elapsed += OnCommandEnd;
            await Connect();
        }
        public static event EventHandler<CmdLinear> CommandEnd;
        public static event EventHandler<CmdLinear> QueueEnd;

        public static event EventHandler<string> StatusChange;
        private static void OnStatusChange(string e)
        {
            StatusChange?.Invoke(null, e);
        }

        public async static Task Connect()
        {
            OnStatusChange("Connecting...");

            if (client != null)
            {
                if (client.Connected)
                    await client.DisconnectAsync();

                client.Dispose();
                client = null;
                device?.Dispose();
                device = null;
                timerReconnect.Enabled = false;
            }
            client = new ButtplugClient("Fallen Angel Marielle");

            client.DeviceAdded += Client_DeviceAdded;
            client.DeviceRemoved += Client_DeviceRemoved;
            client.ErrorReceived += Client_ErrorReceived;
            client.ServerDisconnect += Client_ServerDisconnect;
            client.ScanningFinished += Client_ScanningFinished;

            try {
            
                await client.ConnectAsync(new ButtplugWebsocketConnectorOptions(new Uri(Game.Config.ButtplugUrl)));
            }
            catch (ButtplugConnectorException ex)
            {
                OnStatusChange("Can't Connect");
                return;
            }

            if (client.Connected)
                OnStatusChange("Connected");

            foreach (var buttplugClientDevice in client.Devices)
            {
                AddDevice(buttplugClientDevice);
            }
            if (device == null)
            {
                OnStatusChange($"Scanning For Devices");
                await client.StartScanningAsync();
            }
                
        }
        private static void AddDevice(ButtplugClientDevice Device)
        {
            if (Device.AllowedMessages.ContainsKey(ServerMessage.Types.MessageAttributeType.LinearCmd)
                || Device.AllowedMessages.ContainsKey(ServerMessage.Types.MessageAttributeType.VibrateCmd))
            {
                
                device = Device;
                client.StopScanningAsync();
                SendCmd(CmdLinear.GetCommandMillis(1500,0)); //homming

                OnStatusChange($"Device Found [{Device.Name}]");
            }
            else if (device == null)
            {
                OnStatusChange($"Incompatible Device [{Device.Name}]");
            }
        }
        private static void RemoveDevice(ButtplugClientDevice Device)
        {

            if (device?.Name != Device?.Name)
            {
                OnStatusChange("Disconnect");
                return;
            }
            
            
            OnStatusChange($"Remove Device {Device?.Name ?? ""}");
            device = null;
        }
        private static void timerReconnectevent(object sender, ElapsedEventArgs e)
        {
            if (!client.Connected)
                Connect();
            else
                timerReconnect.Enabled = false;
        }

        public static bool isReady
            => client != null && client.Connected && device != null;


        #region eventos
        private static void Client_ServerDisconnect(object sender, EventArgs e)
        {
            RemoveDevice(device);
            timerReconnect.Enabled = true;
        }
        private static void Client_DeviceRemoved(object sender, DeviceRemovedEventArgs e)
        {
            RemoveDevice(e.Device);
        }
        private static void Client_ErrorReceived(object sender, ButtplugExceptionEventArgs e)
        {
            RemoveDevice(device);
        }
        private static void Client_ScanningFinished(object sender, EventArgs e)
        {
            if (device == null)
                OnStatusChange($"Scanning Finished, no Device Found");
        }
        private static void Client_DeviceAdded(object sender, DeviceAddedEventArgs e)
        {
            AddDevice(e.Device);
        }
        #endregion

        #endregion



        public static bool Invert { get; set; }
        private static Timer timerCmdEnd = new Timer();

        private static List<CmdLinear> queue { get; set; } = new List<CmdLinear>();
        private static CmdLinear LastCommandSent { get; set; }

        public static byte GetCurrentValue()
        {
            if (LastCommandSent?.Sent == null)
                return 0;

            var passes = DateTime.Now - LastCommandSent.Sent.Value;

            if (passes.TotalMilliseconds >= LastCommandSent.Millis)
                return LastCommandSent.Value;
            else
            {
                var distanceToTravel =  LastCommandSent.Value - LastCommandSent.InitialValue;
                var travel = distanceToTravel * (passes.TotalMilliseconds / LastCommandSent.Millis);
                var result = (byte)(Math.Abs(LastCommandSent.InitialValue + Convert.ToInt16(travel)));
                return result;
            }
        }

        public static void StopClear()
        {
            Stop();
            queue.Clear();
        }
        public static async Task Resume()
        {
            if (!queue.Any())
                return;

            await SendCmd(queue.First());
            queue.RemoveAt(0);
        }

        public static async Task Pause() 
            => Stop();


        public static async Task SendGallery(string GalleryName)
        {
            await SendCmd(GalleryRepository.Get(GalleryName));
        }
        public static async Task SendCmd(List<CmdLinear> cmds)
        {
            queue = cmds.ToList();
            await Resume();
        }

        public static async Task InsertCmd(List<CmdLinear> cmds)
        {
            var passes = DateTime.Now - LastCommandSent.Sent.Value;
            LastCommandSent.Millis -= passes.Milliseconds;

            cmds.Add(LastCommandSent);
            queue.InsertRange(0, cmds);

           await SendCmd(queue.First());
            queue.RemoveAt(0);

        }
        private static Task sendtask;
        public static async Task SendCmd(CmdLinear cmd)
        {            
            if (!isReady) 
                return;

            if (Invert)
                cmd.Value = Convert.ToByte(100-Convert.ToInt32(cmd.Value));


            var start = DateTime.Now;
            if (device.AllowedMessages.ContainsKey(ServerMessage.Types.MessageAttributeType.LinearCmd))
            {
                sendtask = device.SendLinearCmd(cmd.buttplugMillis, cmd.LinearValue);
            }
            else if (device.AllowedMessages.ContainsKey(ServerMessage.Types.MessageAttributeType.VibrateCmd))
            {
                sendtask = device.SendVibrateCmd(cmd.vibrateValue);
            }
              
            var pases = (DateTime.Now - start).TotalMilliseconds;

            cmd.Sent = DateTime.Now;
            LastCommandSent = cmd;

            timerCmdEnd.Stop();
            timerCmdEnd.Interval = cmd.Millis < pases ? 1 : cmd.Millis - pases;
            timerCmdEnd.Start();
            await sendtask;
        }
        private static void OnCommandEnd(object sender, ElapsedEventArgs e)
        {
            timerCmdEnd.Stop();
            if (queue.Any()) 
            {
                var cmd = queue.First();
                queue.RemoveAt(0);
                SendCmd(cmd);
                
            }
            else 
            {
                QueueEnd?.Invoke(null, LastCommandSent);
            }

           // CommandEnd?.Invoke(null, LastCommandSent);
        }

        public static async Task Stop()
        {
            timerCmdEnd.Stop();
            LastCommandSent.Stoped = DateTime.Now;
            await device.SendStopDeviceCmd();
        }



    }
}
