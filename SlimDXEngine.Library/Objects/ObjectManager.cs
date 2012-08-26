using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX.Direct3D11;
using SlimDXEngine.Library.Graphics;

namespace SlimDXEngine.Library.Objects
{
    public static class ObjectManager
    {
        #region Fields
        private static List<PositionedObject> positionedObjects;
        private static List<Camera> cameras;
        #endregion
        #region Properties
        private static List<PositionedObject> PositionedObjects
        {
            get
            {
                if (positionedObjects == null) positionedObjects = new List<PositionedObject>();
                return positionedObjects;
            }
        }
        public static List<Camera> Cameras
        {
            get
            {
                if (cameras == null)
                {
                    cameras = new List<Camera>();
                    cameras.Add(new Camera());
                    cameras.First().Position = new SlimDX.Vector3(0, 0, 40);
                    cameras.First().Target = new SlimDX.Vector3(0, 0, 0);
                    cameras.First().NearPlane = 1;
                    cameras.First().FarPlane = 1000;
                    cameras.First().FieldOfView = .8f;
                    cameras.First().AspectRatio = 4f / 3f;
                }
                return cameras;
            }
        }
        public static Camera Camera
        {
            get
            {
                return Cameras.First();
            }
        }
        #endregion
        #region Events
        #endregion
        #region Constructor
        #endregion
        #region Factories
        #endregion
        #region Methods
        public static void Update(float elapsedTime)
        {
            foreach (PositionedObject po in positionedObjects)
            {
                po.Update(elapsedTime);
            }
            foreach (Camera c in Cameras)
            {
                c.Update(elapsedTime);
            }
        }

        public static void Draw(DeviceContext device)
        {
            foreach (Camera c in Cameras)
            {
                c.Draw(device);
            }
        }

        public static void AddPositionedObject(PositionedObject po)
        {
            PositionedObjects.Add(po);
            ObjectManager.Camera.AddPositionedObject(po);
        }
        #endregion


    }
}
