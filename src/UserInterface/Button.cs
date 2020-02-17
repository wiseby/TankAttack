using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UserInterface
{
    public class Button : DrawableGameComponent, IMenuItem
    {        
        private Texture2D texture;
        private SpriteFont font;

        public string Title { get; set; }
        public Vector2 Position { get; set; }

        public Button(Game game, Texture2D texture, SpriteFont font) 
            : base(game)
        {
            this.texture = texture;
            this.font = font;
        }

        public void Update()
        {
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // spriteBatch.Draw(
            //     texture,
            //     Position,
            //     null,
            //     Color.White,
            //     0.0f,
            //     default,
            //     4.0f,
            //     SpriteEffects.None,
            //     0.0f
            // );
            spriteBatch.DrawString(
                font,
                Title,
                Position,
                Color.Black,
                0.0f,
                new Vector2(font.MeasureString(Title).X / 2, 0),
                1.0f,
                SpriteEffects.None,
                0.0f
                );
        }
    }
}