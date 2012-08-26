using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlimDX.Multimedia;
using SlimDX.RawInput;
using SlimDXEngine.Library.Graphics;

namespace SlimDXEngine.Library.Input.Sources
{
    public class Keyboard
    {
        #region Fields

        private Dictionary<int, bool> keysPressed = new Dictionary<int, bool>();

        public void KeyPressed(Keys key, bool isKeyDown)
        {
            keysPressed[(int)key] = isKeyDown;
        }

        public bool IsKeyDown(Keys key)
        {
            bool isKeyDown;
            if (keysPressed.TryGetValue((int)key, out isKeyDown))
                return isKeyDown;
            return false;
        }



        #endregion
        #region Properties
        #endregion
        #region Events
        #endregion
        #region Constructor
        #endregion
        #region Factories
        #endregion
        #region Methods
        public void Manage(Window window)
        {
            SlimDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.Keyboard, SlimDX.RawInput.DeviceFlags.None);
            SlimDX.RawInput.Device.KeyboardInput += Device_KeyboardInput;
        }

        void Device_KeyboardInput(object sender, SlimDX.RawInput.KeyboardInputEventArgs e)
        {
            bool isKeyDown = (e.State == KeyState.Pressed || e.State == KeyState.SystemKeyPressed);
            KeyPressed(e.Key, isKeyDown);    
        }
        #endregion
    }
}
