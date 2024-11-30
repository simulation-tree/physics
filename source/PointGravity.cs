using Physics.Components;
using System;
using System.Numerics;
using Transforms;
using Transforms.Components;
using Unmanaged;
using Worlds;

namespace Physics
{
    public readonly struct PointGravity : IEntity
    {
        public readonly GravitySource gravity;

        public readonly ref float Force => ref gravity.Force;
        public readonly ref float Radius => ref gravity.AsEntity().GetComponentRef<IsPointGravity>().radius;
        public readonly Vector3 Position => ((Transform)gravity).WorldPosition;

        readonly uint IEntity.Value => gravity.GetEntityValue();
        readonly World IEntity.World => gravity.GetWorld();
        readonly Definition IEntity.Definition => new Definition().AddComponentTypes<IsPointGravity, IsGravitySource>();

#if NET
        [Obsolete("Default constructor not available", true)]
        public PointGravity()
        {
            throw new NotSupportedException();
        }
#endif

        public PointGravity(World world, Vector3 position, float radius, float force = 9.8067f)
        {
            gravity = new(world, position, Rotation.Default.value, force);
            gravity.AddComponent(new IsPointGravity(radius));
        }

        public readonly void Dispose()
        {
            gravity.Dispose();
        }

        public readonly override string ToString()
        {
            USpan<char> buffer = stackalloc char[64];
            uint length = ToString(buffer);
            return buffer.Slice(0, length).ToString();
        }

        public readonly uint ToString(USpan<char> buffer)
        {
            return gravity.ToString(buffer);
        }

        public static implicit operator GravitySource(PointGravity gravity)
        {
            return gravity.gravity;
        }

        public static implicit operator Entity(PointGravity gravity)
        {
            return gravity.gravity;
        }

        public static implicit operator Transform(PointGravity gravity)
        {
            return gravity.gravity;
        }
    }
}