using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX.Direct3D11;

namespace SlimDXEngine.Library.Objects
{
    public class Layer
    {
        #region Fields
        protected int level;
        protected Camera cameraBelongsTo;
        protected List<PositionedObject> positionedObjects;
        #endregion
        #region Properties
        #endregion
        #region Events
        #endregion
        #region Constructor
        public Layer(Camera c)
        {
            level = 0;
            cameraBelongsTo = c;
            positionedObjects = new List<PositionedObject>();
        }
        #endregion
        #region Factories
        #endregion
        #region Methods
        internal void Draw(DeviceContext context)
        {
            foreach (PositionedObject po in positionedObjects.OrderByDescending(x => (cameraBelongsTo.Position - x.Position).LengthSquared()))
            {
                po.Draw(context);
            }
        }
        internal void AddPositionedObject(PositionedObject po)
        {
            positionedObjects.Add(po);
            po.Layer = this;
        }
        #endregion 
    
        
    }
}
