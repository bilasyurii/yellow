using System;
using Xunit;
using Yellow.Core.Utils;

namespace Yellow.Tests
{
    public class Vec2Tests
    {
        [Fact]
        public void OperatorPlus()
        {
            // arrange
            var a = new Vec2(2, 3);
            var b = new Vec2(1, 1.5f);

            // act
            var result = a + b;

            // assert
            Assert.True(result.x == 3.0f && result.y == 4.5f);
        }

        [Fact]
        public void OperatorMinus()
        {
            // arrange
            var a = new Vec2(2, 3);
            var b = new Vec2(1, 1.5f);

            // act
            var result = a - b;

            // assert
            Assert.True(result.x == 1.0f && result.y == 1.5f);
        }

        [Fact]
        public void OperatorNegate()
        {
            // arrange
            var a = new Vec2(2, -3);

            // act
            var result = -a;

            // assert
            Assert.True(result.x == -2.0f && result.y == 3.0f);
        }

        [Fact]
        public void OperatorMultiplyScalar()
        {
            // arrange
            var a = new Vec2(2, 3);
            var b = 2;

            // act
            var result = a * b;

            // assert
            Assert.True(result.x == 4.0f && result.y == 6.0f);
        }

        [Fact]
        public void OperatorMultiplyVectors()
        {
            // arrange
            var a = new Vec2(2, 3);
            var b = new Vec2(4, 2);

            // act
            var result = a * b;

            // assert
            Assert.True(result.x == 8.0f && result.y == 6.0f);
        }

        [Fact]
        public void OperatorDivideScalar()
        {
            // arrange
            var a = new Vec2(2, 3);
            var b = 4;

            // act
            var result = a / b;

            // assert
            Assert.True(result.x == 0.5f && result.y == 0.75f);
        }

        [Fact]
        public void OperatorEquals()
        {
            // arrange
            var a = new Vec2(2, 3);
            var b = new Vec2(2, 3);

            // act
            var result = a == b;

            // assert
            Assert.True(result);
        }

        [Fact]
        public void Longer()
        {
            // arrange
            var a = new Vec2(2, 3);
            var b = new Vec2(1, 1);

            // act
            var result = Vec2.Longer(a, b);

            // assert
            Assert.True(result == a);
        }

        [Fact]
        public void Shorter()
        {
            // arrange
            var a = new Vec2(2, 3);
            var b = new Vec2(1, 1);

            // act
            var result = Vec2.Shorter(a, b);

            // assert
            Assert.True(result == b);
        }

        [Fact]
        public void Floor()
        {
            // arrange
            var a = new Vec2(2.2f, 3.8f);

            // act
            a.Floor();

            // assert
            Assert.True(a.x == 2.0f && a.y == 3.0f);
        }

        [Fact]
        public void Clamp()
        {
            // arrange
            var a = new Vec2(2.2f, 3.8f);
            var min = new Vec2(2.5f, 3.2f);
            var max = new Vec2(3.0f, 3.5f);

            // act
            a.Clamp(min, max);

            // assert
            Assert.True(a.x == 2.5f && a.y == 3.5f);
        }

        [Fact]
        public void Lerp()
        {
            // arrange
            var a = new Vec2(2, 3);
            var b = new Vec2(3, 7);

            // act
            a.Lerp(b, 0.5f);

            // assert
            Assert.True(a.x == 2.5f && a.y == 5.0f);
        }

        [Fact]
        public void Rotate()
        {
            // arrange
            var a = new Vec2(2, 3);
            var b = a;

            // act
            a.Rotate(MathF.PI * 0.25f);
            b.Rotate(MathF.PI * -0.25f);

            // assert
            Helper.Equal(a.x, 3.535533707355702f, Helper.Eps);
            Helper.Equal(a.y, 0.7071077740710008f, Helper.Eps);
            Helper.Equal(b.x, -0.7071077740710008f, Helper.Eps);
            Helper.Equal(b.y, 3.535533707355702f, Helper.Eps);
        }

        [Fact]
        public void Rotated()
        {
            // arrange
            var a = new Vec2(2, 3);

            // act
            var b = a.Rotated(MathF.PI * 0.25f);

            // assert
            Helper.Equal(b.x, 3.535533707355702f, Helper.Eps);
            Helper.Equal(b.y, 0.7071077740710008f, Helper.Eps);
        }

        [Fact]
        public void RotateAround()
        {
            // arrange
            var a = new Vec2(3, 4);
            var b = new Vec2(1, 1);

            // act
            a.RotateAround(b, MathF.PI * 0.25f);

            // assert
            Helper.Equal(a.x, 4.535533707355702f, Helper.Eps);
            Helper.Equal(a.y, 1.7071077740710008f, Helper.Eps);
        }

        [Fact]
        public void RotateAroundThis()
        {
            // arrange
            var a = new Vec2(3, 4);
            var b = new Vec2(1, 1);

            // act
            var c = b.RotateAroundThis(a, MathF.PI * 0.25f);

            // assert
            Helper.Equal(c.x, 4.535533707355702f, Helper.Eps);
            Helper.Equal(c.y, 1.7071077740710008f, Helper.Eps);
            Assert.Equal(3.0f, a.x);
            Assert.Equal(4.0f, a.y);
        }

        [Fact]
        public void Distance()
        {
            // arrange
            var a = new Vec2(4, 6);
            var b = new Vec2(1, 2);

            // act
            var distance = a.Distance(b);

            // assert
            Assert.Equal(5.0f, distance);
        }

        [Fact]
        public void DistanceSquared()
        {
            // arrange
            var a = new Vec2(4, 6);
            var b = new Vec2(1, 2);

            // act
            var distance = a.DistanceSquared(b);

            // assert
            Assert.Equal(25.0f, distance);
        }

        [Fact]
        public void ManhattanDistance()
        {
            // arrange
            var a = new Vec2(4, 6);
            var b = new Vec2(1, 2);

            // act
            var distance = a.ManhattanDistance(b);

            // assert
            Assert.Equal(7.0f, distance);
        }

        [Fact]
        public void Negate()
        {
            // arrange
            var a = new Vec2(4, -6);

            // act
            a.Negate();

            // assert
            Assert.Equal(-4.0f, a.x);
            Assert.Equal(6.0f, a.y);
        }

        [Fact]
        public void NegateVector()
        {
            // arrange
            var a = new Vec2(0, 0);
            var b = new Vec2(4, -6);

            // act
            a.NegateVector(b);

            // assert
            Assert.Equal(-4.0f, a.x);
            Assert.Equal(6.0f, a.y);
        }

        [Fact]
        public void Invert()
        {
            // arrange
            var a = new Vec2(4, 5);

            // act
            a.Invert();

            // assert
            Assert.Equal(0.25f, a.x);
            Assert.Equal(0.2f, a.y);
        }

        [Fact]
        public void Normalize()
        {
            // arrange
            var a = new Vec2(4, 3);

            // act
            a.Normalize();

            // assert
            Assert.Equal(0.8f, a.x);
            Assert.Equal(0.6f, a.y);
        }

        [Fact]
        public void Dot()
        {
            // arrange
            var a = new Vec2(2, 3);
            var b = new Vec2(4, 5);

            // act
            var dot = a.Dot(b);

            // assert
            Assert.Equal(23.0f, dot);
        }

        [Fact]
        public void Cross()
        {
            // arrange
            var a = new Vec2(2, 3);
            var b = new Vec2(4, 5);

            // act
            var cross = a.Cross(b);

            // assert
            Assert.Equal(-2.0f, cross);
        }

        [Fact]
        public void AngleTo()
        {
            // arrange
            var a = new Vec2(2, 3);
            var b = new Vec2(4, 5);

            // act
            var angleAB = a.AngleTo(b);
            var angleBA = b.AngleTo(a);

            // assert
            Helper.Equal(0.0867383387f, angleAB, Helper.Eps);
            Helper.Equal(-0.0867383387f, angleBA, Helper.Eps);
        }

        [Fact]
        public void SetFromDegrees()
        {
            // arrange
            var a = Vec2.Zero;

            // act
            a.SetFromDegrees(-60.0f, 10.0f);

            // assert
            Helper.Equal(5.0f, a.x, Helper.Eps);
            Helper.Equal(-8.66025f, a.y, Helper.Eps);
        }

        [Fact]
        public void CopyTo()
        {
            // arrange
            var a = Vec2.Zero;
            var b = new Vec2(2, 3);

            // act
            b.CopyTo(ref a);

            // assert
            Helper.Equal(2f, a.x, Helper.Eps);
            Helper.Equal(3f, a.y, Helper.Eps);
        }

        [Fact]
        public void Angle()
        {
            // arrange
            var a = new Vec2(1, 3);

            // act
            var angle = a.Angle;

            // assert
            Helper.Equal(1.24904f, angle, Helper.Eps);
        }

        [Fact]
        public void AngleBetween()
        {
            // arrange
            var a = new Vec2(1, 3);
            var b = new Vec2(2, 4);

            // act
            var angle = Vec2.AngleBetween(a, b);

            // assert
            Helper.Equal(0.141897f, angle, Helper.Eps);
        }
    }
}
