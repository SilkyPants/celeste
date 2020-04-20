
using System.Threading.Tasks;

namespace Celeste.Services {

    public class VirtualControlsService {

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