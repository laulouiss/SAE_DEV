using Microsoft.Xna.Framework;

namespace SAE1._01_2
{
    class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Vector2 target)
        {
            var position = Matrix.CreateTranslation(
              -target.X,
              -target.Y,
              0);

            var offset = Matrix.CreateTranslation(
                Game1.ScreenWidth / 2,
                Game1.ScreenHeight / 2,
                0);

            Transform = position * offset;
        }
    }
}
