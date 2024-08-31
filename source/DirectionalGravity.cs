using Physics.Components;
using Simulation;
using System;
using System.Numerics;
using Transforms;
using Transforms.Components;
using Unmanaged;

namespace Physics
{
    public readonly struct DirectionalGravity : IEntity
    {
        private readonly GravitySource gravity;

        public readonly ref float Force => ref ((Entity)gravity).GetComponentRef<IsGravitySource>().force;

        uint IEntity.Value => (Entity)gravity;
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

        public DirectionalGravity(World world, Vector3 direction, float force = 9.8067f)
        {
            Entity entity = new(world);
            entity.AddComponent(new IsGravitySource(force));
            entity.AddComponent(new IsDirectionalGravity());
            entity.AddComponent(new IsTransform());
            entity.AddComponent(Rotation.FromDirection(direction));
            gravity = entity.As<GravitySource>();
        }

        readonly Query IEntity.GetQuery(World world)
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