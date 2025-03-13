using FitCore.IRepositories;
using FitCore.Models;
using FitCore.Models.Admin;
using FitData.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitData.Repositories
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public AdminService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<List<VideoView>> GetAllVideoAsync()
        {
            var data = await _context.Video
               .Select(b => new VideoView
               {
                   Id = b.Id,
                   Tilte = b.Tilte,
                   VideoLink = b.VideoLink,
                   Level = b.LevelId

               }).AsNoTracking().ToListAsync();

            if (!data.Any()) return data;

            return data;
        }

        public async Task<VideoView> AddVideo(AddAndEditVideo model)
        {
            var newVideo = new Video
            {
                Tilte = model.Tilte,
                VideoLink = model.VideoLink,
                LevelId = model.Level
            };
            await _context.Video.AddAsync(newVideo);

            await _context.SaveChangesAsync();

            return new VideoView
            {
                Id = newVideo.Id,
                Tilte = newVideo.Tilte,
                VideoLink = newVideo.VideoLink,
                Level = newVideo.LevelId
            };

        }

        public async Task<VideoView> GetByIdAsync(int id)
        {
            var result = await _context.Video.Select(b => new VideoView
            {
                Id = b.Id,
                Tilte = b.Tilte,
                VideoLink = b.VideoLink,
                Level = b.LevelId

            }).AsNoTracking().FirstOrDefaultAsync(b=>b.Id == id);

            return result;

        }

        public async Task<AddAndEditVideo> EditVideo(AddAndEditVideo model , int id)
        {
            var oldData = await _context.Video.FindAsync(id);

            if (oldData is null)
                return null;

            oldData.LevelId = model.Level;
            oldData.Tilte = model.Tilte;
            oldData.VideoLink = model.VideoLink;

            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteVideoAsync(int id)
        {
            var video = await _context.Video.FindAsync(id);

            if (video is null)
                return false;

            _context.Video.Remove(video);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
