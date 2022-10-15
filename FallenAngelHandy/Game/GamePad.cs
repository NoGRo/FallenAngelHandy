using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Timers;
using System.Threading.Tasks;
using SharpDX.XInput;
using System.Windows.Forms;
using Timer = System.Timers.Timer;
using System.Runtime.InteropServices;

namespace FallenAngelHandy
{
    public class GamePad
    {
        private Controller controller = new Controller(UserIndex.One);
        private System.Timers.Timer timer = new Timer(1000);

        const short ThumbTolerance = 20000;

        const UInt32 WM_KEYDOWN = 0x0100;
        const UInt32 WM_KEYUP = 0x0101 ;
        const int VK_KEY_A = 0x41;
        const int VK_KEY_B = 0x42;
        const int VK_KEY_X = 0x58;
        const int VK_KEY_T = 0x54;
        const int VK_DOWN = 0x28;
        const int VK_RIGHT = 0x27;
        const int VK_UP = 0x26;
        const int VK_LEFT = 0x25;
        const int VK_ESCAPE = 0x1B;

        const int VK_OEM_PLUS = 0xBB;
        const int VK_OEM_MINUS = 0xBD;

        const int VK_F5 = 0x74;

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        public Dictionary<GamepadButtonFlags, int> PadKeyDic
            = new Dictionary<GamepadButtonFlags, int>()
            { 
                {GamepadButtonFlags.A, VK_KEY_A },
                {GamepadButtonFlags.B,VK_KEY_B },
                {GamepadButtonFlags.X, VK_KEY_X},
                //{GamepadButtonFlags.Y, "" },
                
                {GamepadButtonFlags.Start, VK_ESCAPE },

                {GamepadButtonFlags.LeftShoulder, VK_OEM_MINUS },
                {GamepadButtonFlags.RightShoulder, VK_OEM_PLUS },

                {GamepadButtonFlags.DPadUp, VK_UP },
                {GamepadButtonFlags.DPadDown , VK_DOWN },
                {GamepadButtonFlags.DPadLeft, VK_LEFT },
                {GamepadButtonFlags.DPadRight, VK_RIGHT }
                
            };

        public GamePad()
        {
            KeyState = PadKeyDic.Values.Distinct().ToDictionary(x => x, y => false);
            KeyState.Add(VK_KEY_T, false);

            timer.Elapsed += UpdateState;
            timer.Start();
        }


        private State previousState { get; set; }
        private void UpdateState(object sender, ElapsedEventArgs e)
        {

            if (!controller.IsConnected)
            {
                timer.Interval = 1000;
                return;
            }

            timer.Interval = 10;

            var state = controller.GetState();

            if (previousState.PacketNumber == state.PacketNumber)
                return;


            previousState = state;
            Debug.WriteLine(state.Gamepad.Buttons);

            bool skipArrows = 
                   state.Gamepad.LeftThumbX is > ThumbTolerance or < -ThumbTolerance
                || state.Gamepad.LeftThumbY is > ThumbTolerance or < -ThumbTolerance;



            if (state.Gamepad.LeftThumbX > ThumbTolerance)
                sendKeyDown(VK_RIGHT);
            else if (state.Gamepad.LeftThumbX < -ThumbTolerance)
                sendKeyDown(VK_LEFT);
            else
            {
                sendKeyUp(VK_RIGHT);
                sendKeyUp(VK_LEFT);
            }
            if (state.Gamepad.LeftThumbY > ThumbTolerance)
                sendKeyDown(VK_UP);
            else if (state.Gamepad.LeftThumbY < -ThumbTolerance)
                sendKeyDown(VK_DOWN);
            else
            {
                sendKeyUp(VK_UP);
                sendKeyUp(VK_DOWN);
            }

            if(state.Gamepad.RightTrigger > 50)
                sendKeyDown(VK_KEY_T);
            else
                sendKeyUp(VK_KEY_T); 

            foreach (var button in PadKeyDic.Keys)
            {
                int keyboardKey = PadKeyDic[button];

                if(skipArrows && VK_UP == keyboardKey)
                    break;

                if ((button & state.Gamepad.Buttons) != 0 )
                    sendKeyDown(keyboardKey);
                else
                    sendKeyUp(keyboardKey);
            }
        }


        private Dictionary<int,bool> KeyState = new Dictionary<int,bool>();
        private void sendKeyUp(int keyboardKey)
        {
            if (!KeyState[keyboardKey])
                return;

            Process[] processes = Process.GetProcessesByName("Fallen Angel Marielle");

            foreach (Process proc in processes)
                PostMessage(proc.MainWindowHandle, WM_KEYUP, keyboardKey, 0);
            Debug.WriteLine($"key Up   {keyboardKey}");
            KeyState[keyboardKey] = false;
        }

        private void sendKeyDown(int keyboardKey)
        {
            if (KeyState[keyboardKey])
                return;
            Process[] processes = Process.GetProcessesByName("Fallen Angel Marielle");

            foreach (Process proc in processes)
                PostMessage(proc.MainWindowHandle, WM_KEYDOWN, keyboardKey, 0);
            Debug.WriteLine($"key Down {keyboardKey}");

            KeyState[keyboardKey] = true;
            //throw new NotImplementedException();
        }
    }
}
