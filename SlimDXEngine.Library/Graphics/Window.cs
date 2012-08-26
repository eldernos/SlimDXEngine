using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX.Windows;
using System.Windows.Forms;
using SlimDXEngine.Library.UI;
using SlimDXEngine.Library.Objects;

namespace SlimDXEngine.Library.Graphics
{
    public class Window : IDisposable
    {
        #region Fields
        protected RenderForm form;
        protected Device device;
        protected FormWindowState currentWindowState;
        protected bool isFullScreen;

        private readonly Bindable<float> framesPerSecond;
        private float frameAccumulator;
        private int frameCount;
        
        #endregion
        #region Properties
        public float FrameDelta { get; private set; }
        public int Width
        {
            get
            {
                return form.ClientSize.Width;
            }
        }
        public int Height
        {
            get
            {
                return form.ClientSize.Height;
            }
        }
        public RenderForm Form
        {
            get
            {
                return form;
            }
        }
        #endregion
        #region Events
        #endregion
        #region Constructor
        public Window() 
        {
            form = new RenderForm("Game");
            form.UserResized += HandleResize;
            device = Device.GetDevice(this);
            
        }
        public Window(int width, int height)
        {
            form = new RenderForm("Game");
            form.Width = width;
            form.Height = height;
            form.UserResized += HandleResize;
            device = Device.GetDevice(this);
            
        }
        #endregion
        #region Factories
        #endregion
        #region Methods
        public void Update()
        {
            FrameDelta = Game.Instance.Clock.Update();
        }
        public void Draw()
        {
            frameAccumulator += FrameDelta;
            ++frameCount;

            if (frameAccumulator >= 1.0f)
            {
                framesPerSecond.Value = frameCount / frameAccumulator;
                frameAccumulator = 0.0f;
                frameCount = 0;
            }
            device.Draw();
        }
        private void HandleResize(object sender, EventArgs e)
        {

        }
        public void Dispose()
        {
            device.Dispose();
        }
        #endregion 
    
        
    }
}
