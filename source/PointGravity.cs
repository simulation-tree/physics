using Physics.Components;
using Simulation;
using System;
using System.Numerics;
using Transforms;
using Transforms.Components;
using Unmanaged;

namespace Physics
{
    public readonly struct PointGravity : IEntity
    {
        private readonly GravitySource gravity;

        public readonly ref float Force => ref ((Entity)gravity).GetComponentRef<IsGravitySource>().force;
        public readonly ref float Radius => ref ((Entity)gravity).GetComponentRef<IsPointGravity>().radius;

        uint IEntity.Value => (Entity)gravity;
        World IEntity.World => (Entity)gravity;

#if NET
        [Obsolete("Default constructor not available", true)]
        public PointGravity()
        {
            throw new NotSupportedException();
        }
#endif

        public PointGravity(World world, Vector3 position, float radius, float force = 9.8067f)
        {
            Entity entity = new(world);
            entity.AddComponent(new IsGravitySource(force));
            entity.AddComponent(new IsPointGravity(radius));
            entity.AddComponent(new IsTransform());
            entity.AddComponent(new Position(position));
            gravity = entity.As<GravitySource>();
        }

        readonly Query IEntity.GetQuery(World world)
        {
            return new(world, RuntimeType.Get<IsPointGravity>());
        }

        public static implicit operator GravitySource(PointGravity gravity)
        {
            return gravity.gravity;
        }

        public static implicit operator Entity(PointGravity gravity)
        {
            return gravity.gravity;
        }

        public static implicit operator Transform(PointGravity gravity)
        {
            return gravity.gravity;
        }
    }
}