using FitCore.Dto;
using FitCore.IRepositories;
using FitCore.Models;
using FitData.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Fit.Controllers
{

    [ApiController]
    [Route("api/messages")]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IMessageStore _messageStore;

        public MessagesController(IHubContext<ChatHub> hubContext, IMessageStore messageStore)
        {
            _hubContext = hubContext;
            _messageStore = messageStore;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromForm]MessageDto dto)
        {
            var userId = User.FindFirstValue("uid");
            var timestamp = DateTime.UtcNow;
           
            await _messageStore.SaveMessageAsync(userId, dto);

            await _hubContext.Clients.User(dto.ResiverUserId).SendAsync("ReceiveMessage", dto.ResiverUserId, dto.Message, timestamp);
            return Ok();
        }

        [HttpPost]
        public IActionResult GetMessageHistory([FromForm] GetMessages model)
        {
            var userId = User.FindFirstValue("uid");
            var messages = _messageStore.GetMessagesBetweenUsers(userId, model.SenderId);
            if (messages == null || messages.Count == 0)
            {
                return NotFound("No messages found between the specified users.");
            }
            return Ok(messages);
        }
    }
}
