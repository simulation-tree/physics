using Simulation;
using System;

namespace Physics.Components
{
    public unsafe readonly struct RaycastHitCallback : IEquatable<RaycastHitCallback>
    {
#if NET
        private readonly delegate* unmanaged<World, eint, RaycastHit, TimeSpan, void> callback;

        public RaycastHitCallback(delegate* unmanaged<World, eint, RaycastHit, TimeSpan, void> callback)
        {
            this.callback = callback;
        }
#else
        private readonly delegate*<World, eint, RaycastHit, TimeSpan, void> callback;

        public RaycastHitCallback(delegate*<World, eint, RaycastHit, TimeSpan, void> callback)
        {
            this.callback = callback;
        }
#endif

        public readonly void Invoke(World world, eint raycaster, RaycastHit hit, TimeSpan delta)
        {
            callback(world, raycaster, hit, delta);
        }

        public readonly override bool Equals(object? obj)
        {
            return obj is RaycastHitCallback callback && Equals(callback);
        }

        public readonly bool Equals(RaycastHitCallback other)
        {
            return (nint)callback == (nint)other.callback;
        }

        public readonly override int GetHashCode()
        {
            return ((nint)callback).GetHashCode();
        }

        public static bool operator ==(RaycastHitCallback left, RaycastHitCallback right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RaycastHitCallback left, RaycastHitCallback right)
        {
            return !(left == right);
        }
    }
}