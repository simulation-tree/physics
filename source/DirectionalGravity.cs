using Physics.Components;
using System;
using System.Numerics;
using Transforms;
using Transforms.Components;
using Unmanaged;
using Worlds;

namespace Physics
{
    public readonly struct DirectionalGravity : IEntity
    {
        public readonly GravitySource gravity;

        public readonly ref float Force => ref gravity.Force;
        public readonly Vector3 Direction
        {
            get
            {
                Transform transform = gravity;
                return Vector3.Transform(Vector3.UnitZ, transform.WorldRotation);
            }
        }

        readonly uint IEntity.Value => gravity.GetEntityValue();
        readonly World IEntity.World => gravity.GetWorld();
        readonly Definition IEntity.Definition => new Definition().AddComponentTypes<IsDirectionalGravity, IsGravitySource>();

#if NET
        [Obsolete("Default constructor not available", true)]
        public DirectionalGravity()
        {
            throw new NotSupportedException();
        }
#endif

        public DirectionalGravity(World world, Quaternion rotation, float force = 9.8067f)
        {
            gravity = new(world, Position.Default.value, rotation, force);
            gravity.AddComponent(new IsDirectionalGravity());
        }

        public DirectionalGravity(World world, Vector3 direction, float force = 9.8067f)
        {
            gravity = new(world, Position.Default.value, Rotation.FromDirection(direction).value, force);
            gravity.AddComponent(new IsDirectionalGravity());
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

        public static implicit operator GravitySource(DirectionalGravity gravity)
        {
            return gravity.gravity;
        }

        public static implicit operator Entity(DirectionalGravity gravity)
        {
            return gravity.gravity;
        }

        public static implicit operator Transform(DirectionalGravity gravity)
        {
            return gravity.gravity;
        }
    }
}