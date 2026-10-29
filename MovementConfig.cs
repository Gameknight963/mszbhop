using Newtonsoft.Json;

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
    }
}
