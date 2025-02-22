using Shapes;
using System;
using System.Numerics;

namespace Physics.Components
{
    public struct IsBody
    {
        public uint version;
        public Shape shape;
        public Vector3 offset;
        public BodyType type;

#if NET
        [Obsolete("Default constructor not available", true)]
        public IsBody()
        {
            throw new NotSupportedException();
        }
#endif

        public IsBody(Shape shape, BodyType type, Vector3 offset = default)
        {
            version = default;
            this.shape = shape;
            this.offset = offset;
            this.type = type;
        }
    }
}