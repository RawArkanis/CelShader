using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace CelShader.Entity
{
    class Sphere
    {

        private Effect effect;
        private Model model;

        private float angle = 0.0f;

        public Sphere()
        {
        }

        public void Initialize()
        {
            //isInitialized = true;
        }

        public void LoadContent(GraphicsDevice device, ContentManager content)
        {
            /*if (isInitialized == false)
                BuildCube();*/

            model = content.Load<Model>("model\\sphere");

            effect = new BasicEffect(device);
        }

        public void UnloadContent()
        {
            /*if (isInitialized == true)
                vertices = null;*/
        }

        public void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            angle += 0.5f * time;
        }

        public void Draw(GraphicsDevice device, Matrix world, Matrix view, Matrix projection, Vector3 cameraPos, Vector4 lightDirection)
        {
            /*if (isInitialized == false)
                return;*/

            foreach (ModelMesh mesh in model.Meshes)
            {
                mesh.Draw();
            }

        }

    }
}
