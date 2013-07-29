using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace CelShader.Entity
{
    class Cube : CelBase
    {

        private const int NUM_TRIANGLES = 12;
        private const int NUM_VERTICES = 36;

        private VertexPositionNormalTexture[] vertices;
        private VertexBuffer buffer;
        private Texture2D texture;

        private float angle = 0.0f;

        public Vector3 Position { get; set; }
        public float Size { get; set; }

        public Cube(Vector3 Position, int Size) : base()
        {
            this.Position = Position;
            this.Size = Size;
        }

        public override void Initialize()
        {
            BuildCube();

            isInitialized = true;
        }

        protected void BuildCube()
        {
            vertices = new VertexPositionNormalTexture[NUM_VERTICES];

            Vector3 topLeftFront = Position +
                new Vector3(-1.0f, 1.0f, -1.0f) * Size;
            Vector3 bottomLeftFront = Position +
                new Vector3(-1.0f, -1.0f, -1.0f) * Size;
            Vector3 topRightFront = Position +
                new Vector3(1.0f, 1.0f, -1.0f) * Size;
            Vector3 bottomRightFront = Position +
                new Vector3(1.0f, -1.0f, -1.0f) * Size;
            Vector3 topLeftBack = Position +
                new Vector3(-1.0f, 1.0f, 1.0f) * Size;
            Vector3 topRightBack = Position +
                new Vector3(1.0f, 1.0f, 1.0f) * Size;
            Vector3 bottomLeftBack = Position +
                new Vector3(-1.0f, -1.0f, 1.0f) * Size;
            Vector3 bottomRightBack = Position +
                new Vector3(1.0f, -1.0f, 1.0f) * Size;

            Vector3 frontNormal = new Vector3(0.0f, 0.0f, 1.0f) * Size;
            Vector3 backNormal = new Vector3(0.0f, 0.0f, -1.0f) * Size;
            Vector3 topNormal = new Vector3(0.0f, 1.0f, 0.0f) * Size;
            Vector3 bottomNormal = new Vector3(0.0f, -1.0f, 0.0f) * Size;
            Vector3 leftNormal = new Vector3(-1.0f, 0.0f, 0.0f) * Size;
            Vector3 rightNormal = new Vector3(1.0f, 0.0f, 0.0f) * Size;

            Vector2 textureTopLeft = new Vector2(1.0f * Size, 0.0f * Size);
            Vector2 textureTopRight = new Vector2(0.0f * Size, 0.0f * Size);
            Vector2 textureBottomLeft = new Vector2(1.0f * Size, 1.0f * Size);
            Vector2 textureBottomRight = new Vector2(0.0f * Size, 1.0f * Size);

            // Frente
            vertices[0] = new VertexPositionNormalTexture(
                topLeftFront, frontNormal, textureTopLeft);
            vertices[1] = new VertexPositionNormalTexture(
                bottomLeftFront, frontNormal, textureBottomLeft);
            vertices[2] = new VertexPositionNormalTexture(
                topRightFront, frontNormal, textureTopRight);
            vertices[3] = new VertexPositionNormalTexture(
                bottomLeftFront, frontNormal, textureBottomLeft);
            vertices[4] = new VertexPositionNormalTexture(
                bottomRightFront, frontNormal, textureBottomRight);
            vertices[5] = new VertexPositionNormalTexture(
                topRightFront, frontNormal, textureTopRight);

            // Trás
            vertices[6] = new VertexPositionNormalTexture(
                topLeftBack, backNormal, textureTopRight);
            vertices[7] = new VertexPositionNormalTexture(
                topRightBack, backNormal, textureTopLeft);
            vertices[8] = new VertexPositionNormalTexture(
                bottomLeftBack, backNormal, textureBottomRight);
            vertices[9] = new VertexPositionNormalTexture(
                bottomLeftBack, backNormal, textureBottomRight);
            vertices[10] = new VertexPositionNormalTexture(
                topRightBack, backNormal, textureTopLeft);
            vertices[11] = new VertexPositionNormalTexture(
                bottomRightBack, backNormal, textureBottomLeft);

            // Topo
            vertices[12] = new VertexPositionNormalTexture(
                topLeftFront, topNormal, textureBottomLeft);
            vertices[13] = new VertexPositionNormalTexture(
                topRightBack, topNormal, textureTopRight);
            vertices[14] = new VertexPositionNormalTexture(
                topLeftBack, topNormal, textureTopLeft);
            vertices[15] = new VertexPositionNormalTexture(
                topLeftFront, topNormal, textureBottomLeft);
            vertices[16] = new VertexPositionNormalTexture(
                topRightFront, topNormal, textureBottomRight);
            vertices[17] = new VertexPositionNormalTexture(
                topRightBack, topNormal, textureTopRight);

            // Baixo
            vertices[18] = new VertexPositionNormalTexture(
                bottomLeftFront, bottomNormal, textureTopLeft);
            vertices[19] = new VertexPositionNormalTexture(
                bottomLeftBack, bottomNormal, textureBottomLeft);
            vertices[20] = new VertexPositionNormalTexture(
                bottomRightBack, bottomNormal, textureBottomRight);
            vertices[21] = new VertexPositionNormalTexture(
                bottomLeftFront, bottomNormal, textureTopLeft);
            vertices[22] = new VertexPositionNormalTexture(
                bottomRightBack, bottomNormal, textureBottomRight);
            vertices[23] = new VertexPositionNormalTexture(
                bottomRightFront, bottomNormal, textureTopRight);

            // Esquerda
            vertices[24] = new VertexPositionNormalTexture(
                topLeftFront, leftNormal, textureTopRight);
            vertices[25] = new VertexPositionNormalTexture(
                bottomLeftBack, leftNormal, textureBottomLeft);
            vertices[26] = new VertexPositionNormalTexture(
                bottomLeftFront, leftNormal, textureBottomRight);
            vertices[27] = new VertexPositionNormalTexture(
                topLeftBack, leftNormal, textureTopLeft);
            vertices[28] = new VertexPositionNormalTexture(
                bottomLeftBack, leftNormal, textureBottomLeft);
            vertices[29] = new VertexPositionNormalTexture(
                topLeftFront, leftNormal, textureTopRight);

            // Direita
            vertices[30] = new VertexPositionNormalTexture(
                topRightFront, rightNormal, textureTopLeft);
            vertices[31] = new VertexPositionNormalTexture(
                bottomRightFront, rightNormal, textureBottomLeft);
            vertices[32] = new VertexPositionNormalTexture(
                bottomRightBack, rightNormal, textureBottomRight);
            vertices[33] = new VertexPositionNormalTexture(
                topRightBack, rightNormal, textureTopRight);
            vertices[34] = new VertexPositionNormalTexture(
                topRightFront, rightNormal, textureTopLeft);
            vertices[35] = new VertexPositionNormalTexture(
                bottomRightBack, rightNormal, textureBottomRight);
        }

        public override void LoadContent(GraphicsDevice device, ContentManager content)
        {
            if (isInitialized == false)
                BuildCube();

            buffer = new VertexBuffer(device,
                VertexPositionNormalTexture.VertexDeclaration,
                vertices.Length, BufferUsage.WriteOnly);

            buffer.SetData(vertices);

            texture = content.Load<Texture2D>("texture\\crate");

            celShader = content.Load<Effect>("fx\\CelShader");
            celMap = content.Load<Texture2D>("texture\\cel_map01");
        }

        public override void UnloadContent()
        {
            if (isInitialized == true)
                vertices = null;

            if (buffer != null)
            {
                buffer.Dispose();
                buffer = null;
            }

            if (texture != null)
            {
                texture.Dispose();
                texture = null;
            }
        }

        public override void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            angle += 0.5f * time;
        }

        public override void Draw(GraphicsDevice device, Matrix world, Matrix view, Matrix projection, Vector3 cameraPos, Vector4 lightDirection)
        {
            if (isInitialized == false)
                return;

            Matrix rotation = Matrix.CreateRotationY(angle);

            celShader.Parameters["Projection"].SetValue(projection);
            celShader.Parameters["View"].SetValue(view);
            celShader.Parameters["LightDirection"].SetValue(lightDirection);
            celShader.Parameters["ColorMap"].SetValue(texture);
            celShader.Parameters["CelMap"].SetValue(celMap);

            celShader.Parameters["World"].SetValue(world * rotation);
            celShader.Parameters["InverseWorld"].SetValue(Matrix.Invert(world * rotation));

            device.SetVertexBuffer(buffer);

            celShader.CurrentTechnique = celShader.Techniques["ToonShader"];

            foreach (EffectPass pass in celShader.CurrentTechnique.Passes)
            {
                pass.Apply();

                device.DrawPrimitives(PrimitiveType.TriangleList, 0, NUM_TRIANGLES);
            }
        }

    }
}
