
using System.Threading.Tasks;
using Celeste.Utility;

namespace Celeste.Services {

    public class VirtualControlsService
    {
        private readonly IVirtualControls _virtualControls;

        public VirtualControlsService(IVirtualControls virtualControls)
        {
            _virtualControls = virtualControls;
        }

        ///
        /// Relies on https://github.com/SimonCropp/TextCopy for Clipboard management
        public async Task SetClipboardAsync(string clipboardText) {
            await TextCopy.Clipboard.SetTextAsync(clipboardText);
        }

        public async Task<string> GetClipboardAsync() {
            return await TextCopy.Clipboard.GetTextAsync();
        }
    }
}