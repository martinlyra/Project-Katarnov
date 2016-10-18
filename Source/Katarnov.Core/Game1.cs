using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.IO;

namespace Katarnov
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatchRenderer renderer;

        ContentManager contentManager;

        internal EntityDatabase entityDatabase;

        public Map currentMap;
        public EntityManager entityManager;

        public View gameView = new View();

        public Game1()
        {
            Global.gameInstance = this;

            graphics = new GraphicsDeviceManager(this);
            entityManager = new EntityManager(this);
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
            ModuleManager.Initialize();
            entityDatabase.Initialize();
            ByondImporter.Initialize();
            // TODO: Add your initialization logic here
            currentMap = Map.FromByondMap(@"Content\Import\BYOND\Map\exodus-1.dmm");

            var ent = entityManager.entities[0];
            var firstPos = ent.position;
            gameView.position.X = firstPos.X;
            gameView.position.Y = firstPos.Y;
            //gameView.position.Z = firstPos.Z;

            Console.WriteLine("Sprite path for {1}: {0}", ent.spritePath, ent.GetType().Name);

            /*
            Sprite floorSprite = Sprite.LoadFile(GraphicsDevice, 
                entityDatabase.GetDefine("Floor").spriteDef);
            Sprite wallSprite = Sprite.LoadFile(GraphicsDevice, 
                entityDatabase.GetDefine("Wall").spriteDef);

            for (int x = 0; x < 12; x++)
            {
                for (int y = 0; y < 12; y++)
                {
                    if (x == 0 || x == 11 || y == 0 || y == 11)
                        new ActorObject(wallSprite, new Vector3(x, y, 0));
                    else
                        new ActorObject(floorSprite, new Vector3(x, y, 0));
                }
            }*/

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

            var keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Up))
                gameView.position.Y -= 1;
            else if (keyState.IsKeyDown(Keys.Down))
                gameView.position.Y += 1;
            if (keyState.IsKeyDown(Keys.Left))
                gameView.position.X -= 1;
            else if (keyState.IsKeyDown(Keys.Right))
                gameView.position.X += 1;

            //Console.WriteLine("{0}",entityManager.entities[0].position);

            entityManager.QueryForUpdate();

            entityManager.ProcessUpdate();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            renderer.QueryForDrawing();

            renderer.ProcessDrawing();

            base.Draw(gameTime);
        }
    }
}
