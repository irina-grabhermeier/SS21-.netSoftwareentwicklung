using System;
using Moq;
using NUnit.Framework;

namespace Assignment7.Tests
{
    [TestFixture]
    [TestOf(typeof(RateLimitedExecutor))]
    public class RateLimitedExecutorTest
    {

        [Test]
        public void InvokeExecutor_Once_WhenOnTickEventIsFired()
        {
            // Arrange
            var executor = new Mock<ITaskExecutor>();
            executor.Setup(foo => foo.Execute(It.IsAny<Action>()));

            var clock = new Mock<IClock>();
            clock.Setup(foo => foo.Start());

            // Act
            var underTest = new RateLimitedExecutor(
                executor.Object,
                clock.Object,
                new[] { new Action(() => { }) });
            clock.Raise(f => f.OnTick += null, null, new TickArg());

            // Assert
            executor.Verify(foo => foo.Execute(It.IsAny<Action>()), Times.Once);
        }

        [Test]
        public void InvokeExecutor_TwoTimes_WhenOnTickEventIsFiredTwice()
        {
            // Arrange
            var executor = new Mock<ITaskExecutor>();
            executor.Setup(foo => foo.Execute(It.IsAny<Action>()));

            var clock = new Mock<IClock>();

            // Act
            var underTest = new RateLimitedExecutor(
                executor.Object,
                clock.Object,
                new[] { new Action(() => { }), new Action(() => { }) });
            clock.Raise(f => f.OnTick += null, this, new TickArg());
            clock.Raise(f => f.OnTick += null, this, new TickArg());

            // Assert
            executor.Verify(foo => foo.Execute(It.IsAny<Action>()), Times.Exactly(2));
        }

        [Test]
        public void InvokeExecutor_Never_WhenOnTickEventAndNoActionIsInQueue()
        {
            // Arrange
            var executor = new Mock<ITaskExecutor>();
            executor.Setup(foo => foo.Execute(It.IsAny<Action>()));

            var clock = new Mock<IClock>();

            // Act
            var underTest = new RateLimitedExecutor(
                executor.Object,
                clock.Object);
            clock.Raise(f => f.OnTick += null, this, new TickArg());

            // Assert
            executor.Verify(foo => foo.Execute(It.IsAny<Action>()), Times.Never);
        }
    }
}