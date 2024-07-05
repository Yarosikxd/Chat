using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatTests
{
    public class ChatTest
    {
        [Fact]
        public void CanCreateChat()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Act
            using (var context = new TestDbContext(options))
            {
                var chat = new Chat { Name = "Test Chat" };
                context.Chats.Add(chat);
                context.SaveChanges();

                // Assert
                Assert.Equal(1, context.Chats.Count());
                Assert.Equal("Test Chat", context.Chats.Single().Name);
            }
        }
    }
}