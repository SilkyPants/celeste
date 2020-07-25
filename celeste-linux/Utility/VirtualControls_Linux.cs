namespace Celeste.Utility
{
    public class VirtualControls : IVirtualControls
    {
        public bool CanRun
        {
            get
            {
                // TODO: Ensure ED is running
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
            }
        }

        public bool TapKey(string key, int delay)
        {
            return true;
        }

        public bool KeyUp(string key)
        {
            return true;
        }

        public bool KeyDown(string key)
        {
            return true;
        }

        public void SendKeys(string output)
        {
            string args;
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

        public bool ProcessKey(string key, IVirtualControls.KeyState state)
        {
            throw new System.NotImplementedException();
        }
    }
}