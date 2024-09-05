using Physics.Components;
using Simulation;
using System;
using Transforms;
using Unmanaged;

namespace Physics
{
    public readonly struct GravitySource : IEntity
    {
        public readonly Transform transform;

        public readonly bool IsDirectional => transform.entity.ContainsComponent<IsDirectionalGravity>();
        public readonly bool IsPoint => transform.entity.ContainsComponent<IsPointGravity>();
        public readonly ref float Force => ref transform.entity.GetComponentRef<IsGravitySource>().force;

        readonly uint IEntity.Value => transform.entity.value;
        readonly World IEntity.World => transform.entity.world;
        readonly Definition IEntity.Definition => new([RuntimeType.Get<IsGravitySource>()], []);

#if NET
        [Obsolete("Default constructor not available", true)]
        public GravitySource()
        {
            throw new NotSupportedException();
        }
#endif

        public GravitySource(World world, float force = 9.8067f)
        {
            transform = new(world);
            transform.entity.AddComponent(new IsGravitySource(force));
        }
    }
}