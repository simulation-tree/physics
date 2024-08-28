using Physics.Components;
using Simulation;
using System;
using System.Numerics;
using Transforms;
using Transforms.Components;
using Unmanaged;

namespace Physics
{
    public readonly struct Body : IEntity
    {
        private readonly Entity entity;

        public readonly ref Vector3 LinearVelocity
        {
            get
            {
                return ref entity.GetComponent<LinearVelocity>().value;
            }
        }

        public readonly ref Vector3 AngularVelocity
        {
            get
            {
                return ref entity.GetComponent<AngularVelocity>().value;
            }
        }

        public readonly ref float GravityScale
        {
            get
            {
                return ref entity.GetComponent<GravityScale>().value;
            }
        }

        public readonly ref float Mass
        {
            get
            {
                return ref entity.GetComponent<Mass>().value;
            }
        }

        public readonly uint ContactCount
        {
            get
            {
                return entity.GetArrayLength<CollisionContact>();
            }
        }

        public readonly CollisionContact this[uint index]
        {
            get
            {
                return entity.GetArrayElement<CollisionContact>(index);
            }
        }

        public readonly ReadOnlySpan<CollisionContact> Contacts
        {
            get
            {
                if (entity.ContainsArray<CollisionContact>())
                {
                    return entity.GetArray<CollisionContact>();
                }
                else
                {
                    return Array.Empty<CollisionContact>();
                }
            }
        }

        public readonly Shape Shape
        {
            get
            {
                rint shapeReference = entity.GetComponent<IsBody>().shapeReference;
                return entity.GetReference<Shape>(shapeReference);
            }
        }

        public readonly (Vector3 min, Vector3 max) Bounds
        {
            get
            {
                WorldBounds bounds = entity.GetComponent<WorldBounds>();
                return (bounds.min, bounds.max);
            }
        }

        eint IEntity.Value => entity;
        World IEntity.World => entity;

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
            entity = new(world);
            rint shapeReference = entity.AddReference(shape);
            entity.AddComponent(new IsBody(shapeReference, type));
            entity.AddComponent(new LinearVelocity(initialVelocity));
            entity.AddComponent(new AngularVelocity());
            entity.AddComponent(Components.GravityScale.Default);
            entity.AddComponent(Components.Mass.Default);
            entity.AddComponent(new Position());
            entity.AddComponent(new IsTransform());
        }

        Query IEntity.GetQuery(World world)
        {
            return new(world, RuntimeType.Get<IsBody>());
        }

        public static implicit operator Entity(Body collider)
        {
            return collider.entity;
        }

        public static implicit operator Transform(Body collider)
        {
            return collider.entity.As<Transform>();
        }
    }
}