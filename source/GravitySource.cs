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
            get => entity.ContainsComponent<DirectionalGravity>();
        }

        public readonly bool IsPoint
        {
            get => entity.ContainsComponent<PointGravity>();
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

        /// <summary>
        /// Creates a directional gravity source.
        /// </summary>
        public GravitySource(World world, Quaternion rotation, float force = 9.8067f)
        {
            entity = new(world);
            entity.AddComponent(new IsGravitySource(force));
            entity.AddComponent(new DirectionalGravity());
            entity.AddComponent(new Rotation(rotation));
            entity.AddComponent(new IsTransform());
        }

        /// <summary>
        /// Creates a point gravity source.
        /// </summary>
        public GravitySource(World world, Vector3 position, float radius, float force)
        {
            entity = new(world);
            entity.AddComponent(new IsGravitySource(force));
            entity.AddComponent(new PointGravity(radius));
            entity.AddComponent(new Position(position));
            entity.AddComponent(new IsTransform());
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