using System;

namespace Physics.Events
{
    public readonly struct PhysicsUpdate
    {
        public readonly TimeSpan delta;

#if NET
        [Obsolete("Default constructor not available", true)]
        public PhysicsUpdate()
        {
            throw new NotSupportedException();
        }
#endif

        public PhysicsUpdate(TimeSpan delta)
        {
            this.delta = delta;
        }
    }
}