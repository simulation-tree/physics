using Simulation;
using System.Numerics;

namespace Physics.Components
{
    public struct CollisionContact
    {
        public Vector3 point;
        public Vector3 normal;
        public eint otherEntity;

        public CollisionContact(Vector3 point, Vector3 normal, eint otherEntity)
        {
            this.point = point;
            this.normal = normal;
            this.otherEntity = otherEntity;
        }
    }
}