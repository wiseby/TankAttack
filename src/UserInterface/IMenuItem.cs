using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UserInterface
{
    public interface IMenuItem
    {
         public string Title { get; set; }
         public void Activate();
         public void Deactivate();
         public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
         public string InputText { get; set; }
    }
}