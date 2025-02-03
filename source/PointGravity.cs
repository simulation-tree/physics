using Physics.Components;
using System.Numerics;
using Transforms;
using Transforms.Components;
using Unmanaged;
using Worlds;

namespace Physics
{
    public readonly partial struct PointGravity : IEntity
    {
        public readonly ref float Force => ref As<GravitySource>().Force;
        public readonly ref float Radius => ref As<GravitySource>().GetComponent<IsPointGravity>().radius;
        public readonly Vector3 Position => As<Transform>().WorldPosition;

        public PointGravity(World world, Vector3 position, float radius, float force = 9.8067f)
        {
            this.world = world;
            value = new GravitySource(world, position, Rotation.Default.value, force).value;
            AddComponent(new IsPointGravity(radius));
        }

        readonly void IEntity.Describe(ref Archetype archetype)
        {
            archetype.AddComponentType<IsPointGravity>();
            archetype.Add<GravitySource>();
        }

        public readonly override string ToString()
        {
            USpan<char> buffer = stackalloc char[64];
            uint length = ToString(buffer);
            return buffer.Slice(0, length).ToString();
        }

        public readonly uint ToString(USpan<char> buffer)
        {
            return value.ToString(buffer);
        }

        public static implicit operator GravitySource(PointGravity gravity)
        {
            return gravity.As<GravitySource>();
        }

        public static implicit operator Transform(PointGravity gravity)
        {
            return gravity.As<Transform>();
        }
    }
}