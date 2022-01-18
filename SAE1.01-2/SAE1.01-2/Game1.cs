using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;
using SAE1._01_2.Core;
using System;
using System.Collections.Generic;

namespace SAE1._01_2
{


    public enum Ecran { Accueil, Jeu, ChoixPerso };
    public enum TypeAnimation { walkWest, walkEast, idle };
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Camera _camera;
        private SpriteBatch _spriteBatch;
        private Vector2 _camPosition;
        private Vector2 _persoPosition;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        public bool hasJumped;
        public Vector2 velocity;
        public int _vitessePerso;
        public KeyboardState kState = Keyboard.GetState();
        private Song _song;
        private SoundEffect _jumpEffect;
        private AnimatedSprite _persoRouge;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = false;


        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferHeight = Constant.SCREEN_HEIGHT;
            _graphics.PreferredBackBufferWidth = Constant.SCREEN_WIDTH;
            _graphics.ApplyChanges();
            _camPosition = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            _persoPosition = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            //_persoPosition = new Vector2(Constant.POS_PERSO_X, Constant.POS_PERSO_Y);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _camera = new Camera();
            SpriteSheet perso_rouge = Content.Load<SpriteSheet>("perso/tiled-perso-rouge.sf", new JsonContentLoader());
            _persoRouge = new AnimatedSprite(perso_rouge);
            _tiledMap = Content.Load<TiledMap>("background/map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            this._song = Content.Load<Song>("Sound/Powerup");
            _jumpEffect = Content.Load<SoundEffect>("Sound/jump");
            MediaPlayer.Play(_song);
            // TODO: use this.Content to load your game content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            string animation = "idle";
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite
            //KeyboardState kState = Keyboard.GetState();
            //_persoPosition += velocity;

            //if (kState.IsKeyDown(Keys.A) || kState.IsKeyDown(Keys.Q))
            //{
            //    velocity.X -= 3f;
            //    animation = "walkWest";
            //}
            //else if (kState.IsKeyDown(Keys.D))
            //{
            //    velocity.X += 3f;
            //    animation = "walkWest";
            //}
            //else
            //    velocity.X = 0f;
            //if ((kState.IsKeyDown(Keys.W) || kState.IsKeyDown(Keys.Space)) && hasJumped == false)
            //{
            //    _persoPosition.Y -= 20f;
            //    velocity.Y = -30f;
            //    _jumpEffect.Play();
            //    hasJumped = true;
            //}

            //if (hasJumped == true)
            //{
            //    float i = 1;
            //    velocity.Y += 1f * i;
            //}

            //if (_persoPosition.Y + Constant.PERSO_HEIGHT >= Constant.POSITION_SOL)
            //    hasJumped = false;

            //if (hasJumped == false)
            //    velocity.Y = 0f;

            _camPosition.X -= 20f; //deplacement de la caméra
            _camera.Follow(_camPosition);
            _persoRouge.Play(animation);
            _persoRouge.Update(deltaSeconds);
            _tiledMapRenderer.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }
       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _tiledMapRenderer.Draw();
            _spriteBatch.End();
            _spriteBatch.Begin(transformMatrix:_camera.Transform);
            _spriteBatch.Draw(_persoRouge, _persoPosition);
            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
