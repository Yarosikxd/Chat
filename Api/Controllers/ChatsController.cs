using Application.Interfaces;
using Application.Services;
using Domain.Hubs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;

        public ChatsController(IChatService chatService, IMessageService messageService)
        {
            _chatService = chatService;
            _messageService = messageService;
        }

        [HttpGet("GetAllChats")]
        public async Task<ActionResult<IEnumerable<Chat>>> GetAllChats()
        {
            var chats = await _chatService.GetAllChatsAsync();
            return Ok(chats);
        }

        [HttpGet("GetChat/{id}")]
        public async Task<ActionResult<Chat>> GetChat(Guid id)
        {
            var chat = await _chatService.GetChatByIdAsync(id);
            if (chat == null)
            {
                return NotFound();
            }
            return chat;
        }

        [HttpGet("SearchChats")]
        public async Task<ActionResult<IEnumerable<Chat>>> SearchChats([FromQuery] string chatName)
        {
            var chats = await _chatService.SearchChatsByNameAsync(chatName);
            return Ok(chats);
        }

        [HttpPost("CreateChat")]
        public async Task<ActionResult<Chat>> CreateChat(Chat chat)
        {
            var createdChat = await _chatService.CreateChatAsync(chat);
            return chat;
        }

        [HttpDelete("DeleteChat/{id}")]
        public async Task<IActionResult> DeleteChat(Guid id, [FromQuery] Guid userId)
        {
            var result = await _chatService.DeleteChatAsync(id, userId);
            if (!result)
            {
                return Forbid("You do not have permission to delete this chat.");
            }
            return NoContent();
        }

        [HttpPost("SendMessage")]
        public async Task<ActionResult<Message>> SendMessage(Guid chatId, Guid userId, string message)
        {
            var msg = await _messageService.SendMessageAsync(chatId, userId, message);
            return Ok(msg);
        }
    }
}
