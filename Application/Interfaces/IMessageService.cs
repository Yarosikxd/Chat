using Domain.Models;

namespace Application.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId);
        Task<Message> SendMessageAsync(Guid chatId, Guid userId, string message);
    }
}
