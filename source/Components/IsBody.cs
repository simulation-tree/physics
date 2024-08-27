using Simulation;

namespace Physics.Components
{
    public struct IsBody
    {
        public uint version;
        public rint shapeReference;
        public Type type;

        public IsBody(rint shapeReference, Type type)
        {
            version = default;
            this.shapeReference = shapeReference;
            this.type = type;
        }

        public enum Type : byte
        {
            Dynamic,
            Kinematic,
            Static
        }
    }
}