using SFML.System;
using System;

namespace Yellow.Core.Utils
{
    public struct Vec2 : IEquatable<Vec2>
    {
        public float x;

        public float y;

        public Vec2(float x = 0, float y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public Vec2(float xy)
        {
            x = y = xy;
        }

        public Vec2(Vec2 other)
        {
            x = other.x;
            y = other.y;
        }

        public Vec2 Normalized
        {
            get
            {
                var length = Length;

                if (length == 0.0f)
                {
                    return Right;
                }
                else
                {
                    return this / length;
                }
            }
        }

        public float Length
        {
            get
            {
                return MathF.Sqrt(x * x + y * y);
            }

            set
            {
                Normalize();
                Multiply(value);
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

        public Vec2 Inverted
        {
            get
            {
                var clone = this;
                clone.Invert();

                return clone;
            }
        }

        public float Angle
        {
            get
            {
                return MathF.Atan2(-y, -x) + MathF.PI;
            }

            set
            {
                var length = Length;
                var sin = MathF.Sin(value);
                var cos = MathF.Cos(value);

                x = cos * length;
                y = sin * length;
            }
        }

        public float AngleDeg
        {
            get
            {
                return MathF.Atan2(-y, -x) * Math2.Rad2Deg + 180.0f;
            }

            set
            {
                value *= Math2.Deg2Rad;

                var length = Length;
                var sin = MathF.Sin(value);
                var cos = MathF.Cos(value);

                x = cos * length;
                y = sin * length;
            }
        }

        public Vec2 Perpendicular
        {
            get
            {
                return new Vec2(-y, x);
            }
        }

        public Vec2 Opposite
        {
            get
            {
                var clone = this;
                clone.Negate();

                return clone;
            }
        }

        public void CopyTo(ref Vec2 destination)
        {
            destination.x = x;
            destination.y = y;
        }

        public void CopyFrom(in Vec2 source)
        {
            x = source.x;
            y = source.y;
        }

        public void CopyFrom(Vec2 source)
        {
            x = source.x;
            y = source.y;
        }

        public void SetX(float x)
        {
            this.x = x;
        }

        public void SetY(float y)
        {
            this.y = y;
        }

        public void Set(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void Set(float xy)
        {
            x = y = xy;
        }

        public void SetZero()
        {
            x = y = 0;
        }

        public void SetFromRadians(float angle, float length)
        {
            var sin = MathF.Sin(angle);
            var cos = MathF.Cos(angle);

            x = cos * length;
            y = sin * length;
        }

        public void SetFromDegrees(float angle, float length)
        {
            angle *= Math2.Deg2Rad;

            var sin = MathF.Sin(angle);
            var cos = MathF.Cos(angle);

            x = cos * length;
            y = sin * length;
        }

        public float AngleTo(Vec2 other)
        {
            return MathF.Atan2(y * other.x - x * other.y, x * other.x + y * other.y);
        }

        public float AngleToDeg(Vec2 other)
        {
            return MathF.Atan2(y * other.x - x * other.y, x * other.x + y * other.y) * Math2.Rad2Deg;
        }

        public float Dot(Vec2 other)
        {
            return x * other.x + y * other.y;
        }

        public float Cross(Vec2 other)
        {
            return x * other.y - y * other.x;
        }

        public void Normalize()
        {
            var length = Length;

            if (length == 0.0f)
            {
                CopyFrom(in Right);
            }
            else
            {
                Multiply(1.0f / length);
            }
        }

        public void Invert()
        {
            if (x != 0.0f)
            {
                x = 1.0f / x;
            }

            if (y != 0.0f)
            {
                y = 1.0f / y;
            }
        }

        public void Multiply(float scalar)
        {
            x *= scalar;
            y *= scalar;
        }

        public void Multiply(Vec2 other)
        {
            x *= other.x;
            y *= other.y;
        }

        public void Multiply(float x, float y)
        {
            this.x *= x;
            this.y *= y;
        }

        public void Divide(float scalar)
        {
            var invertedScalar = 1.0f / scalar;

            x *= invertedScalar;
            y *= invertedScalar;
        }

        public void Divide(float x, float y)
        {
            this.x /= x;
            this.y /= y;
        }

        public void Divide(Vec2 other)
        {
            x /= other.x;
            y /= other.y;
        }

        public void Add(Vec2 other)
        {
            x += other.x;
            y += other.y;
        }

        public void Add(float x, float y)
        {
            this.x += x;
            this.y += y;
        }

        public void AddVectors(Vec2 a, Vec2 b)
        {
            x = a.x + b.x;
            y = a.y + b.y;
        }

        public void Subtract(Vec2 other)
        {
            x -= other.x;
            y -= other.y;
        }

        public void Subtract(float x, float y)
        {
            this.x -= x;
            this.x -= y;
        }

        public void SubtractVectors(Vec2 a, Vec2 b)
        {
            x = a.x - b.x;
            y = a.y - b.y;
        }

        public void Negate()
        {
            x = -x;
            y = -y;
        }

        public void NegateVector(Vec2 other)
        {
            x = -other.x;
            y = -other.y;
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

        public void Rotate(float angle)
        {
            var sin = MathF.Sin(angle);
            var cos = MathF.Cos(angle);
            var prevX = x;

            x = x * cos + y * sin;
            y = y * cos - prevX * sin;
        }

        public Vec2 Rotated(float angle)
        {
            var clone = this;
            var sin = MathF.Sin(angle);
            var cos = MathF.Cos(angle);

            clone.x = x * cos + y * sin;
            clone.y = y * cos - x * sin;

            return clone;
        }

        public void RotateAround(Vec2 center, float angle)
        {
            var sin = MathF.Sin(angle);
            var cos = MathF.Cos(angle);

            var dirX = x - center.x;
            var dirY = y - center.y;

            x = dirX * cos + dirY * sin + center.x;
            y = dirY * cos - dirX * sin + center.y;
        }

        public Vec2 RotateAroundThis(Vec2 point, float angle)
        {
            var sin = MathF.Sin(angle);
            var cos = MathF.Cos(angle);

            var dirX = point.x - x;
            var dirY = point.y - y;

            point.x = dirX * cos + dirY * sin + x;
            point.y = dirY * cos - dirX * sin + y;

            return point;
        }

        public void Lerp(Vec2 to, float time)
        {
            x += (to.x - x) * time;
            y += (to.y - y) * time;
        }

        public void LerpVectors(Vec2 from, Vec2 to, float time)
        {
            x = from.x + (to.x - from.x) * time;
            y = from.y + (to.y - from.y) * time;
        }

        public void Clamp(Vec2 min, Vec2 max)
        {
            x = MathF.Max(min.x, MathF.Min(max.x, x));
            y = MathF.Max(min.y, MathF.Min(max.y, y));
        }

        public void Clamp(float min, float max)
        {
            x = MathF.Max(min, MathF.Min(max, x));
            y = MathF.Max(min, MathF.Min(max, y));
        }

        public void ClampLength(float min, float max)
        {
            var currentLength = Length;

            Normalize();
            Multiply(MathF.Max(min, MathF.Min(max, currentLength)));
        }

        public void Floor()
        {
            x = MathF.Floor(x);
            y = MathF.Floor(y);
        }

        public void Ceiling()
        {
            x = MathF.Ceiling(x);
            y = MathF.Ceiling(y);
        }

        public void Round()
        {
            x = MathF.Round(x);
            y = MathF.Round(y);
        }

        public bool Equals(Vec2 other)
        {
            return x == other.x && y == other.y;
        }

        public bool Equals(Vec2 other, float eps)
        {
            return MathF.Abs(x - other.x) <= eps && Math.Abs(y - other.y) <= eps;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vec2))
            {
                return false;
            }

            Vec2 vec = (Vec2)obj;

            return x == vec.x && y == vec.y;
        }

        public override string ToString()
        {
            return x + " " + y;
        }

        public override int GetHashCode()
        {
            return (int)(x * y + x + y);
        }

        public static readonly Vec2 Zero = new Vec2(0, 0);

        public static readonly Vec2 Up = new Vec2(0, -1);

        public static readonly Vec2 Down = new Vec2(0, 1);

        public static readonly Vec2 Left = new Vec2(-1, 0);

        public static readonly Vec2 Right = new Vec2(1, 0);

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

        public static Vec2 operator *(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x * b.x, a.y * b.y);
        }

        public static Vec2 operator /(Vec2 vec, float scalar)
        {
            var invertedScalar = 1.0f / scalar;

            return new Vec2(vec.x * invertedScalar, vec.y * invertedScalar);
        }

        public static implicit operator Vector2u(Vec2 vec)
        {
            return new Vector2u((uint)vec.x, (uint)vec.y);
        }

        public static implicit operator Vector2f(Vec2 vec)
        {
            return new Vector2f(vec.x, vec.y);
        }

        public static implicit operator Vector2i(Vec2 vec)
        {
            return new Vector2i((int)vec.x, (int)vec.y);
        }

        public static implicit operator Vec2(Vector2u vec)
        {
            return new Vec2(vec.X, vec.Y);
        }

        public static implicit operator Vec2(Vector2f vec)
        {
            return new Vec2(vec.X, vec.Y);
        }

        public static implicit operator Vec2(Vector2i vec)
        {
            return new Vec2(vec.X, vec.Y);
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

        public static Vec2 Lerp(Vec2 a, Vec2 b, float time)
        {
            a.Lerp(b, time);

            return a;
        }

        public static Vec2 FromRadians(float angle, float length)
        {
            var sin = MathF.Sin(angle);
            var cos = MathF.Cos(angle);

            return new Vec2(cos * length, sin * length);
        }

        public static Vec2 FromDegrees(float angle, float length)
        {
            angle *= Math2.Deg2Rad;

            var sin = MathF.Sin(angle);
            var cos = MathF.Cos(angle);

            return new Vec2(cos * length, sin * length);
        }

        public static float AngleBetween(Vec2 a, Vec2 b)
        {
            var dot = a.x * b.x + a.y * b.y;

            return MathF.Acos(dot / (a.Length * b.Length));
        }
    }
}
