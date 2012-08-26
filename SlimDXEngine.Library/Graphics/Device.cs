using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlimDX;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D11;
using SlimDX.DXGI;

using SlimDXEngine.Library.Objects;
using SlimDXEngine.Library.Objects.Primitive;

namespace SlimDXEngine.Library.Graphics
{
    public class Device: IDisposable
    {
        #region Fields
        protected Window window;

        protected SlimDX.Direct3D11.Device device;
        protected SwapChainDescription description;
        protected SwapChain swapChain;
        protected ShaderSignature inputSignature;
        protected InputLayout inputLayout;
        protected DeviceContext context;

        protected RenderTargetView renderTarget;
        protected Viewport viewport;
        protected List<VertexShader> vertexShaders;
        protected List<PixelShader> pixelShaders;

        int halfWidth;
        int halfHeight;

        #endregion
        #region Properties
        public SlimDX.Direct3D11.Device DXDevice
        {
            get { return device; }
        }
        public InputLayout InputLayout
        {
            get { return inputLayout; }
        }
        #endregion
        #region Events
        #endregion
        #region Constructor
        private Device()
        {

        }
        #endregion
        #region Factories
        public static Device GetDevice(Window window)
        {
            Device d = new Device();
            d.vertexShaders = new List<VertexShader>();
            d.pixelShaders = new List<PixelShader>();
            d.window = window;
            window.Form.UserResized += d.Form_Resize;
            window.Form.KeyDown += d.Key_Down;
            d.description = new SwapChainDescription()
            {
                BufferCount = 2,
                Usage = Usage.RenderTargetOutput,
                OutputHandle = window.Form.Handle,
                IsWindowed = true,
                ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                SampleDescription = new SampleDescription(1, 0),
                Flags = SwapChainFlags.AllowModeSwitch,
                SwapEffect = SwapEffect.Discard
            };
            SlimDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, d.description, out d.device, out d.swapChain);

            using (var resource = SlimDX.Direct3D11.Resource.FromSwapChain<Texture2D>(d.swapChain, 0))
            {
                d.renderTarget = new RenderTargetView(d.device, resource);
            }

            d.context = d.device.ImmediateContext;
            d.viewport = new Viewport(0.0f, 0.0f, window.Width, window.Height);

            d.context.OutputMerger.SetTargets(d.renderTarget);
            d.context.Rasterizer.SetViewports(d.viewport);

            d.halfWidth = window.Width / 2;
            d.halfHeight = window.Height / 2;

            /*
            using (var bytecode = ShaderBytecode.CompileFromFile("Graphics/Shaders/default.fx", "VS", "vs_4_0", ShaderFlags.None, EffectFlags.None))
            {
                d.inputSignature = ShaderSignature.GetInputSignature(bytecode);
                var  vertexShader = new VertexShader(d.device, bytecode);
                d.vertexShaders.Add(vertexShader);
            }
            using (var bytecode = ShaderBytecode.CompileFromFile("Graphics/Shaders/default.fx", "PS", "ps_4_0", ShaderFlags.None, EffectFlags.None))
            {
                var pixelShader = new PixelShader(d.device, bytecode);
                d.pixelShaders.Add(pixelShader);
            }
            
           

            d.context.VertexShader.Set(d.vertexShaders.First());
            d.context.PixelShader.Set(d.pixelShaders.First());

            
            
            d.inputLayout = new InputLayout(d.device, d.inputSignature, new[] {
                new InputElement("POSITION", 0, SlimDX.DXGI.Format.R32G32B32_Float, 0),
                new InputElement("COLOR", 0, SlimDX.DXGI.Format.R8G8B8A8_UNorm, InputElement.AppendAligned, 0)
            });


            d.context.InputAssembler.InputLayout = d.inputLayout;
            d.context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            */
            //d.context.I



            using (var factory = d.swapChain.GetParent<Factory>()) factory.SetWindowAssociation(d.window.Form.Handle, WindowAssociationFlags.IgnoreAltEnter);
            return d;
        }
        #endregion
        #region Methods
        private void Key_Down(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.Enter) swapChain.IsFullScreen = !swapChain.IsFullScreen;
        }
        public void Form_Resize(object sender, EventArgs e)
        {
            renderTarget.Dispose();
            swapChain.ResizeBuffers(2, 0, 0, Format.R8G8B8A8_UNorm, SwapChainFlags.AllowModeSwitch);
            using (var resource = SlimDX.Direct3D11.Resource.FromSwapChain<Texture2D>(swapChain, 0))
            {
                renderTarget = new RenderTargetView(device, resource);
            }
            context.OutputMerger.SetTargets(renderTarget);
        }
        public void Draw()
        {
            context.ClearRenderTargetView(renderTarget, new Color4(1f, 1f, 1f));
            ObjectManager.Draw(context);
            swapChain.Present(0, PresentFlags.None);
        }
        public void Dispose()
        {
            swapChain.Dispose();
            device.Dispose();
            context.Dispose();
            renderTarget.Dispose();

            foreach (VertexShader shader in vertexShaders)
            {
                shader.Dispose();
            }
            foreach (PixelShader shader in pixelShaders)
            {
                shader.Dispose();
            }
        }
        #endregion

        
    }
}
