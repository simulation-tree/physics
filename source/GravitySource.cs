using Physics.Components;
using Simulation;
using System;
using Transforms;
using Unmanaged;

namespace Physics
{
    public readonly struct GravitySource : IEntity
    {
        private readonly Entity entity;

        public readonly bool IsDirectional => entity.ContainsComponent<IsDirectionalGravity>();
        public readonly bool IsPoint => entity.ContainsComponent<IsPointGravity>();
        public readonly ref float Force => ref entity.GetComponent<IsGravitySource>().force;

        uint IEntity.Value => entity;
        World IEntity.World => entity;

#if NET
        [Obsolete("Default constructor not available", true)]
        public GravitySource()
        {
            throw new NotSupportedException();
        }
#endif

        public GravitySource(World world, uint existingEntity)
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
}