using System;

namespace TankAttack
{
    class Globals
    {
        public static int ScreenWidth = 1920;
        public static int ScreenHeight = 1080;

        public static TimeSpan CollisionCheckInterval = new TimeSpan(0, 0, 0, 0, 100);

        public static float RotationSpeed = 0.07f;
        public static float TankSpeed = 3.0f;
        public static float ProjectileSpeed = 6.0f;
        public static float FireCooldownLimit = 2.0f;

        public static float TurretRotationSpeed = 0.05f;

        public static float TankScale = 0.8f;

        // DebugWindow settings:
        public static int DebugWindowWidth = 200;
        public static int DebugWindowHeight = 100;
    }
}
