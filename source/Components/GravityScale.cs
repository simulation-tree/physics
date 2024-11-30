using Worlds;

namespace Physics.Components
{
    [Component]
    public struct GravityScale
    {
        public static readonly GravityScale Default = new(1f);

        public float value;

        public GravityScale(float value)
        {
            this.value = value;
        }
    }
}