using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace CelShader.Entity
{
    class CelBase
    {
        protected Effect celShader;
        protected Effect borderShader;

        protected Texture2D celMap;

        protected bool isInitialized = false;

        public CelBase()
        {
        
        }

        public virtual void Initialize()
        {

        }

        public virtual void LoadContent(GraphicsDevice device, ContentManager content)
        {
            
        }

        public virtual void UnloadContent()
        {
        
        }

        public virtual void Update(GameTime gameTime)
        {
        
        }

        public virtual void Draw(GraphicsDevice device, Matrix world, Matrix view, Matrix projection, Vector3 cameraPos, Vector4 lightDirection)
        {
        
        }

    }
}
