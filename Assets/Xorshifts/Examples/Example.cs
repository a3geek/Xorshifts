using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xorshifts.Examples
{
    public class Example : MonoBehaviour
    {
        [SerializeField]
        private int count = 5;
        [SerializeField]
        private GpuXorwow xorwow = null;


        private void Awake()
        {
            var str = "";
            for(var i = 0; i < this.count; i++)
            {
                str += Xorshift.Generate128() + ", ";
            }
            Debug.Log("Xorshifts : " + str);

            str = "";
            for(var i = 0; i < this.count; i++)
            {
                str += XorshiftPlus.Generate128() + ", ";
            }
            Debug.Log("XorshiftPlus : " + str);

            str = "";
            for(var i = 0; i < this.count; i++)
            {
                str += XorshiftStar.Generate() + ", ";
            }
            Debug.Log("XorshiftStar : " + str);

            str = "";
            for(var i = 0; i < this.count; i++)
            {
                str += Xorwow.Generate() + ", ";
            }
            Debug.Log("Xorwow : " + str);

            str = "";
            this.xorwow.Initialize(this.count);
            this.xorwow.Generate();
            var arr = new uint[this.count];
            this.xorwow.Rands.GetData(arr);
            for(var i = 0; i < this.count; i++)
            {
                str += arr[i] + ", ";
            }
            Debug.Log("GpuXorwow : " + str);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                this.xorwow.Generate();

                //var arr = new uint[this.xorwow.Rands.count];
                //this.xorwow.Rands.GetData(arr);

                //Debug.Log(arr[0]);
                //Debug.Log(arr[arr.Length - 1]);
                //Debug.Log("");
            }
        }
    }
}
