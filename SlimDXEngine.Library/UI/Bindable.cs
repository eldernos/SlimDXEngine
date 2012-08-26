using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimDXEngine.Library.UI
{
    public interface IBindable
    {
        object GetValue();
    }

    public class Bindable<T> : IBindable where T : struct 
    {
        #region Fields
        T value;
        #endregion
        #region Properties
        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                if (!object.Equals(this.value, value))
                {
                    this.value = value;
                }
            }
        }
        #endregion
        #region Events
        #endregion
        #region Constructor
        public Bindable() : this(default(T))
        {

        }
        public Bindable(T value)
        {
            this.value = value;
        }
        #endregion
        #region Factories
        #endregion
        #region Methods
        object IBindable.GetValue()
        {
            return Value;
        }
        #endregion 
    }
}
