using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Physics
{
    public unsafe struct Shape
    {
        public Vector3 offset;
        public byte type;

        private fixed float data[5];

        private Shape(byte type, float* data, Vector3 offset)
        {
            this.type = type;
            this.offset = offset;
            for (int i = 0; i < 5; i++)
            {
                this.data[i] = data[i];
            }
        }

        public readonly bool Is<T>(out T shape) where T : unmanaged, IShape
        {
            if (type == default(T).TypeIndex)
            {
                fixed (float* data = this.data)
                {
                    shape = Unsafe.Read<T>(data);
                    return true;
                }
            }
            else
            {
                shape = default;
                return false;
            }
        }

        public static Shape Create<T>(T shape, Vector3 offset = default) where T : unmanaged, IShape
        {
            ThrowIfSizeIsTooGreat<T>();
            void* shapePointer = Unsafe.AsPointer(ref shape);
            return new Shape(shape.TypeIndex, (float*)shapePointer, offset);
        }

        [Conditional("DEBUG")]
        private static void ThrowIfSizeIsTooGreat<T>()
        {
            int maxSize = sizeof(float) * 5;
            int size = Unsafe.SizeOf<T>();
            if (size > maxSize)
            {
                throw new ArgumentException($"The size of {typeof(T).Name} is too great. The maximum amount of floats that can be stored is 5.");
            }
        }
    }
}