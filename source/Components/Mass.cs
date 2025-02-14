namespace Physics.Components
{
    public struct Mass
    {
        public static readonly Mass Default = new(1f);

        public float value;

        public Mass(float value)
        {
            this.value = value;
        }
    }
}