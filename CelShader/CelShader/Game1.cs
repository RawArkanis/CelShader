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

        private Vector3 lightDir = new Vector3(1.0f, -1.0f, -1.0f);

        private Sphere sphere = new Sphere();

        private float angle = 0.0f;

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
            sphere.Initialize();
            sphere.LoadContent(GraphicsDevice, Content);
            SetupCamera();
        }

        protected override void UnloadContent()
        {
            sphere.UnloadContent();
        }

        private void SetupCamera()
        {
            Matrix rotation = Matrix.CreateRotationY(angle);

            cameraPos = Vector3.Transform(new Vector3(0.0f, 5.0f, 10.0f), rotation); // TODO Rodar essa bandida
            viewMatrix = Matrix.CreateLookAt(cameraPos, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000.0f);
        }

        protected override void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            angle += 0.5f * time;

            SetupCamera();

            sphere.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            worldMatrix = Matrix.Identity;

            sphere.Draw(GraphicsDevice, worldMatrix, viewMatrix, projectionMatrix, cameraPos, lightDir);

            base.Draw(gameTime);
        }
    }
}
