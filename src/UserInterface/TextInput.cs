using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UserInterface
{
    public class TextInput : DrawableGameComponent, IMenuItem
    {
        private Texture2D texture;
        private SpriteFont inputFont;
        private SpriteFont titleFont;

        public string Title { get; set; }
        public string InputText { get; set; }
        public Vector2 Position { get; set; }

        public TextInput(Game game, Texture2D texture, SpriteFont inputFont, SpriteFont titleFont) 
            : base(game)
        {
            this.texture = texture;
            this.inputFont = inputFont;
            this.titleFont = titleFont;
            InputText = "";
        }

        public void Update()
        {
            // Logic for textinput simple name in lowercase
            // Enter submits and return removes character
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                new Vector2(Position.X + 400, Position.Y),
                null,
                Color.White
            );
            spriteBatch.DrawString(
                inputFont,
                InputText,
                new Vector2(Position.X + 420, Position.Y + 20),
                Color.Black
                );

            spriteBatch.DrawString(
                titleFont,
                Title,
                new Vector2(Position.X - 700, Position.Y - 40),
                Color.Black
                );
        }
    
    }
}