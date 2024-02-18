using UnityEngine;

namespace Xorshifts.Classes
{
    public class Dispatcher
    {
        public int Kernel { get; } = -1;
        public int GroupsX { get; } = 0;


        public Dispatcher(ComputeShader cs, string kernel, int count)
        {
            this.Kernel = cs.FindKernel(kernel);

            cs.GetKernelThreadGroupSizes(this.Kernel, out var x, out var _, out _);
            this.GroupsX = Mathf.CeilToInt(count / (float)x);
        }

        public bool Dispatch(ComputeShader cs)
        {
            if(this.Kernel < 0)
            {
                return false;
            }

            cs.Dispatch(this.Kernel, this.GroupsX, 1, 1);
            return true;
        }
    }
}
