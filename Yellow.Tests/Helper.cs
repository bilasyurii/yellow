using System;
using Xunit;

namespace Yellow.Tests
{
    public static class Helper
    {
        public const float Eps = 0.001f;

        public static void Equal(float a, float b, float precision)
        {
            Assert.True(MathF.Abs(a - b) <= precision);
        }
    }
}
