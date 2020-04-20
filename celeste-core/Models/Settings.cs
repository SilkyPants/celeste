namespace Celeste.Models
{
    public class Settings
    {
        public string JournalDirectory { get; set; }
        public string BindingsDirectory { get; set; }

        public bool EnableWebSocket { get; set; }
        public int WebSocketPort { get; set; }
    }
}
