using Worlds;

namespace Physics.Components
{
    [Component]
    public struct MeshShape
    {
        public rint meshReference;

        public MeshShape(rint meshReference)
        {
            this.meshReference = meshReference;
        }
    }
}