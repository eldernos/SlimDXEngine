using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimDXEngine.Library.Utility
{
    using System.Diagnostics;
    using SlimDX;
    public class Clock
    {
        #region Fields
        private bool isRunning;
        private readonly long frequency;
        private long count;
        #endregion
        #region Properties
        #endregion
        #region Events
        #endregion
        #region Constructor
        public Clock()
        {
            frequency = Stopwatch.Frequency;
        }
        #endregion
        #region Factories
        #endregion
        #region Methods
        public void Start()
        {
            count = Stopwatch.GetTimestamp();
            isRunning = true;
        }
        public float Update()
        {
            float result = 0.0f;
            if (isRunning)
            {
                long last = count;
                count = Stopwatch.GetTimestamp();
                result = (float)(count - last) / frequency;
            }
            return result;
        }
        #endregion 
    }
}
