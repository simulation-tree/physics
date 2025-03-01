using Physics.Components;
using System.Numerics;
using Transforms;
using Transforms.Components;
using Unmanaged;
using Worlds;

namespace Physics
{
    public readonly partial struct DirectionalGravity : IEntity
    {
        public readonly ref float Force => ref As<GravitySource>().Force;

        public readonly Vector3 Direction
        {
            get
            {
                Transform transform = As<Transform>();
                return Vector3.Transform(Vector3.UnitZ, transform.WorldRotation);
            }
        }

        public DirectionalGravity(World world, Quaternion rotation, float force = 9.8067f)
        {
            this.world = world;
            value = new GravitySource(world, Position.Default.value, rotation, force).value;
            AddTag<IsDirectionalGravity>();
        }

        public DirectionalGravity(World world, Vector3 direction, float force = 9.8067f)
        {
            this.world = world;
            value = new GravitySource(world, Position.Default.value, Rotation.FromDirection(direction).value, force).value;
            AddTag<IsDirectionalGravity>();
        }

        readonly void IEntity.Describe(ref Archetype archetype)
        {
            archetype.AddTagType<IsDirectionalGravity>();
            archetype.Add<GravitySource>();
        }

        public readonly override string ToString()
        {
            USpan<char> buffer = stackalloc char[64];
            uint length = ToString(buffer);
            return buffer.GetSpan(length).ToString();
        }

        public readonly uint ToString(USpan<char> buffer)
        {
            return value.ToString(buffer);
        }

        public static implicit operator GravitySource(DirectionalGravity gravity)
        {
            return gravity.As<GravitySource>();
        }

        public static implicit operator Transform(DirectionalGravity gravity)
        {
            return gravity.As<Transform>();
        }
    }
}