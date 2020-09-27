using System.Collections;
using System.Collections.Generic;
using Yellow.Core.Components;
using Yellow.Core.ECS;
using Yellow.Core.ScreenManagement;

namespace Yellow.Core.Systems
{
    public class CameraManager : ECS.System, IEnumerable<Camera>
    {
        private readonly Screen screen;

        [ComponentsRequest(typeof(Camera))]
        public List<Component> Cameras { private get; set; }

        public Camera Active { get; private set; }

        public CameraManager(Screen screen)
        {
            this.screen = screen;
        }

        public Camera Create(string name)
        {
            var entity = World.CreateEntity();
            var transform = new TransformComponent(World.root);
            var camera = new Camera(screen.Window.DefaultView, name);

            entity.Transform = transform;
            entity.Add(camera);

            return camera;
        }

        public override void Prepare()
        {
            Active = Create("Main");
        }

        public override void Update()
        {
            Active.Update(screen.Window, Active.owner.Transform);
        }

        public void SetActive(string name)
        {
            Active = this[name];
        }

        public Camera this[string name]
        {
            get => (Camera)Cameras.Find(camera => ((Camera)camera).Name == name);
        }

        public IEnumerator<Camera> GetEnumerator()
        {
            throw new System.Exception();
            //return Cameras.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Cameras.GetEnumerator();
        }
    }
}
