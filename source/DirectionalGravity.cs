using Physics.Components;
using Simulation;
using System;
using System.Numerics;
using Transforms.Components;
using Unmanaged;

namespace Physics
{
    public readonly struct DirectionalGravity : IEntity
    {
        public readonly GravitySource gravity;

        public readonly ref float Force => ref gravity.Force;

        readonly uint IEntity.Value => gravity.transform.entity.value;
        readonly World IEntity.World => gravity.transform.entity.world;
        readonly Definition IEntity.Definition => new([RuntimeType.Get<IsDirectionalGravity>(), RuntimeType.Get<IsGravitySource>()], []);

#if NET
        [Obsolete("Default constructor not available", true)]
        public DirectionalGravity()
        {
            throw new NotSupportedException();
        }
#endif

        public DirectionalGravity(World world, Quaternion rotation, float force = 9.8067f)
        {
            gravity = new(world, force);
            gravity.transform.entity.AddComponent(new IsDirectionalGravity());
            gravity.transform.LocalRotation = rotation;
        }

        public DirectionalGravity(World world, Vector3 direction, float force = 9.8067f)
        {
            gravity = new(world, force);
            gravity.transform.entity.AddComponent(new IsDirectionalGravity());
            gravity.transform.LocalRotation = Rotation.FromDirection(direction).value;
        }
    }
}