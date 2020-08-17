using System;

namespace Yellow.Core.Utils
{
    public class Vec2 : IEquatable<Vec2>
    {
        public float x;

        public float y;

        public static readonly Vec2 Zero = new Vec2(0, 0);

        public static readonly Vec2 Up = new Vec2(0, -1);

        public static readonly Vec2 Down = new Vec2(0, 1);

        public static readonly Vec2 Left = new Vec2(-1, 0);

        public static readonly Vec2 Right = new Vec2(1, 0);

        public Vec2(float x, float y)
        {
            this.x = x;
            this.y = y;
            Console.WriteLine("new");
        }

        public Vec2(Vec2 other)
        {
            x = other.x;
            y = other.y;
        }

        public Vec2 Clone()
        {
            return new Vec2(x, y);
        }

        public Vec2 CopyTo(Vec2 destination)
        {
            destination.x = x;
            destination.y = y;

            return this;
        }

        public Vec2 CopyFrom(Vec2 source)
        {
            x = source.x;
            y = source.y;

            return this;
        }

        public Vec2 Set(float x, float y)
        {
            this.x = x;
            this.y = y;

            return this;
        }

        public Vec2 Set(float xy)
        {
            x = y = xy;

            return this;
        }

        public bool Equals(Vec2 other)
        {
            return x == other.x && y == other.y;
        }

        public bool Equals(Vec2 other, float eps)
        {
            return MathF.Abs(x - other.x) <= eps && Math.Abs(y - other.y) <= eps;
        }

        public override string ToString()
        {
            return x + " " + y;
        }

        public override bool Equals(object obj)
        {
            var vec = obj as Vec2;

            if (vec == null)
            {
                return false;
            }
            else
            {
                return x == vec.x && y == vec.y;
            }
        }

        public override int GetHashCode()
        {
            return (int) (x * y + x + y);
        }

        public float Length
        {
            get
            {
                return MathF.Sqrt(x * x + y * y);
            }

            set
            {
                Normalize().Multiply(value);
            }
        }

        public float LengthSquared
        {
            get
            {
                return x * x + y * y;
            }
        }

        public float ManhattanLength
        {
            get
            {
                return MathF.Abs(x) + MathF.Abs(y);
            }
        }

        public bool IsZero
        {
            get
            {
                return x == 0.0f && y == 0.0f;
            }
        }

        public float Dot(Vec2 other)
        {
            return x * other.x + y * other.y;
        }

        public float Cross(Vec2 other)
        {
            return x * other.y - y * other.x;
        }

        public Vec2 Normalize()
        {
            var length = Length;

            if (length == 0.0f)
            {
                CopyFrom(Vec2.Right);
            }
            else
            {
                Multiply(1.0f / length);
            }

            return this;
        }

        public Vec2 Normalized
        {
            get
            {
                var length = Length;

                if (length == 0.0f)
                {
                    return Clone();
                }
                else
                {
                    return this / length;
                }
            }
        }

        public Vec2 Invert()
        {
            if (x != 0.0f)
            {
                x = 1.0f / x;
            }

            if (y != 0.0f)
            {
                y = 1.0f / y;
            }

            return this;
        }

        public Vec2 Inverted
        {
            get
            {
                return Clone().Invert();
            }
        }

        public float Angle
        {
            get
            {
                return MathF.Atan2(-y, -x) + MathF.PI;
            }
        }

        public float AngleDeg
        {
            get
            {
                return MathF.Atan2(-y, -x) * Math2.Rad2Deg + 180.0f;
            }
        }

        public Vec2 Multiply(float scalar)
        {
            x *= scalar;
            y *= scalar;

            return this;
        }

        public Vec2 Multiply(Vec2 other)
        {
            x *= other.x;
            y *= other.y;

            return this;
        }

        public Vec2 Multiply(float x, float y)
        {
            this.x *= x;
            this.y *= y;

            return this;
        }

        public Vec2 MultiplyVectors(Vec2 a, Vec2 b)
        {
            x *= a.x * b.x;
            y *= a.y * b.y;

            return this;
        }

        public Vec2 Divide(float scalar)
        {
            var invertedScalar = 1.0f / scalar;

            x *= invertedScalar;
            y *= invertedScalar;

            return this;
        }

        public Vec2 Divide(float x, float y)
        {
            this.x /= x;
            this.y /= y;

            return this;
        }

        public Vec2 Divide(Vec2 other)
        {
            x /= other.x;
            y /= other.y;

            return this;
        }

        public Vec2 Add(Vec2 other)
        {
            x += other.x;
            y += other.y;

            return this;
        }

        public Vec2 AddVectors(Vec2 a, Vec2 b)
        {
            x = a.x + b.x;
            y = a.y + b.y;

            return this;
        }

        public Vec2 Subtract(Vec2 other)
        {
            x -= other.x;
            y -= other.y;

            return this;
        }

        public Vec2 SubtractVectors(Vec2 a, Vec2 b)
        {
            x = a.x - b.x;
            y = a.y - b.y;

            return this;
        }

        public Vec2 Negate()
        {
            x = -x;
            y = -y;

            return this;
        }

        public Vec2 NegateVector(Vec2 other)
        {
            x = -other.x;
            y = -other.y;

            return this;
        }

        public Vec2 Floor()
        {
            x = MathF.Floor(x);
            y = MathF.Floor(y);

            return this;
        }

        public Vec2 Ceiling()
        {
            x = MathF.Ceiling(x);
            y = MathF.Ceiling(y);

            return this;
        }

        public Vec2 Round()
        {
            x = MathF.Round(x);
            y = MathF.Round(y);

            return this;
        }

        public float Distance(Vec2 other)
        {
            var dX = other.x - x;
            var dY = other.y - y;

            return MathF.Sqrt(dX * dX + dY * dY);
        }

        public float DistanceSquared(Vec2 other)
        {
            var dX = other.x - x;
            var dY = other.y - y;

            return dX * dX + dY * dY;
        }

        public float ManhattanDistance(Vec2 other)
        {
            return MathF.Abs(other.x - x) + MathF.Abs(other.y - y);
        }

        public Vec2 RotateAround(Vec2 center, float angle)
        {
            var sin = MathF.Sin(angle);
            var cos = MathF.Cos(angle);

            var dirX = x - center.x;
            var dirY = y - center.y;

            x = dirX * cos - dirY * sin + center.x;
            y = dirX * sin + dirY * cos + center.y;

            return this;
        }

        public Vec2 RotateAroundThis(Vec2 point, float angle)
        {
            var sin = MathF.Sin(angle);
            var cos = MathF.Cos(angle);

            var dirX = point.x - x;
            var dirY = point.y - y;

            point.x = dirX * cos - dirY * sin + x;
            point.y = dirX * sin + dirY * cos + y;

            return this;
        }

        public Vec2 Lerp(Vec2 to, float time)
        {
            x += (to.x - x) * time;
            y += (to.y - y) * time;

            return this;
        }

        public Vec2 LerpVectors(Vec2 from, Vec2 to, float time)
        {
            x = from.x + (to.x - from.x) * time;
            y = from.y + (to.y - from.y) * time;

            return this;
        }

        public Vec2 Clamp(Vec2 min, Vec2 max)
        {
            x = MathF.Max(min.x, MathF.Min(max.x, x));
            y = MathF.Max(min.y, MathF.Min(max.y, y));

            return this;
        }

        public Vec2 Clamp(float min, float max)
        {
            x = MathF.Max(min, MathF.Min(max, x));
            y = MathF.Max(min, MathF.Min(max, y));

            return this;
        }

        public Vec2 ClampLength(float min, float max)
        {
            var currentLength = Length;

            Normalize().Multiply(MathF.Max(min, MathF.Min(max, currentLength)));

            return this;
        }

        public static Vec2 Longer(Vec2 a, Vec2 b)
        {
            if (b.LengthSquared > a.LengthSquared)
            {
                return b;
            }

            return a;
        }

        public static Vec2 Shorter(Vec2 a, Vec2 b)
        {
            if (b.LengthSquared < a.LengthSquared)
            {
                return b;
            }

            return a;
        }

        public static bool operator ==(Vec2 a, Vec2 b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Vec2 a, Vec2 b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x + b.x, a.y + b.y);
        }

        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x - b.x, a.y - b.y);
        }

        public static Vec2 operator -(Vec2 vec)
        {
            return new Vec2(-vec.x, -vec.y);
        }

        public static Vec2 operator *(Vec2 vec, float scalar)
        {
            return new Vec2(vec.x * scalar, vec.y * scalar);
        }

        public static Vec2 operator *(float scalar, Vec2 vec)
        {
            return new Vec2(vec.x * scalar, vec.y * scalar);
        }

        public static Vec2 operator /(Vec2 vec, float scalar)
        {
            var invertedScalar = 1.0f / scalar;

            return new Vec2(vec.x * invertedScalar, vec.y * invertedScalar);
        }

        public static Vec2 operator *(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x * b.x, a.y * b.y);
        }
    }
}
