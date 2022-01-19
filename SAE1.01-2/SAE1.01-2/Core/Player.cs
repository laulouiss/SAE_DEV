using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace SAE1._01_2.Core
{
    public class Player : ChoixPerso
    {
        private SpriteBatch _spriteBatch;
        private AnimatedSprite _player;
        private Vector2 _playerPosition;
        private bool hasJumped;
        private Vector2 velocity;


        public void Initialize(SpriteSheet spriteSheet)
        {
            _playerPosition = new Vector2(Constant.SCREEN_WIDTH / 2, Constant.SCREEN_HEIGHT / 2);
            CouleurPerso = ChoixCouleur(CouleurPlayer);
        }

        public void LoadContent(SpriteSheet spriteSheet)
        {
            _player = new AnimatedSprite(spriteSheet);
        }

        public void Update(GameTime gameTime)
        {
            string animation = $"{CouleurPerso}_{ChoixDirection(direction)}_idle";
            KeyboardState kState = Keyboard.GetState();

            _playerPosition += velocity;

            if (kState.IsKeyDown(Keys.A) || kState.IsKeyDown(Keys.Q))
            {
                velocity.X -= 3f;
                animation = $"{CouleurPerso}_{ChoixDirection(direction)}_walk";
                direction = 0;
            }
            else if (kState.IsKeyDown(Keys.D))
            {
                velocity.X += 3f;
                animation = $"{CouleurPerso}_{ChoixDirection(direction)}_walk";
                direction = 1;
            }
            else
                velocity.X = 0f;

            if ((kState.IsKeyDown(Keys.W) || kState.IsKeyDown(Keys.Z) || kState.IsKeyDown(Keys.Space)) && hasJumped == false)
            {
                _playerPosition.Y -= 20f;
                velocity.Y = -30f;
                hasJumped = true;
            }
            if (hasJumped == true)
            {
                float i = 1;
                velocity.Y += 1f * i;
            }

            if (_playerPosition.Y + 170 >= 980)
                hasJumped = false;

            if (hasJumped == false)
                velocity.Y = 0f;
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_player, _playerPosition);
            _spriteBatch.End();
        }
    }
}
