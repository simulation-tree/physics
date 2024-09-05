using Physics.Components;
using Simulation;
using System;
using System.Numerics;
using Unmanaged;

namespace Physics
{
    public readonly struct PointGravity : IEntity
    {
        public readonly GravitySource gravity;

        public readonly ref float Force => ref gravity.Force;
        public readonly ref float Radius => ref gravity.transform.entity.GetComponentRef<IsPointGravity>().radius;

        readonly uint IEntity.Value => gravity.transform.entity.value;
        readonly World IEntity.World => gravity.transform.entity.world;
        readonly Definition IEntity.Definition => new([RuntimeType.Get<IsPointGravity>(), RuntimeType.Get<IsGravitySource>()], []);

#if NET
        [Obsolete("Default constructor not available", true)]
        public PointGravity()
        {
            throw new NotSupportedException();
        }
#endif

        public PointGravity(World world, Vector3 position, float radius, float force = 9.8067f)
        {
            gravity = new(world, force);
            gravity.transform.entity.AddComponent(new IsPointGravity(radius));
            gravity.transform.LocalPosition = position;
        }
    }
}