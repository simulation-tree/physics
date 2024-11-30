using Worlds;

namespace Physics.Components
{
    [Component]
    public struct IsGravitySource
    {
        public float force;

        public IsGravitySource(float force)
        {
            this.force = force;
        }
    }
}