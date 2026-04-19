using UnityEngine;

namespace mszbhop
{
    public static class MovementFunctions
    {
        private static MovementConfig MCfg => ConfigManager.MovementConfig;

        public static void ApplyFriction(ref Vector3 velocity)
        {
            float speed = velocity.magnitude;
            if (speed < 0.1f) return;

            float drop = speed * MCfg.Friction * Time.fixedDeltaTime;
            float newSpeed = Mathf.Max(speed - drop, 0.0f);

            velocity *= newSpeed / speed;
        }
        public static void Accelerate(ref Vector3 velocity, Vector3 wishDir, float wishSpeed, float accel)
        {
            float currentSpeed = Vector3.Dot(velocity, wishDir);
            float addSpeed = wishSpeed - currentSpeed;
            if (addSpeed <= 0) return;

            float accelSpeed = accel * Time.fixedDeltaTime * wishSpeed;
            if (accelSpeed > addSpeed) accelSpeed = addSpeed;

            velocity += wishDir * accelSpeed;
        }

        public static void AirMove(ref Vector3 velocity, Vector3 wishDir)
        {
            float wishSpeed = MCfg.MaxGroundSpeedUnity;

            if (wishSpeed > MCfg.AirCapUnity)
            {
                wishSpeed = MCfg.AirCapUnity;
            }

            Accelerate(ref velocity, wishDir, wishSpeed, MCfg.AirAccelUnity);
        }

        public static void GroundMove(ref Vector3 velocity, Vector3 wishDir, bool applyFriction)
        {
            if (applyFriction)
            {
                ApplyFriction(ref velocity);
            }

            float wishSpeed = MCfg.MaxGroundSpeedUnity;
            Accelerate(ref velocity, wishDir, wishSpeed, MCfg.GroundAccelUnity);
        }
    }
}
