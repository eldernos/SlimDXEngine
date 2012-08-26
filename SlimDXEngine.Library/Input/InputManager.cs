using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlimDXEngine.Library.Graphics;
using SlimDXEngine.Library.Input.Sources;

namespace SlimDXEngine.Library.Input
{
    public static class InputManager
    {
        #region Fields
        private static Mouse mouse;
        private static Keyboard keyboard;
      
        #endregion
        #region Properties
        public static Mouse Mouse
        {
            get
            {
                return mouse = mouse ?? new Mouse();
            }
        }
        public static Keyboard Keyboard
        {
            get
            {
                return keyboard = keyboard ?? new Keyboard();
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
        public static void Update()
        {
            Mouse.Update();
        }
        public static void ManageInput(Window window)
        {
            Mouse.Manage(window);
            Keyboard.Manage(window);
        }
        public static string DebugStatus()
        {
            return "Mouse Screen Position: " + Mouse.ScreenMouseAt.ToString();
        }
        #endregion 

    }
}
