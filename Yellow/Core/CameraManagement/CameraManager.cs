using System.Collections;
using System.Collections.Generic;
using Yellow.Core.ScreenManagement;

namespace Yellow.Core.CameraManagement
{
    public class CameraManager : IEnumerable<Camera>
    {
        private readonly Screen screen;

        private readonly Dictionary<string, Camera> cameras = new Dictionary<string, Camera>();

        public CameraManager(Screen screen)
        {
            this.screen = screen;
        }

        public Camera Create(string name)
        {
            var camera = new Camera(screen, name);

            cameras.Add(name, camera);

            return camera;
        }

        public IEnumerator<Camera> GetEnumerator()
        {
            return cameras.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return cameras.Values.GetEnumerator();
        }

        public Camera this[string name]
        {
            get => cameras[name];
        }
    }
}
