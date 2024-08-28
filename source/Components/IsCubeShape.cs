using System;
using System.Numerics;

namespace Physics.Components
{
    public struct IsCubeShape
    {
        public Vector3 extents;

#if NET
        [Obsolete("Default constructor not available", true)]
        public IsCubeShape()
        {
            throw new NotSupportedException();
        }
#endif

        public IsCubeShape(Vector3 extents)
        {
            this.extents = extents;
        }

        public IsCubeShape(float extent)
        {
            this.extents = new(extent);
        }

        public IsCubeShape(float x, float y, float z)
        {
            this.extents = new(x, y, z);
        }
    }
}