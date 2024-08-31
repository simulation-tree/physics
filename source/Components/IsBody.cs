namespace Physics.Components
{
    public struct IsBody
    {
        public uint version;
        public Shape shape;
        public Type type;

        public IsBody(Shape shape, Type type)
        {
            version = default;
            this.shape = shape;
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