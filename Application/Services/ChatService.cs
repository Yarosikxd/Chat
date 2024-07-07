using Application.Interfaces;
using Domain.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ChatService : IChatService
    {
        private readonly AppDbContext _context;

        public ChatService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Chat>> GetAllChatsAsync()
        {
            return await _context.Chats.ToListAsync();
        }

        public async Task<Chat> GetChatByIdAsync(Guid chatId)
        {
            return await _context.Chats.FindAsync(chatId);
        }

        public async Task<Chat> CreateChatAsync(Chat chat)
        {
            chat.Id = Guid.NewGuid();
            chat.CreatedByUserId = chat.Id;
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
            return chat;
        }

        public async Task<bool> DeleteChatAsync(Guid chatId, Guid userId)
        {
            var chat = await _context.Chats.FindAsync(chatId);
            if (chat == null || chat.CreatedByUserId != userId)
            {
                return false;
            }
           
            _context.Chats.Remove(chat);

            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Chat>> SearchChatsByNameAsync(string chatName)
        {
            return await _context.Chats.Where(c => c.Name.Contains(chatName)).ToListAsync();
        }
    }
}
