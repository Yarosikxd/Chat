using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("{chatId}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages(Guid chatId)
        {
            var messages = await _messageService.GetMessagesByChatIdAsync(chatId);
            return Ok(messages);
        }
    }
}
