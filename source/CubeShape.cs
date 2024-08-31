using System.Numerics;

namespace Physics
{
    public readonly struct CubeShape : IShape
    {
        public readonly Vector3 extents;

        byte IShape.TypeIndex => 2;

        public CubeShape(Vector3 extents)
        {
            this.extents = extents;
        }

        public CubeShape(float extent)
        {
            extents = new(extent);
        }

        public CubeShape(float x, float y, float z)
        {
            extents = new(x, y, z);
        }

        public static implicit operator Shape(CubeShape shape)
        {
            return Shape.Create(shape);
        }
    }
}