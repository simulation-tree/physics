using Physics.Components;
using Simulation;
using System;
using System.Numerics;
using Transforms;
using Transforms.Components;
using Unmanaged;

namespace Physics
{
    public readonly struct GravitySource : IEntity
    {
        private readonly Entity entity;

        public readonly bool IsDirectional
        {
            get => entity.ContainsComponent<IsDirectionalGravity>();
        }

        public readonly bool IsPoint
        {
            get => entity.ContainsComponent<IsPointGravity>();
        }

        public readonly ref float Force
        {
            get => ref entity.GetComponent<IsGravitySource>().force;
        }

        eint IEntity.Value => entity;
        World IEntity.World => entity;

#if NET
        [Obsolete("Default constructor not available", true)]
        public GravitySource()
        {
            throw new NotSupportedException();
        }
#endif

        public GravitySource(World world, eint existingEntity)
        {
            entity = new(world, existingEntity);
        }

        Query IEntity.GetQuery(World world)
        {
            return new(world, RuntimeType.Get<IsGravitySource>());
        }

        public static implicit operator Entity(GravitySource gravity)
        {
            return gravity.entity;
        }

        public static implicit operator Transform(GravitySource gravity)
        {
            return gravity.entity.As<Transform>();
        }
    }

    public readonly struct DirectionalGravity : IEntity
    {
        private readonly GravitySource gravity;

        eint IEntity.Value => (Entity)gravity;
        World IEntity.World => (Entity)gravity;

#if NET
        [Obsolete("Default constructor not available", true)]
        public DirectionalGravity()
        {
            throw new NotSupportedException();
        }
#endif

        public DirectionalGravity(World world, Quaternion rotation, float force = 9.8067f)
        {
            Entity entity = new(world);
            entity.AddComponent(new IsGravitySource(force));
            entity.AddComponent(new IsDirectionalGravity());
            entity.AddComponent(new IsTransform());
            entity.AddComponent(new Rotation(rotation));
            gravity = entity.As<GravitySource>();
        }

        Query IEntity.GetQuery(World world)
        {
            return new(world, RuntimeType.Get<IsDirectionalGravity>());
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