using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Xorshifts
{
    using static Utilities.RandomExtensions;

    [AddComponentMenu("")]
    public class GpuXorwow : MonoBehaviour
    {
        public const int ThreadX = 1024;
        public const string PropRands = "_Rands";
        public const string PropStates = "_States";
        public const string PropInitInfo = "_InitInfo";

        public int Count
        {
            get { return this.count; }
            set { this.count = Mathf.Max(1, value); }
        }
        public ComputeBuffer RandsBuffer
        {
            get; private set;
        }

        [SerializeField, Range(1, 32767)]
        private int count = 100;
        [SerializeField]
        private ComputeShader cs = null;

        private ComputeBuffer statesBuffer = null;
        private ComputeBuffer initInfoBuffer = null;

        
        public void Initialize()
        {
            this.OnDestroy();

            this.RandsBuffer = new ComputeBuffer(this.count, Marshal.SizeOf(typeof(uint)), ComputeBufferType.Default);
            this.statesBuffer = new ComputeBuffer(this.count, Marshal.SizeOf(typeof(Xorwow.State)), ComputeBufferType.Default);
            this.initInfoBuffer = new ComputeBuffer(1, Marshal.SizeOf(typeof(InitInfo)), ComputeBufferType.Default);
            this.initInfoBuffer.SetData(new InitInfo[]
            {
                new InitInfo()
                {
                    Max = Convert.ToUInt32(this.count),
                    Seed = RandomUInt()
                }
            });

            this.cs.SetBuffer(0, PropStates, this.statesBuffer);
            this.cs.SetBuffer(0, PropInitInfo, this.initInfoBuffer);
            this.cs.Dispatch(0, Mathf.CeilToInt(this.count / (float)ThreadX), 1, 1);

            this.cs.SetBuffer(1, PropRands, this.RandsBuffer);
            this.cs.SetBuffer(1, PropStates, this.statesBuffer);
            this.cs.SetBuffer(1, PropInitInfo, this.initInfoBuffer);
        }

        public void Generate()
        {
            if(this.RandsBuffer == null)
            {
                this.Initialize();
            }

            this.cs.Dispatch(1, Mathf.CeilToInt(this.count / (float)ThreadX), 1, 1);
        }
        
        private void OnDestroy()
        {
            this.RandsBuffer?.Dispose();
            this.statesBuffer?.Dispose();
            this.initInfoBuffer?.Dispose();
        }

        [Serializable]
        private struct InitInfo
        {
            public uint Max;
            public uint Seed;
        }
    }
}
