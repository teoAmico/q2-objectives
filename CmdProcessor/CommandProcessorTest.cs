using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using CommandApp04;

namespace CommandApp.Tests
{
    [TestClass]
    public class CommandProcessorTest
    {
        private Mock<ICommandHandler> _mockCommandHandler;
        private CommandProcessor _commandProcessor;

        [TestInitialize]
        public void Setup()
        {
            _mockCommandHandler = new Mock<ICommandHandler>();
            _commandProcessor = new CommandProcessor(_mockCommandHandler.Object);
        }

        [TestMethod]
        public void ProcessCommand_Start_ShouldReturnStartMessage()
        {
            _mockCommandHandler.Setup(handler => handler.Handle("start")).Returns("Command 'start' executed");

            string result = _commandProcessor.ProcessCommand("start");

            Assert.AreEqual("Command 'start' executed", result);
            _mockCommandHandler.Verify(handler => handler.Handle("start"), Times.Once);
        }

        [TestMethod]
        public void ProcessCommand_Stop_ShouldReturnStopMessage()
        {
            _mockCommandHandler.Setup(handler => handler.Handle("stop")).Returns("Command 'stop' executed");

            string result = _commandProcessor.ProcessCommand("stop");

            Assert.AreEqual("Command 'stop' executed", result);
            _mockCommandHandler.Verify(handler => handler.Handle("stop"), Times.Once);
        }

        [TestMethod]
        public void ProcessCommand_Pause_ShouldReturnPauseMessage()
        {
            _mockCommandHandler.Setup(handler => handler.Handle("pause")).Returns("Command 'pause' executed");

            string result = _commandProcessor.ProcessCommand("pause");

            Assert.AreEqual("Command 'pause' executed", result);
            _mockCommandHandler.Verify(handler => handler.Handle("pause"), Times.Once);
        }

        [TestMethod]
        public void ProcessCommand_Unknown_ShouldReturnUnknownMessage()
        {
            _mockCommandHandler.Setup(handler => handler.Handle("unknown")).Returns("Unknown command");

            string result = _commandProcessor.ProcessCommand("unknown");

            Assert.AreEqual("Unknown command", result);
            _mockCommandHandler.Verify(handler => handler.Handle("unknown"), Times.Once);
        }

        [TestMethod]
        public void ProcessCommand_Empty_ShouldReturnInvalidMessage()
        {
            _mockCommandHandler.Setup(handler => handler.Handle("")).Returns("Invalid command");

            string result = _commandProcessor.ProcessCommand("");

            Assert.AreEqual("Invalid command", result);
            _mockCommandHandler.Verify(handler => handler.Handle(""), Times.Once);
        }

        [TestMethod]
        public void ProcessCommand_Null_ShouldReturnInvalidMessage()
        {
            _mockCommandHandler.Setup(handler => handler.Handle(null)).Returns("Invalid command");

            string result = _commandProcessor.ProcessCommand(null);

            Assert.AreEqual("Invalid command", result);
            _mockCommandHandler.Verify(handler => handler.Handle(null), Times.Once);
        }
    }
}
