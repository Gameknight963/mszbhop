using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mszbhop
{
    public class MovementConfig
    {
        public float MaxGroundSpeed { get; set; } = 320f;
        public float GroundAccel { get; set; } = 600f;
        public float AirAccel { get; set; } = 100f;
        public float Friction { get; set; } = 4f;
        public float JumpVelocity { get; set; } = 268f;
        public float AirCap { get; set; } = 320f;
    }
}
