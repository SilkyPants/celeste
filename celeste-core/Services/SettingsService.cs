using Celeste.Models;

using System;
using System.IO;
using System.Text.Json;

namespace Celeste.Services
{
    public class SettingsService
    {
        private static readonly string settingsFilename = "settings.json";
        public event EventHandler<Settings> OnSettingsChange;

        private static Settings _currentSettings = new Settings()
        {
            JournalDirectory = "JournalDirectory",
            BindingsDirectory = "BindingsDirectory",
            EnableWebSocket = true,
            WebSocketPort = 83403,
        };

        public SettingsService() {

            if (File.Exists(settingsFilename)) {
                var json = File.ReadAllText(settingsFilename);
                _currentSettings = JsonSerializer.Deserialize<Settings>(json);
            }
        }

        public Settings Get() => _currentSettings;

        public bool Set(Settings newSettings)
        {
            _currentSettings = newSettings;
            
            string settingsJson = JsonSerializer.Serialize<Settings>(_currentSettings);
            File.WriteAllText(settingsFilename, settingsJson);

            OnSettingsChange?.Invoke(this, _currentSettings);

            return true;
        }
    }
}
