using System;
using System.Numerics;

namespace Physics
{
    public struct RaycastHit
    {
        public Vector3 point;
        public Vector3 normal;
        public float distance;
        public uint entity;

        public RaycastHit(Vector3 point, Vector3 normal, float distance, uint entity)
        {
            this.point = point;
            this.normal = normal;
            this.distance = distance;
            this.entity = entity;
        }

        public unsafe readonly override string ToString()
        {
            Span<char> buffer = stackalloc char[96];
            int length = ToString(buffer);
            return buffer.Slice(0, length).ToString();
        }

        public readonly int ToString(Span<char> destination)
        {
            int length = 0;
            destination[length++] = 'R';
            destination[length++] = 'a';
            destination[length++] = 'y';
            destination[length++] = 'c';
            destination[length++] = 'a';
            destination[length++] = 's';
            destination[length++] = 't';
            destination[length++] = 'H';
            destination[length++] = 'i';
            destination[length++] = 't';
            destination[length++] = '(';
            length += point.X.ToString(destination.Slice(length));
            destination[length++] = ',';
            length += point.Y.ToString(destination.Slice(length));
            destination[length++] = ',';
            length += point.Z.ToString(destination.Slice(length));
            destination[length++] = ',';
            length += normal.X.ToString(destination.Slice(length));
            destination[length++] = ',';
            length += normal.Y.ToString(destination.Slice(length));
            destination[length++] = ',';
            length += normal.Z.ToString(destination.Slice(length));
            destination[length++] = ',';
            length += distance.ToString(destination.Slice(length));
            destination[length++] = ',';
            length += entity.ToString(destination.Slice(length));
            destination[length++] = ')';
            return length;
        }
    }
}