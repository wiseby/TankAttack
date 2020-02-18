using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace TankAttack
{
    public class Collision
    {
        private TimeSpan prevTime, currTime;

        public Collision()
        {
            prevTime = new TimeSpan();
        }

        public void CheckColliders(List<IGameObject> gameObjects, GameTime gameTime)
        {
            currTime = gameTime.TotalGameTime;
            TimeSpan elapsedTime = currTime.Subtract(prevTime);


            //TankAttack.debugWindow.Output.Add($"Time to check Collitions: {elapsedTime.CompareTo(Globals.CollisionCheckInterval)}");

            if (elapsedTime.CompareTo(Globals.CollisionCheckInterval) == 1)
            {
                //TODO Left off!
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    for (int comparaIndex = 0; comparaIndex < gameObjects.Count; comparaIndex++)
                    {
                        if (comparaIndex != i)
                        {
                            // If collision is detected send GameObjects to ResolveCollision()
                            bool hasCollided = gameObjects[i].CollisionCircle.Intersects(
                                gameObjects[comparaIndex].CollisionCircle);
                            
                            if (hasCollided)
                            {
                                ResolveCollision(gameObjects[i], gameObjects[comparaIndex]);
                                // Resetting hasCollided for next check.
                                hasCollided = false;
                            }
                        }
                    }
                }
                prevTime = currTime;
            }
        }

        private void ResolveCollision(IGameObject colliderA, IGameObject colliderB)
        {
            
            if (colliderA is Projectile && colliderB is Projectile)
            {
                // If both colliders are projectiles, destroy them.
                colliderA.GotHit = true;
                colliderB.GotHit = true;
                MainGame.debugWindow.Output.Add("Projectile Collision!!!");

            }

            if ((colliderA is Player && colliderB is Projectile) || (colliderA is Projectile && colliderB is Player))
            {
                // If either one collider is projectiles and the other a player.
                MainGame.debugWindow.Output.Add("Player Hit!!!");
                Debug.WriteLine("Player Hit!");
                
                // Player GotHit = true
                colliderA.GotHit = true;
                colliderB.GotHit = true;
            }

            if (colliderA is Player && colliderB is Player)
            {
                // If either one collider is projectiles and the other a player.
                MainGame.debugWindow.Output.Add("Player bumped Player!!!");
                colliderA.Collided = true;
                colliderB.Collided = true;


                // Player GotHit = true
            }
        }
    }
}