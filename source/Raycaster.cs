using Physics.Components;
using Simulation;
using System;
using Unmanaged;

namespace Physics
{
    public readonly struct Raycaster : IEntity
    {
        private readonly Entity entity;

        public readonly ref float MaxDistance
        {
            get
            {
                return ref entity.GetComponent<IsRaycaster>().maxDistance;
            }
        }

        public readonly uint HitCount
        {
            get
            {
                return entity.GetArrayLength<RaycastHit>();
            }
        }

        public readonly RaycastHit this[uint index]
        {
            get
            {
                return entity.GetArrayElement<RaycastHit>(index);
            }
        }

        public readonly ReadOnlySpan<RaycastHit> Hits
        {
            get
            {
                if (entity.ContainsArray<RaycastHit>())
                {
                    return entity.GetArray<RaycastHit>();
                }
                else
                {
                    return Array.Empty<RaycastHit>();
                }
            }
        }

        eint IEntity.Value => entity;
        World IEntity.World => entity;

#if NET
        [Obsolete("Default constructor not available", true)]
        public Raycaster()
        {
            throw new NotSupportedException();
        }
#endif

        public Raycaster(World world, float maxDistance)
        {
            entity = new(world);
            entity.AddComponent(new IsRaycaster(maxDistance));
        }

        Query IEntity.GetQuery(World world)
        {
            return new(world, RuntimeType.Get<IsRaycaster>());
        }

        public static implicit operator Entity(Raycaster raycaster)
        {
            return raycaster.entity;
        }
    }
}