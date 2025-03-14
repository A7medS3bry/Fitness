using FitCore.Dto.VideoReview;
using FitCore.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoReviewController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public VideoReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("Add-Video-Review")]
        public async Task<IActionResult> AddVideoReview([FromForm]NewVideoReview model)
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.VideoReviewServices.AddVideoReview(model, userId);

            if(result == null)
                return BadRequest("You have already reviewed this video or the video does not exist.");

            return Ok(new
            {
                message = "Review Created successfully.",
                Result = result
            });
        }

        [HttpPut("Edit-Video-Review")]
        public async Task<IActionResult> EditVideoReview([FromForm] EditVideoReview model, int videoId)
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.VideoReviewServices.EditVideoReview(model, userId ,videoId);

            if (result == null)
                return NotFound("No existing review found for this video to update.");

            return Ok(new
            {
                message = "Review updated successfully.",
                Result = result
            });
        }
    }
}
