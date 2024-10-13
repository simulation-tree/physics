using Physics.Events;
using Simulation;
using System;
using Unmanaged;

namespace Physics.Functions
{
    public unsafe readonly struct RaycastHitCallback : IEquatable<RaycastHitCallback>
    {
#if NET
        private readonly delegate* unmanaged<World, Raycast, RaycastHit*, uint, void> callback;

        public RaycastHitCallback(delegate* unmanaged<World, Raycast, RaycastHit*, uint, void> callback)
        {
            this.callback = callback;
        }
#else
        private readonly delegate*<World, Raycast, RaycastHit*, uint, void> callback;

        public RaycastHitCallback(delegate*<World, Raycast, RaycastHit*, uint, void> callback)
        {
            this.callback = callback;
        }
#endif

        public readonly void Invoke(World world, Raycast raycast, USpan<RaycastHit> hits)
        {
            callback(world, raycast, (RaycastHit*)hits.Address, hits.Length);
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