using Physics.Components;
using Simulation;
using System;
using System.Numerics;
using Unmanaged;

namespace Physics
{
    public readonly struct SphereShape : IEntity
    {
        private readonly Shape shape;

        public readonly ref float Radius
        {
            get
            {
                Entity entity = shape;
                return ref entity.GetComponent<IsSphereShape>().radius;
            }
        }

        public readonly ref Vector3 Offset
        {
            get
            {
                Entity entity = shape;
                return ref entity.GetComponent<IsShape>().offset;
            }
        }

        eint IEntity.Value => (Entity)shape;
        World IEntity.World => (Entity)shape;

#if NET
        [Obsolete("Default constructor not available", true)]
        public SphereShape()
        {
            throw new NotSupportedException();
        }
#endif

        public SphereShape(World world, float radius, Vector3 offset = default)
        {
            Entity entity = new(world);
            entity.AddComponent(new IsShape(offset));
            entity.AddComponent(new IsSphereShape(radius));
            shape = entity.As<Shape>();
        }

        Query IEntity.GetQuery(World world)
        {
            return new(world, RuntimeType.Get<IsSphereShape>());
        }

        public static implicit operator Shape(SphereShape sphere)
        {
            return sphere.shape;
        }

        public static implicit operator Entity(SphereShape sphere)
        {
            return sphere.shape;
        }
    }
}