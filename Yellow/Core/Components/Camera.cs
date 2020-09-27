﻿using SFML.Graphics;
using Yellow.Core.ECS;

namespace Yellow.Core.Components
{
    public class Camera : Component
    {
        private readonly View view;

        private float currentZoom = 1f;

        public float zoom = 1f;

        public string Name { get; }

        public Camera(View view, string name)
        {
            this.view = view;
            Name = name;
        }

        public float Zoom(float factor)
        {
            return zoom *= factor;
        }

        public void Update(RenderTarget target, TransformComponent transform)
        {
            view.Center = transform.Position;

            if (zoom != currentZoom)
            {
                view.Zoom(zoom / currentZoom);
                currentZoom = zoom;
            }

            target.SetView(view);
        }
    }
}