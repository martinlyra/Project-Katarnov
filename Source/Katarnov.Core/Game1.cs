using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Katarnov
{
    /// TODO: Implement networking (member)
    /// TODO: Event handling to pass to module subroutines


    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatchRenderer renderer;

        ContentManager contentManager;

        internal EntityDatabase entityDatabase;

        public Map currentMap = null;

        public View gameView = new View();

        public static bool HasLoaded = false;

        public Entity playerCharacter;

        public Game1()
        {
            Global.gameInstance = this;

            graphics = new GraphicsDeviceManager(this);
            entityDatabase = new EntityDatabase(this);

            Content.RootDirectory = "Content";

            Window.Title = "Project Katarnov";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            EntityManager.Initialize(this);

            Assets.Initialize();
            ModuleManager.Initialize();
            entityDatabase.Initialize();
            ByondImporter.Initialize();
 
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            contentManager = new ContentManager(Services);
            // Create a new SpriteBatch, which can be used to draw textures.

            renderer = new SpriteBatchRenderer(this);

            new Thread(Assets.ImportSprites).Start();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Time.deltaTime = gameTime.ElapsedGameTime.Milliseconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (HasLoaded)
            {
                if (currentMap == null)
                {
                    currentMap = ByondImporter.ImportMap(@"Content\Import\BYOND\Map\exodus-1.dmm");
                    var ent = EntityManager.entities[0];
                    var firstPos = ent.position;
                    gameView.position.X = firstPos.X;
                    gameView.position.Y = firstPos.Y;
                    //gameView.position.Z = firstPos.Z;
                    playerCharacter = ModuleManager.FirstModule.Interface.SpawnDefault();
                    playerCharacter.position.X = firstPos.X;
                    playerCharacter.position.Y = firstPos.Y;
                }

                Input.Update();

                UpdatePlayer();

                EntityManager.PopulateEvents();

                EntityManager.QueryForUpdate();

                EntityManager.ProcessUpdate();
            }

            base.Update(gameTime);
        }

        private void UpdatePlayer()
        {
            var keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Up))
            {
                playerCharacter.position.Y -= 1;
                gameView.position.Y -= 1;
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                playerCharacter.position.Y += 1;
                gameView.position.Y += 1;
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                playerCharacter.position.X -= 1;
                gameView.position.X -= 1;
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                playerCharacter.position.X += 1;
                gameView.position.X += 1;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (HasLoaded)
            {
                renderer.QueryForDrawing();

                renderer.ProcessDrawing();
            }

            base.Draw(gameTime);
        }
    }
}
