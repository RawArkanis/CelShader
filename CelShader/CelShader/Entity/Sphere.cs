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

            effect = content.Load<Effect>("fx\\cel_shader");
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

        public void Draw(GraphicsDevice device, Matrix world, Matrix view, Matrix projection, Vector3 cameraPos, Vector3 lightDirection)
        {
            /*if (isInitialized == false)
                return;*/

            foreach (ModelMesh mesh in model.Meshes)
            {
                effect.Parameters["World"].SetValue(world);
                effect.Parameters["InverseWorld"].SetValue(Matrix.Invert(world));
                effect.Parameters["View"].SetValue(view);
                effect.Parameters["Projection"].SetValue(projection);
                effect.Parameters["LightDirection"].SetValue(lightDirection);

                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    device.SetVertexBuffer(meshPart.VertexBuffer, meshPart.VertexOffset);
                    device.Indices = meshPart.IndexBuffer;

                    effect.CurrentTechnique = effect.Techniques["CelShader"];

                    foreach (EffectPass effectPass in effect.CurrentTechnique.Passes)
                    {
                        effectPass.Apply();

                        device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,
                            meshPart.NumVertices, meshPart.StartIndex, meshPart.PrimitiveCount);
                    }
                }
            }

        }

    }
}
