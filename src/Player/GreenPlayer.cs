using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankAttack
{
    class GreenPlayer : Player
    {
        public GreenPlayer(
            Game game, 
            Vector2 startPosition, 
            Dictionary<string, 
            Texture2D> textures,
            Dictionary<string, SpriteFont> spriteFonts) 
            : base(game, startPosition, textures, spriteFonts)
        {
            this.TankHullTexture = textures["GreenTank/GreenTankHull"];
            this.TankTurretTexture = textures["GreenTank/GreenTurret"];
            Initialize();
        }

        public override void Initialize()
        {
            topHud.Location = new Vector2(10, 10);
        }

        public override void Interact(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Q))
            TurretRotation -= Globals.TurretRotationSpeed;

            if (keyState.IsKeyDown(Keys.E))
                TurretRotation += Globals.TurretRotationSpeed;

            if (keyState.IsKeyDown(Keys.D))
                HullRotation += Globals.RotationSpeed;

            if (keyState.IsKeyDown(Keys.A))
                HullRotation -= Globals.RotationSpeed;

            if (keyState.IsKeyDown(Keys.W))
            {
                Accelerate();
                Position = Speed;
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                Decelerate();
                Position = Speed;
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                if (FireCooldown == 0) 
                {
                    weaponSystem.Fire(this);
                }
            }   
        }
    }
}