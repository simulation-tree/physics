using System.Numerics;

namespace Physics.Components
{
    public struct LinearVelocity
    {
        public Vector3 value;

        public LinearVelocity(Vector3 value)
        {
            this.value = value;
        }
    }

    public struct AngularVelocity
    {
        public Vector3 value;

        public AngularVelocity(Vector3 value)
        {
            this.value = value;
        }
    }
}