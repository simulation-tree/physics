using Simulation;
using System.Numerics;

namespace Physics.Components
{
    public struct RaycastHit
    {
        public Vector3 point;
        public Vector3 normal;
        public float distance;
        public eint targetEntity;

        public RaycastHit(Vector3 point, Vector3 normal, float distance, eint targetEntity)
        {
            this.point = point;
            this.normal = normal;
            this.distance = distance;
            this.targetEntity = targetEntity;
        }
    }
}