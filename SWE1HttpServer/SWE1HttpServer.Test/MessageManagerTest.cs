using Moq;
using NUnit.Framework;
using SWE1HttpServer.DAL;
using SWE1HttpServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.Test
{
    [TestFixture]
    class MessageManagerTest
    {
        [Test]
        public void AddMessageStoresMessageInRepository()
        {
            // arrange
            var userRepository = new InMemoryUserRepository();
            var user = new User();
            var messageRepo = new Mock<IMessageRepository>();
            var messageManager = new MessageManager(messageRepo.Object,userRepository);
            var content = "testcontent";

            // act
            messageManager.AddMessage(user,content);

            // assert
            messageRepo.Verify(m => m.InsertMessage(user.Username,It.Is<Message>(m => m.Content == content)));
        }

        [Test]
        public void ShowMessageWithInvalidMessageIdThrowsMessageNotFoundException()
        {
            // arrange
            var messageRepo = new Mock<IMessageRepository>();
             var userRepository = new InMemoryUserRepository();
            var user = new User();
            var messageManager = new MessageManager(messageRepo.Object,userRepository);
            var invalidId = 1;
            messageRepo.Setup(m => m.GetMessageById(user.Username,invalidId)).Returns((Message)null);

            // act and assert
            Assert.Throws<MessageNotFoundException>(() => messageManager.ShowMessage(user,invalidId));
        }

        [Test]
        public void ShowMessageWithValidMessageIdReturnsMessage()
        {
            // arrange
              var userRepository = new InMemoryUserRepository();
            var user = new User();
            var messageRepo = new Mock<IMessageRepository>();
            var messageManager = new MessageManager(messageRepo.Object,userRepository);
            var expectedMessage = new Message() { Content = "test", Id = 1 };
            messageRepo.Setup(m => m.GetMessageById(user.Username,expectedMessage.Id)).Returns(expectedMessage);

            // act
            var returnedMessage = messageManager.ShowMessage(user,expectedMessage.Id);

            // assert
            Assert.AreEqual(expectedMessage, returnedMessage);
        }
    }
}
