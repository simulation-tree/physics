using Physics.Components;
using System;
using System.Diagnostics;
using System.Numerics;
using Transforms;
using Unmanaged;
using Worlds;

namespace Physics
{
    public readonly partial struct Body : IEntity
    {
        public readonly ref Vector3 LinearVelocity
        {
            get
            {
                ThrowIfStatic();

                return ref GetComponent<LinearVelocity>().value;
            }
        }

        public readonly ref Vector3 AngularVelocity
        {
            get
            {
                ThrowIfStatic();

                return ref GetComponent<AngularVelocity>().value;
            }
        }

        public readonly ref float GravityScale
        {
            get
            {
                ThrowIfStatic();

                return ref GetComponent<GravityScale>().value;
            }
        }

        public readonly ref float Mass
        {
            get
            {
                ThrowIfStatic();

                return ref GetComponent<Mass>().value;
            }
        }

        public readonly BodyType Type => GetComponent<IsBody>().type;
        public readonly ref Shape Shape => ref GetComponent<IsBody>().shape;
        public readonly uint ContactCount => GetArrayLength<CollisionContact>();
        public readonly CollisionContact this[uint index] => GetArrayElement<CollisionContact>(index);

        public readonly USpan<CollisionContact> Contacts
        {
            get
            {
                if (TryGetArray(out USpan<CollisionContact> contacts))
                {
                    return contacts;
                }
                else
                {
                    return Array.Empty<CollisionContact>();
                }
            }
        }

        public readonly (Vector3 min, Vector3 max) Bounds
        {
            get
            {
                WorldBounds bounds = GetComponent<WorldBounds>();
                return (bounds.min, bounds.max);
            }
        }

        /// <summary>
        /// Creates a physics body with default mass and gravity scale.
        /// </summary>
        public Body(World world, Shape shape, BodyType type, Vector3 initialVelocity = default)
        {
            this.world = world;
            value = new Transform(world).value;
            AddComponent(new IsBody(shape, type));
            if (type != BodyType.Static)
            {
                AddComponent(new LinearVelocity(initialVelocity));
                AddComponent(new AngularVelocity());
                AddComponent(Components.GravityScale.Default);
                AddComponent(Components.Mass.Default);
            }
        }

        readonly void IEntity.Describe(ref Archetype archetype)
        {
            archetype.AddComponentType<IsBody>();
        }

        [Conditional("DEBUG")]
        private readonly void ThrowIfStatic()
        {
            if (Type == BodyType.Static)
            {
                throw new InvalidOperationException($"Physics body `{value}` is static");
            }
        }

        public static implicit operator Transform(Body body)
        {
            return body.As<Transform>();
        }
    }
}