using NUnit.Framework;
using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.Test
{
    [TestFixture]
    class InMemoryMessageRepositoryTest
    {
        [Test]
        public void TestInsertMessageSetsIdBiggerThanZero()
        {
            // arrange
            var repo = new InMemoryMessageRepository();
            var user = new User();
            var message = new Message() { Content = "test", Id = 0 };

            // act
            repo.InsertMessage(user.Username, message);

            // assert
            Assert.Greater(message.Id, 0);
        }

        [Test]
        public void TestGetMessageRetrievesMessageForValidId()
        {
            // arrange
            var repo = new InMemoryMessageRepository();
            var message = new Message() { Content = "test", Id = 0 };
            var user = new User();
            repo.InsertMessage(user.Username,message);

            // act
            var storedMessage = repo.GetMessageById(user.Username,message.Id);

            // assert
            Assert.IsNotNull(storedMessage);
        }

        [Test]
        public void TestGetMessageRetrievesNoMessageForInvalidId()
        {
            var user = new User();
            // arrange
            var repo = new InMemoryMessageRepository();
            var invalidId = 1;

            // act
            var storedMessage = repo.GetMessageById(user.Username,invalidId);

            // assert
            Assert.IsNull(storedMessage);
        }
    }
}
