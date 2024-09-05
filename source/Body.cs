using Physics.Components;
using Simulation;
using System;
using System.Diagnostics;
using System.Numerics;
using Transforms;
using Unmanaged;

namespace Physics
{
    public readonly struct Body : IEntity
    {
        public readonly Transform transform;

        public readonly ref Vector3 LinearVelocity
        {
            get
            {
                ThrowIfBodyIsStatic();
                return ref transform.entity.GetComponentRef<LinearVelocity>().value;
            }
        }

        public readonly ref Vector3 AngularVelocity
        {
            get
            {
                ThrowIfBodyIsStatic();
                return ref transform.entity.GetComponentRef<AngularVelocity>().value;
            }
        }

        public readonly ref float GravityScale
        {
            get
            {
                ThrowIfBodyIsStatic();
                return ref transform.entity.GetComponentRef<GravityScale>().value;
            }
        }

        public readonly ref float Mass
        {
            get
            {
                ThrowIfBodyIsStatic();
                return ref transform.entity.GetComponentRef<Mass>().value;
            }
        }

        public readonly ref Shape Shape => ref transform.entity.GetComponentRef<IsBody>().shape;

        public readonly uint ContactCount => transform.entity.GetArrayLength<CollisionContact>();
        public readonly CollisionContact this[uint index] => transform.entity.GetArrayElementRef<CollisionContact>(index);

        public readonly USpan<CollisionContact> Contacts
        {
            get
            {
                if (transform.entity.ContainsArray<CollisionContact>())
                {
                    return transform.entity.GetArray<CollisionContact>();
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
                WorldBounds bounds = transform.entity.GetComponentRef<WorldBounds>();
                return (bounds.min, bounds.max);
            }
        }

        readonly uint IEntity.Value => transform.entity.value;
        readonly World IEntity.World => transform.entity.world;
        readonly Definition IEntity.Definition => new([RuntimeType.Get<IsBody>()], []);

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
            transform.entity.AddComponent(new IsBody(shape, type));
            if (type != IsBody.Type.Static)
            {
                transform.entity.AddComponent(new LinearVelocity(initialVelocity));
                transform.entity.AddComponent(new AngularVelocity());
                transform.entity.AddComponent(Components.GravityScale.Default);
                transform.entity.AddComponent(Components.Mass.Default);
            }
        }

        [Conditional("DEBUG")]
        private readonly void ThrowIfBodyIsStatic()
        {
            if (transform.entity.GetComponent<IsBody>().type == IsBody.Type.Static)
            {
                throw new InvalidOperationException($"Body `{transform.entity}` is static");
            }
        }
    }
}