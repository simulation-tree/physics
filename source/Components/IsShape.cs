using System.Numerics;

namespace Physics.Components
{
    public struct IsShape
    {
        public Vector3 offset;

        public IsShape(Vector3 offset)
        {
            this.offset = offset;
        }
    }
}