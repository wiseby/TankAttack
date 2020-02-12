using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankAttack
{
    public class DebugWindow : DrawableGameComponent
    {

        public bool IsVisible { get; set; }
        
        public Vector2 Position { get; set; }
        
        private SpriteFont outputSpriteFont;
        private Vector2 textOrigin;
        private Rectangle textBox;
        private Vector2 textPosition;

        // TODO Gör en Set metod som begränsar antalet rader som får plats.
        public List<string> Output { get; set; }
        
        private Vector2 frameOrigin;
        private Texture2D frameTexture;

        private Dictionary<string, SpriteFont> spriteFonts;
        private Dictionary<string, Texture2D> spriteAssets;

        public DebugWindow(Game game, bool isVisible, Dictionary<string, SpriteFont> spriteFonts, Dictionary<string, Texture2D> spriteAssets) : base(game)
        {
            IsVisible = isVisible;
            this.spriteFonts = spriteFonts;
            this.spriteAssets = spriteAssets;
            LoadContent();
            Initialize();
            Output = new List<string>();
        }

        public override void Initialize()
        {
            Position = new Vector2(1510, 870);
            frameOrigin = new Vector2(0, 0);
            textOrigin = new Vector2(0, 0);
            textPosition = new Vector2(
                Position.X + 15,
                Position.Y + 10);
            textBox = new Rectangle(
                (int)Position.X + 20, 
                (int)Position.Y + 20, 
                frameTexture.Width - 20, 
                frameTexture.Height - 20);
        }

        protected override void LoadContent()
        {
            frameTexture = spriteAssets["UI/DebugConsole"];
            outputSpriteFont = spriteFonts["SpriteFonts/DebugOutput"];
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                // TODO Check origin
                spriteBatch.Draw(
                    frameTexture, 
                    Position,
                    Color.White);
                for (int i = 0; i < Output.Count; i++)
                {
                    spriteBatch.DrawString(
                        outputSpriteFont,
                        Output[i],
                        new Vector2(
                            textPosition.X,
                            textPosition.Y + (i * 10)),
                        Color.Black,
                        0.0f,
                        textOrigin,
                        1.0f,
                        SpriteEffects.None,
                        0.0f);
                }
            }
        }
    }
}