using Application.Interfaces;
using Domain.Context;
using Domain.Hubs;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<ChatHub> _chatHubContext;

        public MessageService(AppDbContext context, IHubContext<ChatHub> chatHubContext)
        {
            _context = context;
            _chatHubContext = chatHubContext;
        }

        public async Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId)
        {
            return await _context.Messages.Where(m => m.ChatId == chatId).ToListAsync();
        }

        public async Task<Message> SendMessageAsync(Guid chatId, Guid userId, string message)
        {
            var msg = new Message
            {
                Id = Guid.NewGuid(),
                ChatId = chatId,
                UserId = userId,
                Text = message,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();

            await _chatHubContext.Clients.All.SendAsync("ReceiveMessage", msg);

            return msg;
        }
    }
}
