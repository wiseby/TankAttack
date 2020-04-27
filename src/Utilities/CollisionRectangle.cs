using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankAttack 
{
    public class CollisionRectangle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Vector2 Center { get; set; }
        public float Rotation { get; set; }
        public Vector2 TopLeft { get; set; }
        public Vector2 TopRight { get; set; }
        public Vector2 BottomLeft { get; set; }
        public Vector2 BottomRight { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        // private Vector2[] TopPoints;
        // private Vector2[] BottomPoints;
        // private Vector2[] RightPoints;
        // private Vector2[] LeftPoints;

        /// <summary>
        /// Creates a CollisionRectangle from the points given and size.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public CollisionRectangle(Point point, int width, int height)
        {
            this.X = point.X;
            this.Y = point.Y;
            this.Width = width;
            this.Height = height;
            Initialize();
        }

        /// <summary>
        /// Creates a CollisionRectangle with the additional rotation
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="rotation"></param>
        public CollisionRectangle(Point point, int width, int height, float rotation)
            : this(point, width, height)
        {
            this.Rotation = rotation;
        }

        public CollisionRectangle(Vector2 point)
        {
            this.X = (int)point.X;
            this.Y = (int)point.Y;
            Initialize();   
        }

        /// <summary>
        /// Creates a CollisionRectangle with center on position at a specific rotation.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="rotation"></param>
        public CollisionRectangle(Vector2 point, float rotation)
            : this(point)
        {
            this.Rotation = rotation;
        }

        /// <summary>
        /// Returns a Texture2D of the Rectangle with a border.
        /// </summary>
        /// <returns>Texture2D representation of the CollisionRectangle</returns>
        /// <param name="color"></param>
        public Texture2D MakeVisible(Color color)
        {
            return null;
        }

        private void Initialize()
        {
            Center = new Vector2(X + 
            (Width / 2), Y + (Height / 2));

            // Set the rest of the positions of the rect according to rotation.
            // Using trigonometry (Sin(Rotation) * Width) for X and (Cos(Rotation) * Height
            // Add 90 degrees to the next 3 points.

            // Top
        }
    }
}