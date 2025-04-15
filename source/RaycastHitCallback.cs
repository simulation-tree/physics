using Physics.Events;
using System;
using Worlds;

namespace Physics.Functions
{
    public unsafe readonly struct RaycastHitCallback : IEquatable<RaycastHitCallback>
    {
#if NET
        private readonly delegate* unmanaged<Input, void> callback;

        public RaycastHitCallback(delegate* unmanaged<Input, void> callback)
        {
            this.callback = callback;
        }
#else
        private readonly delegate*<Input, void> callback;

        public RaycastHitCallback(delegate*<Input, void> callback)
        {
            this.callback = callback;
        }
#endif

        public readonly void Invoke(World world, RaycastRequest raycast, Span<RaycastHit> hits)
        {
            callback(new(world, raycast, hits));
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

        public readonly struct Input
        {
            public readonly World world;
            public readonly RaycastRequest request;

            private readonly RaycastHit* pointer;
            private readonly int length;

            public readonly bool Any => length > 0;
            public readonly ReadOnlySpan<RaycastHit> Hits => new(pointer, length);

            public Input(World world, RaycastRequest raycast, Span<RaycastHit> hits)
            {
                this.world = world;
                this.request = raycast;
                pointer = hits.GetPointer();
                length = hits.Length;
            }
        }
    }
}