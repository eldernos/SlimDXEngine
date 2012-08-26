using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace SlimDXEngine.Library.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ColoredVertex
    {
        #region Fields
        
        #endregion
        #region Properties
        public Vector3 Position
        {
            get;
            set;
        }
        public int Color
        {
            get;
            set;
        }
        #endregion
        #region Events
        #endregion
        #region Constructor
        public ColoredVertex(Vector3 position, int color) : this()
        {
            Position = position;
            Color = color;
        }
        #endregion
        #region Factories
        #endregion
        #region Methods
        public static bool operator ==(ColoredVertex left, ColoredVertex right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(ColoredVertex left, ColoredVertex right)
        {
            return !(left == right);
        }
        public override int GetHashCode()
        {
            return Position.GetHashCode() + Color.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            return Equals((ColoredVertex)obj);
        }
        public bool Equals(ColoredVertex other)
        {
            return (Position == other.Position && Color == other.Color);
        }
        #endregion 

    }
}
