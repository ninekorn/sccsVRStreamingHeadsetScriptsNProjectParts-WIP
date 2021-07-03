using System;
using System.Collections.Generic;
using System.Text;

using Jitter.LinearMath;

namespace Jitter
{
    public struct sc_jitter_data
    {
        public int smalliterations;
        public int iterations;
        public float allowedpenetration;
        public bool alloweddeactivation;
        public JVector gravity;
        public int width;
        public int height;
        public int depth;
    }

}
