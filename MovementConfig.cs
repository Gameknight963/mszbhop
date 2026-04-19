using MelonLoader.Utils;
using Newtonsoft.Json;
using System.IO;

namespace mszbhop
{
    public class MovementConfig
    {
        [JsonIgnore]
        private const float HU_TO_UNITY = 0.0254f;

        public float MaxGroundSpeed { get; set; } = 320f;
        public float GroundAccel { get; set; } = 600f;
        public float AirAccel { get; set; } = 150f;
        public float Friction { get; set; } = 4f;
        public float JumpVelocity { get; set; } = 268f;
        public float AirCap { get; set; } = 320f;

        public float MaxGroundSpeedUnity => MaxGroundSpeed * HU_TO_UNITY;
        public float GroundAccelUnity => GroundAccel * HU_TO_UNITY;
        public float AirAccelUnity => AirAccel * HU_TO_UNITY;
        public float JumpVelocityUnity => JumpVelocity * HU_TO_UNITY;
        public float AirCapUnity => AirCap * HU_TO_UNITY;

        public void Reload()
        {
            string path = Path.Combine(MelonEnvironment.ModsDirectory, "mszbhop", "MovementConfig.json");
            if (File.Exists(path))
                JsonConvert.PopulateObject(File.ReadAllText(path), this);
        }

        public MovementConfig()
        {
            string path = Path.Combine(MelonEnvironment.ModsDirectory, "mszbhop", "MovementConfig.json");
            if (!File.Exists(path))
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
                return;
            }
            JsonConvert.PopulateObject(File.ReadAllText(path), this);
        }
    }
}
