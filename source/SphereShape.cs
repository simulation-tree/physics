namespace Physics
{
    public readonly struct SphereShape : IShape
    {
        public readonly float radius;

        byte IShape.TypeIndex => 1;

        public SphereShape(float radius)
        {
            this.radius = radius;
        }

        public static implicit operator Shape(SphereShape shape)
        {
            return Shape.Create(shape);
        }
    }
}