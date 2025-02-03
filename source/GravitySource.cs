using Physics.Components;
using System;
using System.Numerics;
using Transforms;
using Transforms.Components;
using Unmanaged;
using Worlds;

namespace Physics
{
    public readonly partial struct GravitySource : IEntity
    {
        public readonly bool IsDirectional => ContainsComponent<IsDirectionalGravity>();
        public readonly bool IsPoint => ContainsComponent<IsPointGravity>();
        public readonly ref float Force => ref GetComponent<IsGravitySource>().force;

        public GravitySource(World world, Vector3 position, Quaternion rotation, float force = 9.8067f)
        {
            this.world = world;
            value = new Transform(world, position, rotation, Scale.Default.value).value;
            AddComponent(new IsGravitySource(force));
        }

        readonly void IEntity.Describe(ref Archetype archetype)
        {
            archetype.AddComponentType<IsGravitySource>();
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
                Quaternion rotation = As<Transform>().WorldRotation;
                USpan<char> template = "Directional Gravity(".AsSpan();
                length += template.CopyTo(buffer);
                Vector3 direction = Vector3.Transform(Vector3.UnitZ, rotation);
                length += direction.ToString(buffer.Slice(length));
                buffer[length++] = ',';
                buffer[length++] = ' ';
                length += Force.ToString(buffer.Slice(length));
                buffer[length++] = ')';
            }
            else
            {
                Vector3 position = As<Transform>().WorldPosition;
                USpan<char> template = "Point Gravity(".AsSpan();
                length += template.CopyTo(buffer);
                length += position.ToString(buffer.Slice(length));
                buffer[length++] = ',';
                buffer[length++] = ' ';
                length += Force.ToString(buffer.Slice(length));
                buffer[length++] = ')';
            }

            return length;
        }

        public static implicit operator Transform(GravitySource gravity)
        {
            return gravity.As<Transform>();
        }
    }
}