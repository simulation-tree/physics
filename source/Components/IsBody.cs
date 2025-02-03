using System;
using Worlds;

namespace Physics.Components
{
    [Component]
    public struct IsBody
    {
        public uint version;
        public Shape shape;
        public BodyType type;

#if NET
        [Obsolete("Default constructor not available", true)]
        public IsBody()
        {
            throw new NotSupportedException();
        }
#endif

        public IsBody(Shape shape, BodyType type)
        {
            version = default;
            this.shape = shape;
            this.type = type;
        }
    }
}