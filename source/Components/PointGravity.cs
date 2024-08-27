using System;

namespace Physics.Components
{
    public struct PointGravity
    {
        public float radius;

#if NET
        [Obsolete("Default constructor not available", true)]
        public PointGravity()
        {
            throw new NotSupportedException();
        }
#endif

        public PointGravity(float radius)
        {
            this.radius = radius;
        }
    }
}