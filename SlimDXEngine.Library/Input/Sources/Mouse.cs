using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;
using SlimDX.Multimedia;
using SlimDX.RawInput;
using SlimDXEngine.Library.Graphics;

namespace SlimDXEngine.Library.Input.Sources
{
    public class Mouse
    {
        #region Fields
        private Vector2 mousePos;
        private Dictionary<int, bool> buttonsPressed = new Dictionary<int, bool>();
        #endregion
        #region Properties
        public Vector2 ScreenMouseAt
        {
            get
            {
                return mousePos;
            }
        }
        #endregion
        #region Events
        #endregion
        #region Constructor
        public Mouse()
        {
            SlimDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.Mouse, SlimDX.RawInput.DeviceFlags.None);
            SlimDX.RawInput.Device.MouseInput += Device_MouseInput;
        }

        void Device_MouseInput(object sender, MouseInputEventArgs e)
        {
            /*
            if ((e.ButtonFlags & MouseButtonFlags.LeftDown) == MouseButtonFlags.LeftDown)
                ButtonPressed(0);
            else if ((e.ButtonFlags & MouseButtonFlags.LeftUp) == MouseButtonFlags.LeftUp)
                ButtonReleased(0);

            if ((e.ButtonFlags & MouseButtonFlags.RightDown) == MouseButtonFlags.RightDown)
                ButtonPressed(1);
            else if ((e.ButtonFlags & MouseButtonFlags.RightUp) == MouseButtonFlags.RightUp)
                ButtonReleased(1);

            if ((e.ButtonFlags & MouseButtonFlags.MiddleDown) == MouseButtonFlags.MiddleDown)
                ButtonPressed(2);
            else if ((e.ButtonFlags & MouseButtonFlags.MiddleUp) == MouseButtonFlags.MiddleUp)
                ButtonReleased(2);

            if ((e.ButtonFlags & MouseButtonFlags.Button4Down) == MouseButtonFlags.Button4Down)
                ButtonPressed(3);
            else if ((e.ButtonFlags & MouseButtonFlags.Button4Up) == MouseButtonFlags.Button4Up)
               ButtonReleased(3);

            if ((e.ButtonFlags & MouseButtonFlags.Button5Down) == MouseButtonFlags.Button5Down)
                ButtonPressed(4);
            else if ((e.ButtonFlags & MouseButtonFlags.Button5Up) == MouseButtonFlags.Button5Up)
                ButtonReleased(4);
            */
           
            mousePos.X += e.X;
            mousePos.Y += e.Y;
        }
        #endregion
        #region Factories
        #endregion
        #region Methods
        public void Update()
        {
            mousePos = Vector2.Zero;
        }
        public void Manage(Window window)
        {

        }
        #endregion
    }
}
