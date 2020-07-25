using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace Celeste.Utility
{
    class VirtualControls : IVirtualControls
    {
        private readonly InputSimulator inputSimulator = new InputSimulator();

        public bool CanRun => true;

        /// <summary>
        /// Sends a key tap command
        /// </summary>
        /// <param name="key">Key to tap</param>
        /// <param name="delay">Delay between down and up messages in milliseconds (default: 30 ms)</param>
        /// <returns>True if successful</returns>
        public bool TapKey(string key, int delay = 30)
        {
            var keyCode = MapKeyToKeyCode(key);
            inputSimulator.Keyboard.KeyPress(keyCode);

            return true;
        }

        private VirtualKeyCode MapKeyToKeyCode(string key)
        {
            switch (key)
            {
                case "A":
                    return VirtualKeyCode.VK_A;
            }
        }

        public bool KeyDown(string key)
        {
            ProcessKey(key, IVirtualControls.KeyState.Down);
            return true;
        }

        public bool KeyUp(string key)
        {
            ProcessKey(key, IVirtualControls.KeyState.Up);
            return true;
        }

        public bool ProcessKey(string key, IVirtualControls.KeyState state)
        {
            throw new NotImplementedException();
        }

        public bool SendKey(VirtualKeys key, IVirtualControls.KeyState state)
        {
            if (!CanRun) return false;
            try
            {
                var keyNotification = state == IVirtualControls.KeyState.Down ? WindowMessages.WM_KEYDOWN : WindowMessages.WM_KEYUP;
                // Docs say the lParam should be these values, but might not be needed?
                var lParam = state == IVirtualControls.KeyState.Down ? 0x400000 : 0xC00000;
                //var lParam = IntPtr.Zero;

                // Result should be Zero if it was OK
                Console.WriteLine($"Sending keystroke: {key} for {(int)keyNotification}");
                SetForegroundWindow(eliteProcess.MainWindowHandle);
                var result = PostMessage(new IntPtr(eliteProcess.MainWindowHandle.ToInt32()), (uint)keyNotification, (int)key, lParam);
                Console.WriteLine($"Result: {result}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }

            return false;
        }

        private void Tap(VirtualKeys key)
        {
            SendKey(key, IVirtualControls.KeyState.Down);
            Thread.Sleep(30);
            SendKey(key, IVirtualControls.KeyState.Up);
        }

        public void SendKeys(string output)
        {

            input.Keyboard.TextEntry(output);
        }
    }
}
