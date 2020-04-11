using Celeste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Celeste.Services
{
    public class SettingsService
    {
        public event EventHandler<Settings> OnSettingsChange;

        private static Settings _currentSettings = new Settings()
        {
            JournalDirectory = "JournalDirectory",
            BindingsDirectory = "BindingsDirectory",
            EnableWebSocket = true,
            WebSocketPort = 83403,
        };

        public Settings Get() => _currentSettings;

        public bool Set(Settings newSettings)
        {
            _currentSettings = newSettings;
            // TODO: Save to file

            OnSettingsChange?.Invoke(this, _currentSettings);

            return true;
        }
    }
}
