using System.Numerics;

namespace Physics.Components
{
    public struct AngularVelocity
    {
        public Vector3 value;

        public AngularVelocity(Vector3 value)
        {
            this.value = value;
        }
    }
}