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
            var message = new Message() { Content = "test", Id = 0 };
            const string username = "testusr";

            // act
            repo.InsertMessage(username, message);

            // assert
            Assert.Greater(message.Id, 0);
        }

        [Test]
        public void TestGetMessageRetrievesMessageForValidId()
        {
            // arrange
            var repo = new InMemoryMessageRepository();
            var message = new Message() { Content = "test", Id = 0 };
            const string username = "testusr";

            repo.InsertMessage(username, message);

            // act
            var storedMessage = repo.GetMessageById(username, message.Id);

            // assert
            Assert.IsNotNull(storedMessage);
        }

        [Test]
        public void TestGetMessageRetrievesNoMessageForInvalidId()
        {
            // arrange
            var repo = new InMemoryMessageRepository();
            const string username = "testusr";
            var invalidId = 1;

            // act
            var storedMessage = repo.GetMessageById(username, invalidId);

            // assert
            Assert.IsNull(storedMessage);
        }

    }
}
