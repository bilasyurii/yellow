using Xunit;
using Yellow.Core;

namespace Yellow.Tests
{
    public class TimerTests
    {
        [Fact]
        public void Add()
        {
            // arrange
            Locator.ProvideStandardServices();

            var game = new Game();
            var timer = game.Time.MakeTimer();
            var temp = 0;

            timer.Start();

            timer.Add(70, (args) => temp = 1);

            // act
            timer.Update(50);

            Assert.NotEqual(1, temp);

            timer.Update(100);

            Assert.Equal(1, temp);
        }
    }
}
