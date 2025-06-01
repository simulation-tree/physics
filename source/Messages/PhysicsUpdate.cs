namespace Physics.Messages
{
    public readonly struct PhysicsUpdate
    {
        public readonly double deltaTime;

        public PhysicsUpdate(double deltaTime)
        {
            this.deltaTime = deltaTime;
        }
    }
}