using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xorshifts.Utilities
{
    using Random = UnityEngine.Random;

    public static class RandomExtensions
    {
        public static uint RandomUInt()
        {
            var body = Convert.ToUInt32(Random.Range(0, 1 << 30));
            var tail = Convert.ToUInt32(Random.Range(0, 1 << 2));
            return (body << 2) | tail;
        }

        public static ulong RandomULong()
        {
            var body = Convert.ToUInt64(RandomUInt());
            var tail = Convert.ToUInt64(RandomUInt());
            return (body << 32) | tail;
        }

        public static void CheckZeroValue(ref uint state)
        {
            state = state == 0u ? RandomUInt() : state;
        }

        public static void CheckZeroValue(ref ulong state)
        {
            state = state == 0ul ? RandomULong() : state;
        }
    }
}
