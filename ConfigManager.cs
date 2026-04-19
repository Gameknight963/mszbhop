using MelonLoader;
using MelonLoader.Utils;
using System.IO;
using System.Threading;

namespace mszbhop
{
    public static class ConfigManager
    {
        public static MovementConfig MovementConfig = new MovementConfig();
        private static readonly FileSystemWatcher watcher;

        static ConfigManager()
        {
            string dir = Path.Combine(MelonEnvironment.ModsDirectory, "mszbhop");
            watcher = new FileSystemWatcher(dir, "MovementConfig.json");
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += (s, e) =>
            {
                Thread.Sleep(100);
                MovementConfig.Reload();
                MelonLogger.Msg($"Config reloaded!");
            };
            watcher.EnableRaisingEvents = true;
        }
    }
}
