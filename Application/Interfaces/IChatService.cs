using Domain.Models;

namespace Application.Interfaces
{
    public interface IChatService
    {
        Task<IEnumerable<Chat>> GetAllChatsAsync();
        Task<Chat> GetChatByIdAsync(Guid id);
        Task<Chat> CreateChatAsync(Chat chat);
        Task<bool> DeleteChatAsync(Guid id, Guid userId);
        Task<IEnumerable<Chat>> SearchChatsByNameAsync(string chatName);
    }
}
