using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace TankAttack
{
    class WeaponType : DrawableGameComponent 
    {
        // Needs:

        // Texture representaion for firing
        public Texture2D FireTexture { get; private set; }
        public Texture2D ProjectileTexture { get; private set; }

        // Sound effect when fired
        public SoundEffect FireSound { get; set; }

        // Weapon Name
        public string Name { get; set; }
        // Damage
        public int Damage { get; set; }
        // Speed
        public float Speed { get; set; }
        // Cooldown time
        public float Cooldown { get; set; }

        private List<Projectile> projectiles;
        private Game game;

        public WeaponType(
            Game game, 
            string name,
            Texture2D fireTexture,
            Texture2D projectileTexture,
            SoundEffect soundEffect)
            : base(game)
        {
            Name = name;
            FireTexture = fireTexture;
            ProjectileTexture = projectileTexture;
            FireSound = soundEffect;
            projectiles = new List<Projectile>();
            this.game = game;
        }


        public void Update()
        {
            for (int i = projectiles.Count -1; i >= 0; i--)
            {
                if (ProjectileOutOfFrame(projectiles[i]))
                { RemoveProjectile(projectiles[i]); }

                else { projectiles[i].Update(); }
            }
            // FIXME Debugging
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }

        public void AddProjectile(Player player)
        {

            var projectile = new Projectile(
                Game, player.Position, player.TurretRotation, ProjectileTexture);
            projectiles.Add(projectile);
            TankAttack.gameComponents.Add(projectile);
        }


        public void RemoveProjectile(Projectile projectile)
        {
            projectiles.Remove(projectile);
        }

        public bool ProjectileOutOfFrame(Projectile projectile)
        {
            // TODO Destroy projectile
            if (projectile.Position.X < 0)
                return true;
            if (projectile.Position.X > Globals.ScreenWidth)
                return true;
            if (projectile.Position.Y < 0)
                return true;
            if (projectile.Position.Y > Globals.ScreenHeight)
                return true;
            else return false;
        }

        public List<Projectile> GetProjectiles()
        {
            return projectiles;
        }
    }
}