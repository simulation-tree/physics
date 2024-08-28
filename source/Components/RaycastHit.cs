using Simulation;
using System;
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

        public readonly override string ToString()
        {
            Span<char> buffer = stackalloc char[96];
            int length = ToString(buffer);
            return new string(buffer[..length]);
        }

        public readonly int ToString(Span<char> buffer)
        {
            int length = 0;
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

            point.X.TryFormat(buffer[length..], out int written);
            length += written;

            buffer[length++] = ',';

            point.Y.TryFormat(buffer[length..], out written);
            length += written;

            buffer[length++] = ',';

            point.Z.TryFormat(buffer[length..], out written);
            length += written;

            buffer[length++] = ',';

            normal.X.TryFormat(buffer[length..], out written);
            length += written;

            buffer[length++] = ',';

            normal.Y.TryFormat(buffer[length..], out written);
            length += written;

            buffer[length++] = ',';

            normal.Z.TryFormat(buffer[length..], out written);
            length += written;

            buffer[length++] = ',';

            distance.TryFormat(buffer[length..], out written);
            length += written;

            buffer[length++] = ',';

            targetEntity.TryFormat(buffer[length..], out written);
            length += written;

            buffer[length++] = ')';
            return length;
        }
    }
}