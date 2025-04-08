using FitCore.Dto;
using FitCore.IRepositories;
using FitCore.Models;
using FitData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitData.Repositories 
{
    public class EFMessageStore : IMessageStore
    {
        private readonly ApplicationDbContext _context;

        public EFMessageStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveMessageAsync(string userId, MessageDto message)
        {

            var Model = new ChatMessage
            {
                SenderId = userId,
                ReceiverId = message.ResiverUserId,
                Message = message.Message,
                Timestamp = DateTime.UtcNow
            };

            _context.ChatMessages.Add(Model);
            await _context.SaveChangesAsync();
        }

        public List<ChatMessage> GetMessagesBetweenUsers(string senderId, string receiverId)
        {
            var messagesList =  _context.ChatMessages
                .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                            (m.SenderId == receiverId && m.ReceiverId == senderId))
                .OrderBy(m => m.Timestamp)
                .ToList();

            return messagesList;
        }
    }
}
