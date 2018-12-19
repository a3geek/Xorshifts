using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xorshifts.Examples
{
    public class Example : MonoBehaviour
    {
        public GpuXorwow xorwow;


        private void Awake()
        {
            this.xorwow.Initialize();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                this.xorwow.Generate();

                var arr = new uint[this.xorwow.Count];
                this.xorwow.RandsBuffer.GetData(arr);

                Debug.Log(arr[0]);
            }
        }
    }
}
