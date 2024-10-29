using System;
using System.Diagnostics;
using System.Numerics;

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
            this.data[0] = data[0];
            this.data[1] = data[1];
            this.data[2] = data[2];
            this.data[3] = data[3];
            this.data[4] = data[4];
        }

        public readonly bool Is<T>(out T shape) where T : unmanaged, IShape
        {
            if (type == default(T).TypeIndex)
            {
                fixed (float* data = this.data)
                {
                    shape = *(T*)data;
                    return true;
                }
            }
            else
            {
                shape = default;
                return false;
            }
        }

        public readonly bool Is<T>() where T : unmanaged, IShape
        {
            return type == default(T).TypeIndex;
        }

        public readonly T Read<T>() where T : unmanaged, IShape
        {
            ThrowIfTypeIsNot<T>();
            fixed (float* data = this.data)
            {
                return *(T*)data;
            }
        }

        public static Shape Create<T>(T shape, Vector3 offset = default) where T : unmanaged, IShape
        {
            ThrowIfSizeIsTooGreat<T>();
            void* shapePointer = &shape;
            return new Shape(shape.TypeIndex, (float*)shapePointer, offset);
        }

        [Conditional("DEBUG")]
        private readonly void ThrowIfTypeIsNot<T>() where T : unmanaged, IShape
        {
            if (type != default(T).TypeIndex)
            {
                throw new InvalidOperationException($"The shape is not of type {typeof(T).Name}");
            }
        }

        [Conditional("DEBUG")]
        private static void ThrowIfSizeIsTooGreat<T>() where T : unmanaged
        {
            int maxSize = sizeof(float) * 5;
            int size = sizeof(T);
            if (size > maxSize)
            {
                throw new ArgumentException($"The size of {typeof(T).Name} is too great. The maximum amount of floats that can be stored is 5");
            }
        }
    }
}