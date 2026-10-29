using MelonLoader;
using MelonLoader.Utils;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace mszbhop
{
    public static class ConfigManager
    {
        public static MovementConfig MovementConfig = new MovementConfig();
        private static readonly FileSystemWatcher watcher;

        public static void Reload()
        {
            string path = Path.Combine(MelonEnvironment.ModsDirectory, "mszbhop", "MovementConfig.json");
            if (File.Exists(path))
                JsonConvert.PopulateObject(File.ReadAllText(path), MovementConfig);
        }

        static ConfigManager()
        {
            string dir = Path.Combine(MelonEnvironment.ModsDirectory, "mszbhop");
            string path = Path.Combine(dir, "MovementConfig.json");
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(dir);
                File.WriteAllText(path, JsonConvert.SerializeObject(MovementConfig, Formatting.Indented));
            }
            
            // possibly redundant, but not worth the several hundred nanoseconds it might save
            JsonConvert.PopulateObject(File.ReadAllText(path), MovementConfig);

            watcher = new FileSystemWatcher(dir, "MovementConfig.json");
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += (s, e) =>
            {
                Thread.Sleep(100);
                Reload();
                MelonLogger.Msg($"Config reloaded!");
            };
            watcher.EnableRaisingEvents = true;
        }
    }
}
