using UnityEngine;

namespace mszbhop
{
    public static class MovementFunctions
    {
        private const float HU_TO_UNITY = 0.0254f;

        public static float MaxGroundSpeed = 320f * HU_TO_UNITY;
        public static float GroundAccel = 600f * HU_TO_UNITY;
        public static float AirAccel = 10f * HU_TO_UNITY;
        public static float Friction = 4f;
        public static float JumpVelocity = 268f * HU_TO_UNITY;
        public static float AirCap = 320f * HU_TO_UNITY;

        public static void ApplyFriction(ref Vector3 velocity)
        {
            float speed = velocity.magnitude;
            if (speed < 0.1f) return;

            float drop = speed * Friction * Time.deltaTime;
            float newSpeed = Mathf.Max(speed - drop, 0.0f);

            velocity *= newSpeed / speed;
        }
        public static void Accelerate(ref Vector3 velocity, Vector3 wishDir, float wishSpeed, float accel)
        {
            float currentSpeed = Vector3.Dot(velocity, wishDir);
            float addSpeed = wishSpeed - currentSpeed;
            if (addSpeed <= 0) return;

            float accelSpeed = accel * Time.deltaTime * wishSpeed;
            if (accelSpeed > addSpeed) accelSpeed = addSpeed;

            velocity += wishDir * accelSpeed;
        }

        public static void AirMove(ref Vector3 velocity, Vector3 wishDir)
        {
            float wishSpeed = MaxGroundSpeed;

            if (wishSpeed > AirCap)
            {
                wishSpeed = AirCap;
            }

            Accelerate(ref velocity, wishDir, wishSpeed, AirAccel);
        }

        public static void GroundMove(ref Vector3 velocity, Vector3 wishDir, bool applyFriction)
        {
            if (applyFriction)
            {
                ApplyFriction(ref velocity);
            }

            float wishSpeed = MaxGroundSpeed;
            Accelerate(ref velocity, wishDir, wishSpeed, GroundAccel);
        }
    }
}
