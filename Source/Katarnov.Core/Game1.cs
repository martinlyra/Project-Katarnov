using Katarnov.Module;
using Katarnov.Network;
using Katarnov.Events;
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
        private static readonly Version version = Assembly.GetEntryAssembly().GetName().Version;

        GraphicsDeviceManager graphics;
        SpriteBatchRenderer renderer;

        ContentManager contentManager;

        NetworkMember network;

        ProgramArgs args;

        internal EntityDatabase entityDatabase;

        public Map currentMap = null;

        public View gameView = new View();

        public static bool HasLoaded = false;

        public Entity playerCharacter;

        public static Version Version
        {
            get
            {
                return version;
            }
        }

        public Game1(ProgramArgs args = new ProgramArgs())
        {
            Global.gameInstance = this;
            this.args = args;

            if (this.args.ServerMode)
                Console.WriteLine("ServerMode argument found");

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
            if (!args.Headless)
            {
                Assets.Initialize();
            }
            if (args.ServerMode)
            {
                EntityManager.Initialize(this);
                ModuleManager.Initialize();
                entityDatabase.Initialize();
                ByondImporter.Initialize();
            }

            NetworkInitalize();

            base.Initialize();
        }

        private void NetworkInitalize()
        {
            if (args.ServerMode)
                network = new NetworkServer();
            else
                network = new NetworkClient();

            network.Connect();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            if (!args.Headless)
            {
                contentManager = new ContentManager(Services);
                // Create a new SpriteBatch, which can be used to draw textures.

                renderer = new SpriteBatchRenderer(this);

                new Thread(Assets.ImportSprites).Start();

                // TODO: use this.Content to load your game content here
            }
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

            if (args.ServerMode)
                ServerUpdate();
            else
                ClientUpdate();

            base.Update(gameTime);
        }

        private void ClientUpdate()
        {
            network.SendMessage("Hi");
            Input.Update();

            //UpdatePlayer();
        }

        private void ServerUpdate()
        {
            if (HasLoaded)
            {
                if (currentMap == null)
                {
                    currentMap = ByondImporter.ImportMap(@"Content\Import\BYOND\Map\exodus-1.dmm");

                    World.Ready();
                    /*var ent = EntityManager.entities[0];
                    var firstPos = ent.position;
                    gameView.position.X = firstPos.Xf;
                    gameView.position.Y = firstPos.Yf;
                    //gameView.position.Z = firstPos.Z;
                    */
                }
                network.Update();

                EntityManager.PopulateEvents();

                EntityManager.QueueUpdates();
                RoutineManager.QueueUpdates();

                Scheduler.ProcessUpdates();
            }
        }

        private void UpdatePlayer()
        {
            var keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Up))
            {
                playerCharacter.position.Y -= 1;
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                playerCharacter.position.Y += 1;
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                playerCharacter.position.X -= 1;
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                playerCharacter.position.X += 1;
            }
            gameView.position = (Vector3)playerCharacter.position;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (!args.Headless)
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
}
