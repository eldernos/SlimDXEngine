using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SlimDXEngine.Library.Graphics;

namespace SlimDXEngine.Library.Objects
{
    public class PositionedObject
    {
        #region Fields
        protected Vector3 position;
        protected Vector3 velocity;
        protected Vector3 acceleration;
        protected Vector3 scale;
        protected Vector3 rotation;
        protected Vector3 rotationVelocity;
        protected Vector3 rotationAcceleration;

        public Camera Camera { get; set; }
        public Layer Layer { get; set; }
        #endregion
        #region Properties
        public virtual Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        public Vector3 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }
        public Vector3 Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        public Vector3 Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public Vector3 RotationVelocity
        {
            get { return rotationVelocity; }
            set { rotationVelocity = value; }
        }
        public Vector3 RotationAcceleration
        {
            get { return rotationAcceleration; }
            set { rotationAcceleration = value; }
        }
        #endregion
        #region Events
        #endregion
        #region Constructor
        public PositionedObject()
        {
            Rotation = new Vector3(0, 0, 0);
            Scale = new Vector3(1, 1, 1);
        }
        public PositionedObject(Vector3 position) : this()
        {
            this.position = position;
        }
        #endregion
        #region Factories
        #endregion
        #region Methods
        internal virtual void Draw(DeviceContext context)
        {

        }
        public virtual void Update(float elapsedTime)
        {
            Velocity += Acceleration * elapsedTime;
            Position += Velocity * elapsedTime;

            RotationVelocity += RotationAcceleration * elapsedTime;
            Rotation += RotationVelocity * elapsedTime;
        }
        #endregion 

    }
}
