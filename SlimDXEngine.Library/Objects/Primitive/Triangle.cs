﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D11;
using SlimDXEngine.Library.Graphics;
using SlimDXEngine.Library.Objects;

namespace SlimDXEngine.Library.Objects.Primitive
{
    public class Triangle : PositionedObject
    {
        #region Fields
        protected DataStream vertices;
        #endregion
        #region Properties
        
        #endregion
        #region Events
        #endregion
        #region Constructor
        public Triangle()
        {
            Rotation = new Vector3(0f, (float)System.Math.PI , 0f);
        }
        #endregion
        #region Factories
        #endregion
        #region Methods

        internal override void Draw(DeviceContext context)
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

            DataStream vertices = new DataStream((Vector3.SizeInBytes + 4) * 3, true, true);
            vertices.Write(new ColoredVertex(new Vector3(0.0f, 0.5f, 0.0f), new Color4(1.0f, 1.0f, 0.0f, 0.0f).ToArgb()));
            vertices.Write(new ColoredVertex(new Vector3(0.45f, -0.5f, 0.0f), new Color4(1.0f, 0.0f, 1.0f, 0.0f).ToArgb()));
            vertices.Write(new ColoredVertex(new Vector3(-0.45f, -0.5f, 0.0f), new Color4(1.0f, 0.0f, 0.0f, 1.0f).ToArgb()));
            vertices.Position = 0;
            BufferDescription bd = new BufferDescription()
            {
                Usage = ResourceUsage.Default,
                SizeInBytes = 16 * 3,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None
            };

            var vertexBuffer = new SlimDX.Direct3D11.Buffer(context.Device, vertices, bd);



            context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBuffer, 16, 0));
            //context.InputAssembler.SetIndexBuffer(indices, Format.R16_UInt, 0);

            /* scale * rotation * translation */
            Matrix worldMatrix = Matrix.Scaling(Scale) * Matrix.RotationYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z) *  Matrix.Translation(Position);

            Matrix viewMatrix = Camera.ViewMatrix;

            Matrix projectionMatrix = Camera.ProjectionMatrix;

            effect.GetVariableByName("finalMatrix").AsMatrix().SetMatrix(worldMatrix * viewMatrix * projectionMatrix);

            context.InputAssembler.InputLayout = inputLayout;
            context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;



            pass.Apply(context);
            context.Draw(3, 0);

        }
        public override void Update(float elapsedTime)
        {
            base.Update(elapsedTime);
        }
        #endregion
    }
}
