using FitCore.Models;
using FitCore.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.IRepositories
{
    public interface ISearch
    {
        Task<List<GetUsers>> GetUsersByRoleAsync(string role);
        Task<List<GetUsers>> GetAllTrainees();
        Task<List<GetUsers>> GetAllTrainers();


        Task<GetUsers> SearchUserById(string id);
        Task<List<GetUsers>> SearchUserByName(string name);
        List<GetUsers> MapUsersToUser(List<ApplicationUser> users);
    }
}
