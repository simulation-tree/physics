using System;

namespace Physics.Components
{
    public struct IsSphereShape
    {
        public float radius;

#if NET
        [Obsolete("Default constructor not available", true)]
        public IsSphereShape()
        {
            throw new NotSupportedException();
        }
#endif

        public IsSphereShape(float radius)
        {
            this.radius = radius;
        }
    }
}