using System;
using Worlds;

namespace Physics.Components
{
    [Component]
    public struct IsPointGravity
    {
        public float radius;

#if NET
        [Obsolete("Default constructor not available", true)]
        public IsPointGravity()
        {
            throw new NotSupportedException();
        }
#endif

        public IsPointGravity(float radius)
        {
            this.radius = radius;
        }
    }
}