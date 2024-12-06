using Physics.Components;
using System;
using System.Diagnostics;
using System.Numerics;
using Transforms;
using Unmanaged;
using Worlds;

namespace Physics
{
    public readonly struct Body : IEntity
    {
        private readonly Transform transform;

        public readonly ref Vector3 LinearVelocity
        {
            get
            {
                ThrowIfBodyIsStatic();
                return ref transform.AsEntity().GetComponent<LinearVelocity>().value;
            }
        }

        public readonly ref Vector3 AngularVelocity
        {
            get
            {
                ThrowIfBodyIsStatic();
                return ref transform.AsEntity().GetComponent<AngularVelocity>().value;
            }
        }

        public readonly ref float GravityScale
        {
            get
            {
                ThrowIfBodyIsStatic();
                return ref transform.AsEntity().GetComponent<GravityScale>().value;
            }
        }

        public readonly ref float Mass
        {
            get
            {
                ThrowIfBodyIsStatic();
                return ref transform.AsEntity().GetComponent<Mass>().value;
            }
        }

        public readonly ref Shape Shape => ref transform.AsEntity().GetComponent<IsBody>().shape;
        public readonly uint ContactCount => transform.AsEntity().GetArrayLength<CollisionContact>();
        public readonly CollisionContact this[uint index] => transform.AsEntity().GetArrayElement<CollisionContact>(index);

        public readonly USpan<CollisionContact> Contacts
        {
            get
            {
                if (transform.AsEntity().TryGetArray(out USpan<CollisionContact> contacts))
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
                WorldBounds bounds = transform.AsEntity().GetComponent<WorldBounds>();
                return (bounds.min, bounds.max);
            }
        }

        readonly uint IEntity.Value => transform.GetEntityValue();
        readonly World IEntity.World => transform.GetWorld();
        readonly Definition IEntity.Definition => new Definition().AddComponentType<IsBody>();

#if NET
        [Obsolete("Default constructor not available", true)]
        public Body()
        {
            throw new NotSupportedException();
        }
#endif

        /// <summary>
        /// Creates a physics body with default mass and gravity scale.
        /// </summary>
        public Body(World world, Shape shape, IsBody.Type type, Vector3 initialVelocity = default)
        {
            transform = new(world);
            transform.AddComponent(new IsBody(shape, type));
            if (type != IsBody.Type.Static)
            {
                transform.AddComponent(new LinearVelocity(initialVelocity));
                transform.AddComponent(new AngularVelocity());
                transform.AddComponent(Components.GravityScale.Default);
                transform.AddComponent(Components.Mass.Default);
            }
        }

        public readonly void Dispose()
        {
            transform.Dispose();
        }

        [Conditional("DEBUG")]
        private readonly void ThrowIfBodyIsStatic()
        {
            if (transform.AsEntity().GetComponent<IsBody>().type == IsBody.Type.Static)
            {
                throw new InvalidOperationException($"Body `{transform}` is static");
            }
        }

        public static implicit operator Transform(Body body)
        {
            return body.transform;
        }

        public static implicit operator Entity(Body body)
        {
            return body.transform;
        }
    }
}