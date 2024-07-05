using Api.Controllers;
using Application.Interfaces;
using Domain.Context;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ChatTests
{
    public class ChatIntegrationTests
    {
        [Fact]
        public async Task CanCreateAndDeleteChat()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                // Create mock services
                var mockChatService = new Mock<IChatService>();
                var mockMessageService = new Mock<IMessageService>();

                // Setup mock methods for ChatService
                mockChatService.Setup(s => s.CreateChatAsync(It.IsAny<Chat>()))
                    .ReturnsAsync((Chat chat) =>
                    {
                        chat.Id = Guid.NewGuid();
                        chat.CreatedByUserId = Guid.NewGuid(); // Set a mock user ID
                        return chat;
                    });

                mockChatService.Setup(s => s.DeleteChatAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                    .ReturnsAsync((Guid chatId, Guid userId) =>
                    {
                        // Simulate deletion based on some mock condition
                        return chatId == Guid.NewGuid(); // Return true if deletion succeeds
                    });

                // Initialize controller with mock services
                var controller = new ChatsController(mockChatService.Object, mockMessageService.Object);

                // Act: Create a chat
                var chat = new Chat { Name = "Integration Test Chat", CreatedByUserId = Guid.NewGuid() };
                var createResult = await controller.CreateChat(chat);

                // Assert: Check creation result
                var result = await controller.GetAllChats(); // Corrected method call
                var chats = Assert.IsType<List<Chat>>(result.Value);
                Assert.Single(chats);

                // Get the ID of the created chat
                var createdChat = (createResult.Result as CreatedAtActionResult).Value as Chat;
                Guid chatId = createdChat.Id;

                // Act: Delete the chat
                var deleteResult = await controller.DeleteChat(chatId, Guid.NewGuid()); // Provide a mock user ID

                // Assert: Check deletion result
                Assert.IsType<NoContentResult>(deleteResult);

                // Ensure the chat was deleted
                result = await controller.GetAllChats(); // Corrected method call
                chats = Assert.IsType<List<Chat>>(result.Value);
                Assert.Empty(chats);
            }
        }
    }
}
