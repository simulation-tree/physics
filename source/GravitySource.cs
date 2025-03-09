using Physics.Components;
using System;
using System.Numerics;
using Transforms;
using Transforms.Components;
using Worlds;

namespace Physics
{
    public readonly partial struct GravitySource : IEntity
    {
        public readonly bool IsDirectional => ContainsTag<IsDirectionalGravity>();
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
            Span<char> buffer = stackalloc char[64];
            int length = ToString(buffer);
            return buffer.Slice(0, length).ToString();
        }

        public readonly int ToString(Span<char> destination)
        {
            int length = 0;
            if (IsDirectional)
            {
                Quaternion rotation = As<Transform>().WorldRotation;
                ReadOnlySpan<char> template = "Directional Gravity(";
                template.CopyTo(destination);
                length += template.Length;
                Vector3 direction = Vector3.Transform(Vector3.UnitZ, rotation);
                length += direction.ToString(destination.Slice(length));
                destination[length++] = ',';
                destination[length++] = ' ';
                length += Force.ToString(destination.Slice(length));
                destination[length++] = ')';
            }
            else
            {
                Vector3 position = As<Transform>().WorldPosition;
                ReadOnlySpan<char> template = "Point Gravity(";
                template.CopyTo(destination);
                length += template.Length;
                length += position.ToString(destination.Slice(length));
                destination[length++] = ',';
                destination[length++] = ' ';
                length += Force.ToString(destination.Slice(length));
                destination[length++] = ')';
            }

            return length;
        }

        public static implicit operator Transform(GravitySource gravity)
        {
            return gravity.As<Transform>();
        }
    }
}