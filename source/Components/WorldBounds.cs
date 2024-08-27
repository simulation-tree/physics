using System.Numerics;

namespace Physics.Components
{
    public struct WorldBounds
    {
        public Vector3 min;
        public Vector3 max;

        public WorldBounds(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public readonly bool Contains(Vector3 point)
        {
            return point.X >= min.X && point.X <= max.X &&
                   point.Y >= min.Y && point.Y <= max.Y &&
                   point.Z >= min.Z && point.Z <= max.Z;
        }

        public readonly bool Intersects(WorldBounds other)
        {
            return min.X <= other.max.X && max.X >= other.min.X &&
                   min.Y <= other.max.Y && max.Y >= other.min.Y &&
                   min.Z <= other.max.Z && max.Z >= other.min.Z;
        }
    }
}