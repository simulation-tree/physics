using System.Numerics;
using Worlds;

namespace Physics.Components
{
    [Component]
    public struct AngularVelocity
    {
        public Vector3 value;

        public AngularVelocity(Vector3 value)
        {
            this.value = value;
        }
    }
}