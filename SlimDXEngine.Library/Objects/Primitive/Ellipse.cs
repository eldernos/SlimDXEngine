using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D11;
using SlimDXEngine.Library.Graphics;

namespace SlimDXEngine.Library.Objects.Primitive
{
    public class Ellipse : PositionedObject
    {
        #region Fields
        protected int detail;
        #endregion
        #region Properties
        public int Detail
        {
            get { return detail; }
            set { detail = value; }
        }
        #endregion
        #region Events
        #endregion
        #region Constructor
        public Ellipse()
        {
            detail = 200;
        }
        #endregion
        #region Factories
        #endregion
        #region Methods
        internal override void Draw(SlimDX.Direct3D11.DeviceContext context)
        {
            Effect effect;
            using (ShaderBytecode byteCode = ShaderBytecode.CompileFromFile("Graphics/Effects/default.fx", "bidon", "fx_5_0", ShaderFlags.OptimizationLevel3, EffectFlags.None))
            {
                effect = new Effect(context.Device, byteCode);
            }
            var technique = effect.GetTechniqueByIndex(1);
            var pass = technique.GetPassByIndex(0);
            InputLayout inputLayout = new InputLayout(context.Device, pass.Description.Signature, new[] {
                new InputElement("POSITION", 0, SlimDX.DXGI.Format.R32G32B32_Float, 0),
                new InputElement("COLOR", 0, SlimDX.DXGI.Format.R8G8B8A8_UNorm, InputElement.AppendAligned, 0)
            });

            ColoredVertex first = default(ColoredVertex);
            DataStream vertices = new DataStream((Vector3.SizeInBytes + 4) * (detail + 1), true, true);
            for (int i = 0; i < detail; i++)
            {
                float angle = 3.14159f * 2f / detail * i;
                Vector3 pos = new Vector3((float)Math.Sin(angle), (float)Math.Cos(angle), 0);
                if (i == 0) first = new ColoredVertex(pos, new Color4(1.0f, 0.0f, 0.0f, 0.0f).ToArgb());
                vertices.Write(new ColoredVertex(pos, new Color4(1.0f, 0.0f, 0.0f, 0.0f).ToArgb()));
                // Now you fill this position into a vertexbuffer. If you don't know how to do this, the directx sdk got alot of tutorials and there are also alot available in the internet.
            }
            vertices.Write(first);
            /*
            vertices.Write(new ColoredVertex(new Vector3(1.0f, 1.0f, 0.0f), new Color4(1.0f, 0.0f, 0.0f, 0.0f).ToArgb()));
            vertices.Write(new ColoredVertex(new Vector3(-1.0f, 1.0f, 0.0f), new Color4(1.0f, 0.0f, 0.0f, 0.0f).ToArgb()));
            vertices.Write(new ColoredVertex(new Vector3(-1.0f, -1.0f, 0.0f), new Color4(1.0f, 0.0f, 0.0f, 0.0f).ToArgb()));
            vertices.Write(new ColoredVertex(new Vector3(-1.0f, -1.0f, 0.0f), new Color4(1.0f, 0.0f, 0.0f, 0.0f).ToArgb()));
            vertices.Write(new ColoredVertex(new Vector3(1.0f, 1.0f, 0.0f), new Color4(1.0f, 0.0f, 0.0f, 0.0f).ToArgb()));
            vertices.Write(new ColoredVertex(new Vector3(1.0f, -1.0f, 0.0f), new Color4(1.0f, 0.0f, 0.0f, 0.0f).ToArgb()));
            */
            vertices.Position = 0;
            BufferDescription bd = new BufferDescription()
            {
                Usage = ResourceUsage.Default,
                SizeInBytes = 16 * (detail + 1),
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None
            };

            var vertexBuffer = new SlimDX.Direct3D11.Buffer(context.Device, vertices, bd);



            context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBuffer, 16, 0));
            //context.InputAssembler.SetIndexBuffer(indices, Format.R16_UInt, 0);

            /* scale * rotation * translation */
            Matrix worldMatrix = Matrix.Scaling(Scale) * Matrix.RotationYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z) * Matrix.Translation(Position);

            Matrix viewMatrix = Camera.ViewMatrix;

            Matrix projectionMatrix = Camera.ProjectionMatrix;

            effect.GetVariableByName("finalMatrix").AsMatrix().SetMatrix(worldMatrix * viewMatrix * projectionMatrix);

            context.InputAssembler.InputLayout = inputLayout;
            context.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineStrip;



            pass.Apply(context);
            context.Draw(detail + 1, 0);
        }
        #endregion
    }
}
