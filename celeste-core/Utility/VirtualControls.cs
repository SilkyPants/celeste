
namespace Celeste.Utility
{
    public interface IVirtualControls
    {
        enum KeyState
        {
            Tap,
            Down,
            Up
        }

        bool CanRun { get; }

        bool TapKey(string key, int delay);
        bool KeyUp(string key);
        bool KeyDown(string key);
        bool ProcessKey(string key, KeyState state);
        void SendKeys(string output);
    }
}