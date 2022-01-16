using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System;

namespace SAE1._01_2
{
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera _camera;
        private AnimatedSprite _perso;
        private Vector2 _persoPosition;
        private Vector2 _camPosition;
        private Vector2 velocity;
        private Texture2D _bg;
        private int _vitessePerso;
        private bool hasJumped;
        public static int ScreenHeight;
        public static int ScreenWidth;

        public int playerColor = 2;
        public int direction = 0;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = false;


        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            _camPosition = new Vector2(30900, 520);
            _persoPosition = new Vector2(31700, 810);
            _vitessePerso = 220;
            hasJumped = true;
            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _camera = new Camera();
            //_mvmCam = new CamMovement(Content.Load<Texture2D>("cam"));
            _bg = Content.Load<Texture2D>("background/bg2");
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>(".\\tiled-perso-allside.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            String animation = $"{ChoixCouleur(playerColor)}_{DirectionPlayer(direction)}_idle";
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite
            KeyboardState keyboardState = Keyboard.GetState();

            _persoPosition += velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Q)) {
                velocity.X -= 3f;
                animation = $"{ChoixCouleur(playerColor)}_{DirectionPlayer(direction)}_walk";
                direction = 0;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                velocity.X += 3f;
                animation = $"{ChoixCouleur(playerColor)}_{DirectionPlayer(direction)}_walk";
                direction = 1;
            }

            else
                velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false) {
                _persoPosition.Y -= 20f;
                velocity.Y = -11f;
                hasJumped = true;
            }
            if (hasJumped == true) {
                float i = 1;
                velocity.Y += 0.15f * i;
            }

            if (_persoPosition.Y + 170 >= 980)
                hasJumped = false;

            if (hasJumped == false)
                velocity.Y = 0f;
            _camPosition.X -= 10f; //deplacement de la caméra
            _camera.Follow(_camPosition);
            _perso.Play(animation);
            _perso.Update(deltaSeconds);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: _camera.Transform);
            _spriteBatch.Draw(_bg, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(_perso, _persoPosition);
            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public static String ChoixCouleur(int choix) {
            String couleur = "";
            if(choix == 0) {
                couleur = "red";
            }
            else if(choix == 1) {
                couleur = "blue";
            }
            else if (choix == 2) {
                couleur = "green";
            }
            else if (choix == 3) {
                couleur = "yellow";
            }
            return couleur;
        }

        public static String DirectionPlayer(int direction) {
            String view = "";
            if (direction == 1) {
                view = "Left";
            }
            else
                view = "Right";
            return view;
        }

    }
}
