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

        public enum SphereType {
            Sun,
            Earth,
            Moon
        }

        private Effect effect;
        private Model model;
        private Texture2D texture;
        private Texture2D celTexture;
        private Texture2D edgeTexture;

        private float angle = 0.0f;
        private float angle2 = 0.0f;
        private float rotation;
        private float rotation2;
        private float scale;
        private Vector3 translation;

        private SphereType type;

        public Sphere(SphereType type)
        {
            this.type = type;

            switch (type)
            {
                case SphereType.Sun:
                    rotation = 0.0f;
                    rotation2 = 0.5f;
                    translation = new Vector3(0.0f, 0.0f, 0.0f);
                    scale = 2.5f;
                    break;
                case SphereType.Earth:
                    rotation = 0.5f;
                    rotation2 = 5.0f;
                    translation = new Vector3(5.0f, 0.0f, 0.0f);
                    scale = 0.75f;
                    break;
                case SphereType.Moon:
                    rotation = 0.75f;
                    rotation2 = 0.0f;
                    translation = new Vector3(-1.5f, 0.0f, 0.0f);
                    scale = 0.25f;
                    break;
            }
        }

        public void LoadContent(GraphicsDevice device, ContentManager content)
        {

            model = content.Load<Model>("model\\sphere");

            String name = "";
            switch (type)
            {
                case SphereType.Sun:
                    name = "sun";
                    break;
                case SphereType.Earth:
                    name = "earth";
                    break;
                case SphereType.Moon:
                    name = "moon";
                    break;
            }

            texture = content.Load<Texture2D>("texture\\" + name);

            celTexture = content.Load<Texture2D>("texture\\cel_map");
            edgeTexture = content.Load<Texture2D>("texture\\edge_map");

            effect = content.Load<Effect>("fx\\cel_shader");
        }

        public void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            angle += rotation * time;
            angle2 += rotation2 * time;
        }

        public Matrix Draw(GraphicsDevice device, Matrix world, Matrix view, Matrix projection, Vector3 cameraPos, Vector3 lightDirection)
        {

            Matrix localWorld = Matrix.CreateTranslation(translation) * world;

            foreach (ModelMesh mesh in model.Meshes)
            {
                effect.Parameters["World"].SetValue(Matrix.CreateScale(scale) * Matrix.CreateRotationY(angle) * localWorld);
                effect.Parameters["View"].SetValue(view);
                effect.Parameters["Projection"].SetValue(projection);

                effect.Parameters["CameraPosition"].SetValue(cameraPos);

                effect.Parameters["LightDirection"].SetValue(lightDirection);
                effect.Parameters["LightColor"].SetValue(new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
                effect.Parameters["LightIntensity"].SetValue(1.0f);
                effect.Parameters["AmbientColor"].SetValue(new Vector4(0.75f, 0.75f, 0.75f, 1.0f));
                effect.Parameters["AmbientIntensity"].SetValue(0.05f);

                effect.Parameters["Scale"].SetValue(scale);

                effect.Parameters["ColorMap"].SetValue(texture);
                effect.Parameters["CelMap"].SetValue(celTexture);
                effect.Parameters["EdgeMap"].SetValue(edgeTexture);

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

            switch (type)
            {
                case SphereType.Earth:
                    localWorld = Matrix.CreateRotationY(angle2) * localWorld;
                    break;
                default:
                    localWorld = Matrix.CreateRotationY(angle2) * localWorld;
                    break;
            }

            return localWorld;

        }

    }
}
