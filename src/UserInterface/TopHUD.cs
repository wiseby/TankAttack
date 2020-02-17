using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TankAttack
{
    class TopHUD : DrawableGameComponent
    {
        public SpriteFont ScoreSpriteFont { get; set; }
        public SpriteFont NameSpriteFont { get; set; }
        public SpriteFont SelectedWeaponSpriteFont { get; set; }
        public SpriteFont WarningMessageSpriteFont { get; set; }
        public Texture2D CooldownBar { get; set; }
        public Texture2D HealthBar { get; set; }
        public Texture2D ShieldBar { get; set; }

        public string ScoreText { get; set; }
        public string NameText { get; set; }
        public string SelectedWeaponText { get; set; }
        public string WarningMessagesText { get; set; }

        // SpriteFonts
        private Dictionary<string, SpriteFont> spriteFonts;

        public Vector2 Location { get; set; }
        public Rectangle HudRect { get; set; }

        public TopHUD(Game1 game, Dictionary<string, SpriteFont> spriteFonts) : base(game)
        {
            this.spriteFonts = spriteFonts;
            NameSpriteFont = this.spriteFonts["SpriteFonts/Name"];
            ScoreSpriteFont = this.spriteFonts["SpriteFonts/Score"];
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(NameSpriteFont, NameText, Location, Color.Black);
        }

    }
}
