namespace Physics.Components
{
    public struct IsRaycaster
    {
        public float maxDistance;
        public RaycastHitCallback callback;

        public IsRaycaster(float maxDistance, RaycastHitCallback callback)
        {
            this.maxDistance = maxDistance;
            this.callback = callback;
        }
    }
}