using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using CelShader.Entity;

namespace CelShader
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private Matrix worldMatrix;

        private Vector3 cameraPos;

        private Vector4 lightDir = new Vector4(0.0f, 0.0f, 1.0f, 1.0f);

        private Cube cube = new Cube(Vector3.Zero, 1);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            cube.Initialize();
            cube.LoadContent(GraphicsDevice, Content);
            SetupCamera();
        }

        protected override void UnloadContent()
        {
            cube.UnloadContent();
        }

        private void SetupCamera()
        {
            cameraPos = new Vector3(0.0f, 5.0f, 10.0f);
            viewMatrix = Matrix.CreateLookAt(cameraPos, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000.0f);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //SetupCamera();

            cube.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            worldMatrix = Matrix.Identity;

            cube.Draw(GraphicsDevice, worldMatrix, viewMatrix, projectionMatrix, cameraPos, lightDir);

            base.Draw(gameTime);
        }
    }
}