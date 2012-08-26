using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace SlimDXEngine.Library.Objects
{
    public class Camera : PositionedObject
    {
        #region Fields
        protected Vector3 target;
        protected Vector3 up = Vector3.UnitY;
        protected float fieldOfView;
        protected float aspectRatio;
        protected float nearPlane;
        protected float farPlane;

        protected Matrix viewMatrix;
        protected Matrix projectionMatrix;
        protected bool viewDirty = true;
        protected bool projectionDirty = true;

        protected List<Layer> layers;
        #endregion
        #region Properties
        public override Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                viewDirty = true;
                projectionDirty = true;
                position = value;
            }
        }
        public List<Layer> Layers
        {
            get
            {
                if (layers == null)
                {
                    layers = new List<Layer>();
                    layers.Add(new Layer(this));
                }
                return layers;
            }
        }
        public Vector3 Target
        {
            get { return target; }
            set
            {
                if (target == value) return;
                target = value;
                viewDirty = true;
            }
        }
        public Vector3 Up
        {
            get { return up; }
            set
            {
                if (up == value) return;
                up = value;
                viewDirty = true;
            }
        }
        public float FieldOfView
        {
            get { return fieldOfView; }
            set
            {
                if (fieldOfView == value) return;
                fieldOfView = value;
                projectionDirty = true;

            }
        }
        public float AspectRatio
        {
            get { return aspectRatio; }
            set
            {
                if (value == aspectRatio) return;
                aspectRatio = value;
                projectionDirty = true;
            }
        }
        public float NearPlane
        {
            get { return nearPlane; }
            set
            {
                if (nearPlane == value) return;
                nearPlane = value;
                projectionDirty = true;
            }
        }
        public float FarPlane
        {
            get { return farPlane; }
            set
            {
                if (farPlane == value) return;
                farPlane = value;
                projectionDirty = true;
            }
        }
        public Matrix ViewMatrix
        {
            get
            {
                if (viewDirty) RebuildViewMatrix();
                return viewMatrix;
            }
            protected set { viewMatrix = value; }
        }
        public Matrix ProjectionMatrix
        {
            get
            {
                if (projectionDirty) RebuildProjectionMatrix();
                return projectionMatrix;
            }
            protected set { projectionMatrix = value; }
        }
        #endregion
        #region Events
        #endregion
        #region Constructor
        public Camera()
        {
            
        }
        #endregion
        #region Factories
        #endregion
        #region Methods
        protected virtual void RebuildViewMatrix()
        {
            viewMatrix = Matrix.LookAtLH(Position, Target, Up);
            viewDirty = false;
        }
        protected virtual void RebuildProjectionMatrix()
        {
            projectionMatrix = Matrix.PerspectiveFovLH(FieldOfView, AspectRatio, NearPlane, FarPlane);
            projectionDirty = false;
        }
        #endregion 
        internal override void Draw(SlimDX.Direct3D11.DeviceContext context)
        {
            foreach (Layer l in Layers)
            {
                l.Draw(context);
            }
        }    

        internal void AddPositionedObject(PositionedObject po)
        {
            Layers.First().AddPositionedObject(po);
            po.Camera = this;
        }
    }
}
