using Physics.Components;
using Simulation;
using System;
using System.Numerics;
using Transforms;
using Transforms.Components;
using Unmanaged;

namespace Physics
{
    public readonly struct Raycaster : IEntity
    {
        private readonly Entity entity;

        public readonly bool IsEnabled
        {
            get => entity.IsEnabled;
            set => entity.IsEnabled = value;
        }

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

        public readonly ref RaycastHitCallback Callback
        {
            get
            {
                ref IsRaycaster component = ref entity.GetComponent<IsRaycaster>();
                return ref component.callback;
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

        public Raycaster(World world, eint existingEntity)
        {
            entity = new(world, existingEntity);
        }

        public Raycaster(World world, Vector3 position, Quaternion rotation, float maxDistance = 1000f)
        {
            entity = new(world);
            entity.AddComponent(new IsRaycaster(maxDistance, default));
            entity.AddComponent(new IsTransform());
            entity.AddComponent(new Position(position));
            entity.AddComponent(new Rotation(rotation));
        }

        Query IEntity.GetQuery(World world)
        {
            return new(world, RuntimeType.Get<IsRaycaster>());
        }

        public static implicit operator Entity(Raycaster raycaster)
        {
            return raycaster.entity;
        }

        public static implicit operator Transform(Raycaster raycaster)
        {
            return raycaster.entity.As<Transform>();
        }
    }
}