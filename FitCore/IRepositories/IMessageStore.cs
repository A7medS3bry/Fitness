using FitCore.Dto;
using FitCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.IRepositories
{
    public interface IMessageStore
    {
        Task SaveMessageAsync(string userId, MessageDto message);
        List<ChatMessage> GetMessagesBetweenUsers(string userId1, string userId2);
    }
}
