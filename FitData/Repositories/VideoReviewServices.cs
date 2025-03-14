using FitCore.Dto.VideoReview;
using FitCore.IRepositories;
using FitCore.Models;
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
    public class VideoReviewServices : IVideoReviewServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public VideoReviewServices(UserManager<ApplicationUser> userManager , ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<VideoReviewResponse> AddVideoReview(NewVideoReview model,string userId)
        {

            var VideoReviewIsExist = await _context.VideoReview.AsNoTracking()
                .AnyAsync(b=>b.VideoId == model.VideoId && b.TraineeId == userId);

            var VideoIsExist = await _context.Video.AsNoTracking()
                .AnyAsync(b => b.Id == model.VideoId);

            if (!VideoIsExist || VideoReviewIsExist)
                return null;
            
            var newVideoReview = new VideoReview();
            newVideoReview.TraineeId = userId;
            newVideoReview.VideoId = model.VideoId;
            newVideoReview.Review = model.Review == 0 ? 1 : model.Review;
            newVideoReview.Description = model.Description;
            
            await _context.VideoReview.AddAsync(newVideoReview);

            await _context.SaveChangesAsync();

            return new VideoReviewResponse
            {
                Id = newVideoReview.Id,
                Review = newVideoReview.Review,
                Description = newVideoReview.Description,
                VideoId = newVideoReview.VideoId,
                TraineeId = newVideoReview.TraineeId
            };
        }

        public async Task<VideoReviewResponse> EditVideoReview(EditVideoReview model, string userId , int videoId)
        {

            var oldData = await _context.VideoReview
                .FirstOrDefaultAsync(b => b.VideoId == videoId && b.TraineeId == userId);

            if (oldData is null)
                return null;

            oldData.Review = model.Review == 0 ? 1 : model.Review;
            oldData.Description = model.Description;


            await _context.SaveChangesAsync();

            return new VideoReviewResponse
            {
                Id = oldData.Id,
                Review = oldData.Review,
                Description = oldData.Description,
                VideoId = oldData.VideoId,
                TraineeId = oldData.TraineeId
            };
        }

    }
}
