using Physics.Events;
using Simulation;
using System;

namespace Physics.Functions
{
    public unsafe readonly struct RaycastHitCallback : IEquatable<RaycastHitCallback>
    {
#if NET
        private readonly delegate* unmanaged<World, Raycast, void*, int, void> callback;

        public RaycastHitCallback(delegate* unmanaged<World, Raycast, void*, int, void> callback)
        {
            this.callback = callback;
        }
#else
        private readonly delegate*<World, Raycast, void*, int, void> callback;

        public RaycastHitCallback(delegate*<World, Raycast, void*, int, void> callback)
        {
            this.callback = callback;
        }
#endif

        public readonly void Invoke(World world, Raycast raycast, Span<RaycastHit> hits)
        {
            fixed (RaycastHit* hitsPtr = hits)
            {
                callback(world, raycast, hitsPtr, hits.Length);
            }
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