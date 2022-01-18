using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;

namespace SAE1._01_2.Core
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Vector2 target)
        {
            var position = Matrix.CreateTranslation(
              -target.X,
              -target.Y,
              0);

            var offset = Matrix.CreateTranslation(400, 240, 0);

            Transform = position * offset;
        }
    }

}
