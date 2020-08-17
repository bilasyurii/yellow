using Yellow.Core.ECS;
using SFML.Graphics;
using Yellow.Core.Utils;
using System;

namespace Yellow.Core.Components
{
    public class TransformComponent : IComponent
    {
        private Transform cachedTransform;

        private bool updateNeeded = true;

        private float mRotation;

#pragma warning disable IDE1006

        public Vec2 position { get; private set; } = new Vec2(0, 0);

        public Vec2 scale { get; private set; } = new Vec2(1, 1);

        public Vec2 pivot { get; private set; } = new Vec2(0, 0);

        public float Rotation
        {
            get
            {
                return mRotation;
            }

            set
            {
                mRotation = value % Math2.PI2;

                if (mRotation < 0.0f)
                {
                    mRotation += Math2.PI2;
                }

                updateNeeded = true;
            }
        }

        public float x
        {
            get
            {
                return position.x;
            }

            set
            {
                position.x = value;
                updateNeeded = true;
            }
        }

        public float y
        {
            get
            {
                return position.y;
            }

            set
            {
                position.y = value;
                updateNeeded = true;
            }
        }

        public float scaleX
        {
            get
            {
                return scale.x;
            }

            set
            {
                scale.x = value;
                updateNeeded = true;
            }
        }

        public float scaleY
        {
            get
            {
                return scale.y;
            }

            set
            {
                scale.y = value;
                updateNeeded = true;
            }
        }

        public float pivotX
        {
            get
            {
                return pivot.x;
            }

            set
            {
                pivot.x = value;
                updateNeeded = true;
            }
        }

        public float pivotY
        {
            get
            {
                return pivot.y;
            }

            set
            {
                pivot.y = value;
                updateNeeded = true;
            }
        }

        #pragma warning restore IDE1006

        public TransformComponent Translate(float x, float y)
        {
            position.x += x;
            position.y += y;
            updateNeeded = true;

            return this;
        }

        public TransformComponent Translate(Vec2 movement)
        {
            position.x += movement.x;
            position.y += movement.y;
            updateNeeded = true;

            return this;
        }

        public TransformComponent Rotate(float rotation)
        {
            Rotation = mRotation + rotation;

            return this;
        }

        public TransformComponent Scale(float x, float y)
        {
            scale.x *= x;
            scale.y *= y;
            updateNeeded = true;

            return this;
        }

        public TransformComponent Scale(Vec2 scale)
        {
            this.scale.x *= scale.x;
            this.scale.y *= scale.y;
            updateNeeded = true;

            return this;
        }

        public TransformComponent() {}

        public TransformComponent(TransformComponent other)
        {
            // TODO copy scale, position, rotation, origin
            // and if other doens't need update - copy matrix, too
        }
        
        private void UpdateTransform()
        {
            float sin = MathF.Sin(-mRotation);
            float cos = MathF.Cos(mRotation);

            float a = scale.x * cos;
            float b = scale.x * sin;
            float c = scale.y * sin;
            float d = scale.y * cos;

            float tx = -pivot.x * a - pivot.y * c + position.x;
            float ty = pivot.x * b - pivot.y * d + position.y;

            cachedTransform = new Transform(
                a, c, tx,
                -b, d, ty,
                0.0f, 0.0f, 1.0f
            );

            updateNeeded = false;
        }

        public static explicit operator Transform(TransformComponent component)
        {
            if (component.updateNeeded)
            {
                component.UpdateTransform();
            }

            return component.cachedTransform;
        }
    }
}
