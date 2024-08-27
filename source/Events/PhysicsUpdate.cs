using System;

namespace Physics.Events
{
    public readonly struct PhysicsUpdate
    {
        public readonly TimeSpan delta;

        [Obsolete("Default constructor not available", true)]
        public PhysicsUpdate()
        {
            throw new NotSupportedException();
        }

        public PhysicsUpdate(TimeSpan delta)
        {
            this.delta = delta;
        }
    }
}