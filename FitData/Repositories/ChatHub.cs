using FitCore.Dto;
using FitCore.IRepositories;
using FitCore.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitData.Repositories
{
    public class ChatHub : Hub
    {   
        private readonly IMessageStore _messageStore;

        public ChatHub(IMessageStore messageStore)
        {
            _messageStore = messageStore;
        }

        public async Task SendMessageToUser(MessageDto model)
        {
            var senderId = Context.UserIdentifier;
            var timestamp = DateTime.UtcNow;

            await _messageStore.SaveMessageAsync(senderId, model);


            await Clients.User(senderId).SendAsync("ReceiveMessage", senderId, model.Message, timestamp);
        }
    }
}
