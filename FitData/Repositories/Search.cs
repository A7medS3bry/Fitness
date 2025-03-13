using FitCore.IRepositories;
using FitCore.Models;
using FitCore.Models.Authentication;
using FitCore.Models.Search;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitData.Repositories
{
    public class Search : ISearch
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public Search(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public List<GetUsers> MapUsersToUser(List<ApplicationUser> users)
        {
            return users.Select(user => new GetUsers
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Height = user.Height,
                Weight = user.Weight,
                Gender = user.Gender,
                Level = user.LevelId
            }).ToList();
        }
        // Return null because the user does not exist
        public async Task<GetUsers> SearchUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            var SearchUserById = await _userManager.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (SearchUserById is null)
                return null;

            return MapUsersToUser(new List<ApplicationUser> { SearchUserById }).FirstOrDefault();
        }
        public async Task<List<GetUsers>> SearchUserByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new List<GetUsers>();


            var users = await _userManager.Users
                .Where(u =>
                    EF.Functions.Like(u.FirstName, $"{name}%") ||
                    EF.Functions.Like(u.LastName, $"{name}%"))
                .AsNoTracking()
                .ToListAsync();

            if (!users.Any())
                return new List<GetUsers>();

            return MapUsersToUser(users);
        }
        public async Task<List<GetUsers>> GetUsersByRoleAsync(string role)
        {
            if (string.IsNullOrEmpty(role))
                return new List<GetUsers>();

            var users = await _userManager.GetUsersInRoleAsync(role);
            if (users is null || !users.Any())
                return new List<GetUsers>();

            return MapUsersToUser(users.ToList());
        }
        public Task<List<GetUsers>> GetAllTrainees()
        {
            return GetUsersByRoleAsync("Trainees");
        }
        public Task<List<GetUsers>> GetAllTrainers()
        {
            return GetUsersByRoleAsync("Trainers");
        }
    }
}
