using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace SlimDXEngine.Library.Graphics
{

    public struct TexturedVertex
    {
        #region Fields
        #endregion
        #region Properties
        public Vector3 Position
        {
            get;
            set;
        }
        public Vector2 TextureCoordinate
        {
            get;
            set;
        }
        #endregion
        #region Events
        #endregion
        #region Constructor
        public TexturedVertex(Vector3 position, Vector2 textureCoordinate) : this()
        {
            Position = position;
            TextureCoordinate = textureCoordinate;
        }
        #endregion
        #region Factories
        #endregion
        #region Methods
        public static bool operator ==(TexturedVertex left, TexturedVertex right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(TexturedVertex left, TexturedVertex right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode() + TextureCoordinate.GetHashCode();
        }

   
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            return Equals((TexturedVertex)obj);
        }

        public bool Equals(TexturedVertex other)
        {
            return (Position == other.Position && TextureCoordinate == other.TextureCoordinate);
        }
        #endregion 

    }
}
