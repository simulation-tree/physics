using System;
using System.Numerics;

namespace Physics.Components
{
    public struct CubeShape
    {
        public Vector3 extents;

#if NET
        [Obsolete("Default constructor not available", true)]
        public CubeShape()
        {
            throw new NotSupportedException();
        }
#endif

        public CubeShape(Vector3 extents)
        {
            this.extents = extents;
        }

        public CubeShape(float extent)
        {
            this.extents = new(extent);
        }

        public CubeShape(float x, float y, float z)
        {
            this.extents = new(x, y, z);
        }
    }
}