using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CelShader.Entity;

namespace CelShader
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        // Matrixes
        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private Matrix worldMatrix;

        // Camera Position and rotation angle
        private Vector3 cameraPos;
        private float angle = 0.0f;

        // Light Direction and rotation angles
        private Vector3 lightDir = new Vector3(0.0f, -1.0f, 0.0f);
        private float lightXAngle = 0.0f;
        private float lightYAngle = 0.0f;
        private float lightZAngle = 0.0f;

        // Objects
        private Sphere sun = new Sphere(Sphere.SphereType.Sun);
        private Sphere earth = new Sphere(Sphere.SphereType.Earth);
        private Sphere moon = new Sphere(Sphere.SphereType.Moon);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            // Load objects
            sun.LoadContent(GraphicsDevice, Content);
            earth.LoadContent(GraphicsDevice, Content);
            moon.LoadContent(GraphicsDevice, Content);

            // Setup initial camera
            SetupCamera();
        }

        protected override void UnloadContent()
        {
        }

        private void SetupCamera()
        {
            // Create camera rotation matrix
            Matrix rotation = Matrix.CreateRotationY(angle);

            // Calculate camera position, view and projection
            cameraPos = Vector3.Transform(new Vector3(0.0f, 6.0f, 12.0f), rotation);
            viewMatrix = Matrix.CreateLookAt(cameraPos, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000.0f);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyState = Keyboard.GetState();

            // Rotate by X axis
            if (keyState.IsKeyDown(Keys.Q))
                lightXAngle -= 0.05f;
            else if (keyState.IsKeyDown(Keys.A))
                lightXAngle += 0.05f;

            // Rotate by Y axis
            if (keyState.IsKeyDown(Keys.W))
                lightYAngle -= 0.05f;
            else if (keyState.IsKeyDown(Keys.S))
                lightYAngle += 0.05f;

            // Rotate by Z axis
            if (keyState.IsKeyDown(Keys.E))
                lightZAngle -= 0.05f;
            else if (keyState.IsKeyDown(Keys.D))
                lightZAngle += 0.05f;

            // Rotate Camera by Y axis
            if (keyState.IsKeyDown(Keys.Left))
                angle -= 0.05f;
            else if (keyState.IsKeyDown(Keys.Right))
                angle += 0.05f;

            // Resetup camera
            SetupCamera();

            // Update objects
            sun.Update(gameTime);
            earth.Update(gameTime);
            moon.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);

            // Recalculate light direction
            Vector3 newLightDir = Vector3.Transform(lightDir,
                Matrix.CreateRotationX(lightXAngle)*Matrix.CreateRotationY(lightYAngle)*
                Matrix.CreateRotationZ(lightZAngle));
            worldMatrix = Matrix.Identity;

            // Draw objects 
            worldMatrix = sun.Draw(GraphicsDevice, worldMatrix, viewMatrix, projectionMatrix, cameraPos, newLightDir);
            worldMatrix = earth.Draw(GraphicsDevice, worldMatrix, viewMatrix, projectionMatrix, cameraPos, newLightDir);
            moon.Draw(GraphicsDevice, worldMatrix, viewMatrix, projectionMatrix, cameraPos, newLightDir);

            base.Draw(gameTime);
        }
    }
}
