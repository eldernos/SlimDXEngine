using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;
using SlimDX.Windows;
using SlimDXEngine.Library.Configuration;
using SlimDXEngine.Library.Diagnostics;
using SlimDXEngine.Library.Graphics;
using SlimDXEngine.Library.Input;
using SlimDXEngine.Library.Objects;
using SlimDXEngine.Library.Objects.Primitive;
using SlimDXEngine.Library.UI;
using SlimDXEngine.Library.Utility;

namespace SlimDXEngine.Library
{
    public class Game : IDisposable
    {
        #region Fields
        private static Game instance;
        private static dlgDebug debug;
        private readonly Clock clock;
        
        private readonly IDisposable apiContext;
        private EngineConfiguration configuration;
        private Window formWindow;
        #endregion
        #region Properties
        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game();
                }
                return instance;
            }
        }
        public Clock Clock
        {
            get
            {
                return clock;
            }
        }
        #endregion
        #region Events
        #endregion
        #region Constructor
        private Game()
        {
            formWindow = new Window(1024, 768);
            InputManager.ManageInput(formWindow);
            clock = new Clock();
            clock.Start();
            debug = new dlgDebug();


            Ellipse e = new Ellipse();
            e.Scale = new Vector3(20, 10, 1);
            e.RotationVelocity = new Vector3(0, 0, 1);
            ObjectManager.AddPositionedObject(e);

        }
        #endregion
        #region Factories
        #endregion
        #region Methods
        public void Run()
        {
            MessagePump.Run(formWindow.Form, () =>
            {
                Update();
                Draw();

            });
        }
        private void Update()
        {
            float elapsedTime = clock.Update();
            if (InputManager.Keyboard.IsKeyDown(System.Windows.Forms.Keys.F12))
            {
                debug.Show();
            }
            debug.Set(InputManager.DebugStatus());
            InputManager.Update();
            ObjectManager.Update(elapsedTime);
        }
        private void Draw()
        {
            formWindow.Draw();
        }
        private void Pause()
        {

        }
        private void Quit()
        {

        }
        public void Dispose()
        {
            formWindow.Dispose();
        }
        #endregion 

    
       
    }
}
