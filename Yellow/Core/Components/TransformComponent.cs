using Yellow.Core.ECS;
using SFML.Graphics;
using Yellow.Core.Utils;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Yellow.Core.Components
{
    public class TransformComponent : BaseComponent, IEnumerable<TransformComponent>
    {
        private float rotation;

        private Vec2 position = new Vec2(0, 0);

        private Vec2 scale = new Vec2(1, 1);

        private Vec2 pivot = new Vec2(0, 0);

        private bool worldDirty = true;

        private TransformComponent parent;

        private Transform localTransform;

        private Transform worldTransform;

        public Transform LocalTransform => localTransform;

        public Transform WorldTransform => worldTransform;

        public TransformComponent Parent
        {
            get
            {
                return parent;
            }

            set
            {
                parent = value;
                worldDirty = true;
            }
        }

        private List<TransformComponent> children;

        public int ChildCount { get; private set; } = 0;

        public bool Dirty { get; private set; } = true;

        public bool WorldDirty
        {
            get
            {
                return worldDirty || Dirty || (parent != null && parent.WorldDirty);
            }
        }

        public TransformComponent() {}

        public TransformComponent(TransformComponent parent)
        {
            this.parent = parent;
        }

        public Vec2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
                Dirty = true;
            }
        }

        public Vec2 Scale
        {
            get
            {
                return scale;
            }

            set
            {
                scale = value;
                Dirty = true;
            }
        }

        public Vec2 Pivot
        {
            get
            {
                return pivot;
            }

            set
            {
                pivot = value;
                Dirty = true;
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value % Math2.PI2;

                if (rotation < 0.0f)
                {
                    rotation += Math2.PI2;
                }

                Dirty = true;
            }
        }

#pragma warning disable IDE1006

        public float x
        {
            get
            {
                return position.x;
            }

            set
            {
                position.SetX(value);
                Dirty = true;
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
                position.SetY(value);
                Dirty = true;
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
                scale.SetX(x);
                Dirty = true;
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
                scale.SetY(value);
                Dirty = true;
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
                pivot.SetX(value);
                Dirty = true;
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
                pivot.SetY(value);
                Dirty = true;
            }
        }

#pragma warning restore IDE1006

        public TransformComponent Translate(float x, float y)
        {
            position.Add(x, y);
            Dirty = true;

            return this;
        }

        public TransformComponent Translate(Vec2 movement)
        {
            position.Add(movement);
            Dirty = true;

            return this;
        }

        public TransformComponent Rotate(float angle)
        {
            Rotation = rotation + angle;
            // dirty is marked in Rotation's set

            return this;
        }

        public TransformComponent ScaleTransform(float x, float y)
        {
            scale.Multiply(x, y);
            Dirty = true;

            return this;
        }

        public TransformComponent ScaleTransform(float amount)
        {
            scale.Multiply(amount, amount);
            Dirty = true;

            return this;
        }

        public TransformComponent ScaleTransform(Vec2 scale)
        {
            this.scale.Multiply(scale);
            Dirty = true;

            return this;
        }

        public TransformComponent SetScale(float scale)
        {
            this.scale.Set(scale);
            Dirty = true;

            return this;
        }

        public TransformComponent SetPosition(float x, float y)
        {
            position.Set(x, y);
            Dirty = true;

            return this;
        }

        public TransformComponent SetPivot(float x, float y)
        {
            pivot.Set(x, y);
            Dirty = true;

            return this;
        }

        public void UpdateLocalTransform()
        {
            float sin = MathF.Sin(-rotation);
            float cos = MathF.Cos(rotation);

            float a = scale.x * cos;
            float b = scale.x * sin;
            float c = scale.y * sin;
            float d = scale.y * cos;

            float tx = -pivot.x * a - pivot.y * c + position.x;
            float ty = pivot.x * b - pivot.y * d + position.y;

            localTransform = new Transform(
                a, c, tx,
                -b, d, ty,
                0.0f, 0.0f, 1.0f
            );

            Dirty = false;
        }

        public void ForceUpdateTransform()
        {
            UpdateLocalTransform();

            if (parent != null)
            {
                worldTransform = parent.worldTransform;

                worldTransform.Combine(localTransform);
            }
            else
            {
                worldTransform = localTransform;
            }

            worldDirty = false;
        }
        
        public void RecursiveUpdateTransform()
        {
            if (Dirty)
            {
                UpdateLocalTransform();

                if (parent == null)
                {
                    worldTransform = Transform.Identity;
                }
                else
                {
                    if (parent.WorldDirty)
                    {
                        parent.RecursiveUpdateTransform();
                    }

                    worldTransform = parent.worldTransform;
                }

                worldTransform.Combine(localTransform);

                worldDirty = false;
            }
            else if (parent != null)
            {
                if (parent.WorldDirty)
                {
                    parent.RecursiveUpdateTransform();

                    worldTransform = parent.worldTransform;

                    worldTransform.Combine(localTransform);

                    worldDirty = false;
                }
            }
        }

        public void AddChild(TransformComponent child)
        {
            if (children == null)
            {
                children = new List<TransformComponent>();
            }

            child.Parent = this;

            children.Add(child);

            ++ChildCount;
        }

        public bool RemoveChild(TransformComponent child)
        {
            if (children == null)
            {
                return false;
            }

            if (children.Remove(child))
            {
                child.Parent = null;

                --ChildCount;

                return true;
            }

            return false;
        }

        public IEnumerator<TransformComponent> GetEnumerator()
        {
            if (ChildCount == 0)
            {
                return Enumerable.Empty<TransformComponent>().GetEnumerator();
            }

            return children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (ChildCount == 0)
            {
                return Enumerable.Empty<TransformComponent>().GetEnumerator();
            }

            return children.GetEnumerator();
        }

        public static implicit operator Transform(TransformComponent transform)
        {
            return transform.worldTransform;
        }
    }
}
