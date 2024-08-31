using Physics.Components;
using Simulation;
using System;
using System.Numerics;
using Unmanaged;

namespace Physics
{
    public readonly struct CubeShape : IEntity
    {
        private readonly Shape shape;

        public readonly ref Vector3 Extents
        {
            get
            {
                Entity entity = shape;
                return ref entity.GetComponent<IsCubeShape>().extents;
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

        uint IEntity.Value => (Entity)shape;
        World IEntity.World => (Entity)shape;

#if NET
        [Obsolete("Default constructor not available", true)]
        public CubeShape()
        {
            throw new NotSupportedException();
        }
#endif

        public CubeShape(World world, Vector3 extents, Vector3 offset = default)
        {
            Entity entity = new(world);
            entity.AddComponent(new IsShape(offset));
            entity.AddComponent(new IsCubeShape(extents));
            shape = entity.As<Shape>();
        }

        public CubeShape(World world, float extent, Vector3 offset = default)
        {
            Entity entity = new(world);
            entity.AddComponent(new IsShape(offset));
            entity.AddComponent(new IsCubeShape(extent));
            shape = entity.As<Shape>();
        }

        public CubeShape(World world, float x, float y, float z, Vector3 offset = default)
        {
            Entity entity = new(world);
            entity.AddComponent(new IsShape(offset));
            entity.AddComponent(new IsCubeShape(x, y, z));
            shape = entity.As<Shape>();
        }

        Query IEntity.GetQuery(World world)
        {
            return new(world, RuntimeType.Get<IsCubeShape>());
        }

        public static implicit operator Shape(CubeShape cube)
        {
            return cube.shape;
        }

        public static implicit operator Entity(CubeShape cube)
        {
            return cube.shape;
        }
    }
}