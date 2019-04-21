using siberianoilrush_server.Config.Elements;

namespace siberianoilrush_server.Config
{
    public class Section
    {
        public DBElement DB { get; set; }

        public LogElement Log { get; set; }

        public EmailElement Email { get; set; }

        public UserElement User { get; set; }

        public PvpElement PVP { get; set; }

        public SettingsElement Settings { get; set; }
    }
}
