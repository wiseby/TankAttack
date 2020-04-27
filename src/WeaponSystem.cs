using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankAttack
{
    class WeaponSystem
    {
        private List<WeaponType> weaponTypes;
        WeaponType activeWeapon;

        public WeaponSystem(Game game, Player player)
        {
            weaponTypes = new List<WeaponType>();
            weaponTypes.Add(
                new WeaponType(
                game, 
                "Default",
                player.Textures["WeaponSystem/BarrelFlame"],
                player.Textures["WeaponSystem/Bullet"],
                null
                ));
            
            activeWeapon = weaponTypes[0];
        }

        public List<WeaponType> GetWeaponTypes()
        {
            return weaponTypes;
        }

        public void Update()
        {
            foreach (var weapontype in weaponTypes)
            {
                weapontype.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var weapontype in weaponTypes)
            {
                weapontype.Draw(spriteBatch);
            }
        }

        public void Fire(Player player)
        {
            activeWeapon.AddProjectile(player);
        }

        public int GetTotalNumberOfProjectiles()
        {
            int projectileCount = 0;
            foreach (var type in weaponTypes)
            {
                projectileCount += type.GetProjectiles().Count;
            }
            return projectileCount;
        }
    }
}