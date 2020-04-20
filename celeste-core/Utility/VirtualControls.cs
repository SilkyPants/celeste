
using System.Runtime.InteropServices;

namespace Celeste.Utility
{
    public static class VirtualControls
    {
        public static bool CanRun
        {
            get
            {
                #if WINDOWS
                return true;
                #else
                var proc = new System.Diagnostics.Process
                {
                    StartInfo =
                    {
                        FileName = "xdotool",
                        Arguments = "-v",
                        UseShellExecute = false,
                        RedirectStandardError = false,
                        RedirectStandardInput = false,
                        RedirectStandardOutput = false
                    }
                };
                return proc.Start(); // Linux requires xdotool
                #endif
            }
        }

        public static bool TapKey(string key, int delay)
        {
            return true;
        }

        public static bool KeyUp(string key)
        {
            return true;
        }

        public static bool KeyDown(string key)
        {
            return true;
        }

        private enum KeyState {
            Tap,
            Down,
            Up
        }

        private static bool ProcessKey(string key, KeyState state) {
            return true;
        }

        public static bool LinuxOS
        {
            get { return System.IO.Path.DirectorySeparatorChar == '/'; }
        }

        public static void SendKeys(string output)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var args = "";
                switch (output)
                {
                    case "{RIGHT}":
                        args = "key Right";
                        break;
                    case "{LEFT}":
                        args = "key Left";
                        break;
                    default:
                        if (output.StartsWith("{") && output.EndsWith("}"))
                            output = output.Substring(1, output.Length - 2);

                        args = "type \"" + output + "\"";
                        break;
                }

                var proc = new System.Diagnostics.Process
                {
                    StartInfo =
                    {
                        FileName = "xdotool",
                        Arguments = args,
                        UseShellExecute = false,
                        RedirectStandardError = false,
                        RedirectStandardInput = false,
                        RedirectStandardOutput = false
                    }
                };
                proc.Start();
            }
            else
            {
                #if WINDOWS
                System.Windows.Forms.SendKeys.Send(output);
                #endif
            }
        }
    }
}