using Physics.Components;
using Simulation;
using System;
using System.Numerics;
using Unmanaged;

namespace Physics
{
    public readonly struct Shape : IEntity
    {
        private readonly Entity entity;

        public readonly ref Vector3 Offset
        {
            get
            {
                return ref entity.GetComponent<IsShape>().offset;
            }
        }

        eint IEntity.Value => entity;
        World IEntity.World => entity;

#if NET
        [Obsolete("Default constructor not available", true)]
        public Shape()
        {
            throw new NotSupportedException();
        }
#endif

        public Shape(World world, CubeShape cube, Vector3 offset = default)
        {
            entity = new(world);
            entity.AddComponent(cube);
            entity.AddComponent(new IsShape(offset));
        }

        public Shape(World world, SphereShape sphere, Vector3 offset = default)
        {
            entity = new(world);
            entity.AddComponent(sphere);
            entity.AddComponent(new IsShape(offset));
        }

        Query IEntity.GetQuery(World world)
        {
            return new(world, RuntimeType.Get<IsShape>());
        }

        public static implicit operator Entity(Shape shape)
        {
            return shape.entity;
        }
    }
}