using Physics.Components;
using System;
using System.Numerics;
using Transforms;
using Transforms.Components;
using Unmanaged;
using Worlds;

namespace Physics
{
    public readonly struct GravitySource : IEntity
    {
        private readonly Transform transform;

        public readonly bool IsDirectional => transform.AsEntity().ContainsComponent<IsDirectionalGravity>();
        public readonly bool IsPoint => transform.AsEntity().ContainsComponent<IsPointGravity>();
        public readonly ref float Force => ref transform.AsEntity().GetComponentRef<IsGravitySource>().force;

        readonly uint IEntity.Value => transform.GetEntityValue();
        readonly World IEntity.World => transform.GetWorld();
        readonly Definition IEntity.Definition => new Definition().AddComponentType<IsGravitySource>();

#if NET
        [Obsolete("Default constructor not available", true)]
        public GravitySource()
        {
            throw new NotSupportedException();
        }
#endif

        public GravitySource(World world, Vector3 position, Quaternion rotation, float force = 9.8067f)
        {
            transform = new(world, position, rotation, Scale.Default.value);
            transform.AddComponent(new IsGravitySource(force));
        }

        public readonly void Dispose()
        {
            transform.Dispose();
        }

        public readonly override string ToString()
        {
            USpan<char> buffer = stackalloc char[64];
            uint length = ToString(buffer);
            return buffer.Slice(0, length).ToString();
        }

        public readonly uint ToString(USpan<char> buffer)
        {
            uint length = 0;
            if (IsDirectional)
            {
                Quaternion rotation = transform.WorldRotation;
                length += "Directional Gravity(".AsUSpan().CopyTo(buffer);
                Vector3 direction = Vector3.Transform(Vector3.UnitZ, rotation);
                length += direction.ToString(buffer.Slice(length));
                buffer[length++] = ',';
                buffer[length++] = ' ';
                length += Force.ToString(buffer.Slice(length));
                buffer[length++] = ')';
            }
            else
            {
                Vector3 position = transform.WorldPosition;
                length += "Point Gravity(".AsUSpan().CopyTo(buffer);
                length += position.ToString(buffer.Slice(length));
                buffer[length++] = ',';
                buffer[length++] = ' ';
                length += Force.ToString(buffer.Slice(length));
                buffer[length++] = ')';
            }

            return length;
        }

        public static implicit operator Entity(GravitySource gravity)
        {
            return gravity.transform;
        }

        public static implicit operator Transform(GravitySource gravity)
        {
            return gravity.transform;
        }
    }
}