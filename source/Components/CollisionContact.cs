using System.Numerics;
using Worlds;

namespace Physics.Components
{
    [ArrayElement]
    public struct CollisionContact
    {
        public Vector3 point;
        public Vector3 normal;
        public uint otherEntity;

        public CollisionContact(Vector3 point, Vector3 normal, uint otherEntity)
        {
            this.point = point;
            this.normal = normal;
            this.otherEntity = otherEntity;
        }
    }
}