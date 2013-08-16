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

        private Vector3 lightDir;
        private float lightDirX = 0.0f;
        private float lightDirY = -1.0f;
        private float lightDirZ = 0.0f;

        private Sphere sun = new Sphere(Sphere.SphereType.Sun);
        private Sphere earth = new Sphere(Sphere.SphereType.Earth);
        private Sphere moon = new Sphere(Sphere.SphereType.Moon);

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
            sun.LoadContent(GraphicsDevice, Content);
            earth.LoadContent(GraphicsDevice, Content);
            moon.LoadContent(GraphicsDevice, Content);
            SetupCamera();
        }

        protected override void UnloadContent()
        {
        }

        private void SetupCamera()
        {
            Matrix rotation = Matrix.CreateRotationY(angle);

            cameraPos = Vector3.Transform(new Vector3(0.0f, 6.0f, 12.0f), rotation);
            viewMatrix = Matrix.CreateLookAt(cameraPos, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000.0f);
        }

        protected override void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Q))
            {
                lightDirX += 0.01f;
                if (lightDirX > 1.0f)
                    lightDirX = 1.0f;
            }
            else if (keyState.IsKeyDown(Keys.A))
            {
                lightDirX -= 0.01f;
                if (lightDirX < -1.0f)
                    lightDirX = -1.0f;
            }

            if (keyState.IsKeyDown(Keys.W))
            {
                lightDirY += 0.01f;
                if (lightDirY > 1.0f)
                    lightDirY = 1.0f;
            }
            else if (keyState.IsKeyDown(Keys.S))
            {
                lightDirY -= 0.01f;
                if (lightDirY < -1.0f)
                    lightDirY = -1.0f;
            }

            if (keyState.IsKeyDown(Keys.E))
            {
                lightDirZ += 0.01f;
                if (lightDirZ > 1.0f)
                    lightDirZ = 1.0f;
            }
            else if (keyState.IsKeyDown(Keys.D))
            {
                lightDirZ -= 0.01f;
                if (lightDirZ < -1.0f)
                    lightDirZ = -1.0f;
            }

            if (keyState.IsKeyDown(Keys.Left))
                angle -= 0.1f;
            else if (keyState.IsKeyDown(Keys.Right))
                angle += 0.1f;


            SetupCamera();

            sun.Update(gameTime);
            earth.Update(gameTime);
            moon.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);

            lightDir = new Vector3(lightDirX, lightDirY, lightDirZ);
            worldMatrix = Matrix.Identity;

            worldMatrix = sun.Draw(GraphicsDevice, worldMatrix, viewMatrix, projectionMatrix, cameraPos, lightDir);
            worldMatrix = earth.Draw(GraphicsDevice, worldMatrix, viewMatrix, projectionMatrix, cameraPos, lightDir);
            moon.Draw(GraphicsDevice, worldMatrix, viewMatrix, projectionMatrix, cameraPos, lightDir);

            base.Draw(gameTime);
        }
    }
}
