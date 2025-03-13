using Fit.Authorization;
using FitCore.IRepositories;
using FitCore.Models.Admin;
using FitCore.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fit.Controllers
{
    [Route("api/[controller]")]
    [AdminAuthorize]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Get-All-Videos")]
        public async Task<IActionResult> GetAllVideos()
        {
                
            var result = await _unitOfWork.AdminService.GetAllVideoAsync();

            if(result is null || !result.Any()) 
                return NotFound($"No videos found");

            return Ok(result);
        }
        [HttpGet("Get-Video-By-Id")]
        public async Task<IActionResult> GetVideoById(int id)
        {
            var result = await _unitOfWork.AdminService.GetByIdAsync(id);

            if (result is null)
                return NotFound($"No videos found With {id}");

            return Ok(result);
        }
        [HttpPost("Add-Video")]
        public async Task<IActionResult> AddVideo([FromForm] AddAndEditVideo model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.AdminService.AddVideo(model);

            if (result == null)
                return BadRequest("Failed to add video.");


            return Ok(result);
        }

        [HttpPut("Edit-Video/{id}")]
        public async Task<IActionResult> EditVideo([FromForm] AddAndEditVideo model , int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.AdminService.EditVideo(model , id);

            if (result is null)
                return NotFound($"No video found with ID = {id}");


            return Ok(result);
        }

        [HttpDelete("Delete-Video/{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {

            var result = await _unitOfWork.AdminService.DeleteVideoAsync(id);

            if (!result)
                return NotFound($"Video with ID {id} not found.");

            return Ok("Video deleted successfully.");
        }
        
    }
}
