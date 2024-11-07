using Physics.Functions;
using Simulation;
using System;
using System.Numerics;

namespace Physics.Events
{
    public readonly struct RaycastRequest
    {
        public readonly World world;
        public readonly Vector3 origin;
        public readonly Vector3 direction;
        public readonly RaycastHitCallback callback;
        public readonly float distance;
        public readonly ulong identifier;

#if NET
        [Obsolete("Default constructor not available", true)]
        public RaycastRequest()
        {
            throw new NotSupportedException();
        }
#endif

        public RaycastRequest(World world, Vector3 origin, Vector3 direction, RaycastHitCallback callback, float distance = 1000f, ulong identifier = default)
        {
            this.world = world;
            this.origin = origin;
            this.direction = direction;
            this.callback = callback;
            this.distance = distance;
            this.identifier = identifier;
        }
    }
}