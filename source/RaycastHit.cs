using System.Numerics;
using Unmanaged;

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
            USpan<char> buffer = stackalloc char[96];
            uint length = ToString(buffer);
            return buffer.GetSpan(length).ToString();
        }

        public readonly uint ToString(USpan<char> buffer)
        {
            uint length = 0;
            buffer[length++] = 'R';
            buffer[length++] = 'a';
            buffer[length++] = 'y';
            buffer[length++] = 'c';
            buffer[length++] = 'a';
            buffer[length++] = 's';
            buffer[length++] = 't';
            buffer[length++] = 'H';
            buffer[length++] = 'i';
            buffer[length++] = 't';
            buffer[length++] = '(';
            length += point.X.ToString(buffer.Slice(length));
            buffer[length++] = ',';
            length += point.Y.ToString(buffer.Slice(length));
            buffer[length++] = ',';
            length += point.Z.ToString(buffer.Slice(length));
            buffer[length++] = ',';
            length += normal.X.ToString(buffer.Slice(length));
            buffer[length++] = ',';
            length += normal.Y.ToString(buffer.Slice(length));
            buffer[length++] = ',';
            length += normal.Z.ToString(buffer.Slice(length));
            buffer[length++] = ',';
            length += distance.ToString(buffer.Slice(length));
            buffer[length++] = ',';
            length += entity.ToString(buffer.Slice(length));
            buffer[length++] = ')';
            return length;
        }
    }
}