using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Xorshifts
{
    using Classes;
    using Random = UnityEngine.Random;

    [Serializable]
    public class GpuXorwow : MonoBehaviour, IDisposable
    {
        private const string InitKernel = "Init";
        private const string XorwowKernel = "Xorwow";
        private static readonly int PropMax = Shader.PropertyToID("_Max");
        private static readonly int PropSeed = Shader.PropertyToID("_Seed");
        private static readonly int PropRands = Shader.PropertyToID("_Rands");
        private static readonly int PropStates = Shader.PropertyToID("_States");

        public bool Inited { get; private set; } = false;
        public GraphicsBuffer Rands { get; private set; } = null;

        [SerializeField]
        private ComputeShader cs = null;

        private Dispatcher init = null;
        private Dispatcher xorwow = null;
        private GraphicsBuffer states = null;


        public void Initialize(int count)
        {
            this.Rands ??= new GraphicsBuffer(
                GraphicsBuffer.Target.Structured, count, Marshal.SizeOf(typeof(uint))
            );
            this.states ??= new GraphicsBuffer(
                GraphicsBuffer.Target.Structured, count, Marshal.SizeOf(typeof(Xorwow.States))
            );

            this.init ??= new Dispatcher(this.cs, InitKernel, count);
            this.xorwow ??= new Dispatcher(this.cs, XorwowKernel, count);

            this.cs.SetInt(PropMax, count);
            this.cs.SetInt(PropSeed, Random.Range(int.MinValue, int.MaxValue));
            this.cs.SetBuffer(this.init.Kernel, PropStates, this.states);
            this.cs.SetBuffer(this.xorwow.Kernel, PropStates, this.states);
            this.cs.SetBuffer(this.xorwow.Kernel, PropRands, this.Rands);

            this.init.Dispatch(this.cs);
            this.Inited = true;
        }

        public void Generate()
        {
            if(!this.Inited)
            {
                return;
            }

            this.xorwow.Dispatch(this.cs);
        }

        public void Dispose()
        {
            this.Rands?.Dispose();
            this.states?.Dispose();
        }

        private void OnDestroy()
        {
            this.Dispose();
        }
    }
}
