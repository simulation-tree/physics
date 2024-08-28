using System;

namespace Physics.Components
{
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