using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UserInterface
{
    public class Button : DrawableGameComponent, IMenuItem
    {        
        private Texture2D texture;
        private SpriteFont font;
        public bool IsActive { get; set; }
        public string InputText { 
            get 
            {
                // Logging
                throw new NotImplementedException();
            }
            set 
            {
                throw new NotImplementedException();
            }
        } 
        public string Title { get; set; }
        public string MenuMarker { get; set; } = " ++ ";
        public Vector2 Position { get; set; }

        public Button(Game game, Texture2D texture, SpriteFont font) 
            : base(game)
        {
            this.texture = texture;
            this.font = font;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Update()
        {
        }

        private string RenderTitle()
        {
            var renderedTitle = Title;
            if(IsActive)
            {
                renderedTitle = $"{MenuMarker}{Title}{MenuMarker}";
            }
            return renderedTitle;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                font,
                RenderTitle(),
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