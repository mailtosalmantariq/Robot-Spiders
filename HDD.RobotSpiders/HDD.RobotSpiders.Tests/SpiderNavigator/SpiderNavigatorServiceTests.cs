using HDD.RobotSpiders.Domain.Enums;
using HDD.RobotSpiders.Domain.Models;
using HDD.RobotSpiders.Services.SpiderNavigator;
using HDD.RobotSpiders.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDD.RobotSpiders.Tests.SpiderNavigator
{
    public class SpiderNavigatorServiceTests
    {
        private ISpiderNavigatorService _service;
        private ISpiderInputValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new SpiderInputValidator();
            _service = new SpiderNavigatorService(_validator);
        }

        [Test]
        public async Task ExecuteAsync_ValidInput_ReturnsExpectedPosition()
        {
            // Arrange
            var start = new Position(2, 4, Direction.Left);
            string commands = "FLFLFRFFLF";

            // Act
            var result = await _service.ExecuteAsync(start, commands, 7, 15);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.X, Is.EqualTo(3));
                Assert.That(result.Y, Is.EqualTo(1));
                Assert.That(result.Facing, Is.EqualTo(Direction.Right));
            });
        }

        [Test]
        public void ExecuteAsync_InvalidCommand_ThrowsApplicationException()
        {
            // Arrange
            var start = new Position(0, 0, Direction.Up);
            string commands = "FX"; // X is invalid

            // Act & Assert
            Assert.ThrowsAsync<ApplicationException>(async () =>
                await _service.ExecuteAsync(start, commands, 5, 5));
        }

        [Test]
        public void Validate_StartPositionOutsideWall_ThrowsArgumentException()
        {
            // Arrange
            var validator = new SpiderInputValidator();
            var start = new Position(10, 10, Direction.Up);

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                validator.Validate(start, "FFF", 5, 5));
        }

        [Test]
        public void Validate_NullStart_ThrowsArgumentNullException()
        {
            var validator = new SpiderInputValidator();

            Assert.Throws<ArgumentNullException>(() =>
                validator.Validate(null, "FFF", 5, 5));
        }

        [Test]
        public void Validate_EmptyCommands_ThrowsArgumentException()
        {
            var validator = new SpiderInputValidator();
            var start = new Position(0, 0, Direction.Up);

            Assert.Throws<ArgumentException>(() =>
                validator.Validate(start, "", 5, 5));
        }

        [Test]
        public async Task ExecuteAsync_MovementStopsAtBoundary()
        {
            // Arrange
            var start = new Position(0, 0, Direction.Down); // Already at bottom boundary
            string commands = "FFFF"; // Should not go below 0

            // Act
            var result = await _service.ExecuteAsync(start, commands, 5, 5);

            using (Assert.EnterMultipleScope())
            {
                // Assert
                Assert.That(result.X, Is.EqualTo(0));
                Assert.That(result.Y, Is.EqualTo(0));
                Assert.That(result.Facing, Is.EqualTo(Direction.Down));
            }
        }

        [Test]
        public async Task ExecuteAsync_TurnsCorrectly()
        {
            // Arrange
            var start = new Position(1, 1, Direction.Up);
            string commands = "LRLRLR";

            // Act
            var result = await _service.ExecuteAsync(start, commands, 5, 5);

            using (Assert.EnterMultipleScope())
            {
                // Assert
                Assert.That(result.X, Is.EqualTo(1));
                Assert.That(result.Y, Is.EqualTo(1));
                Assert.That(result.Facing, Is.EqualTo(Direction.Up));
            }
        }
    }

}
