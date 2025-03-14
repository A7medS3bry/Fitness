using Fit.Authorization;
using FitCore.IRepositories;
using FitCore.Models.Authentication;
using FitCore.Models.NutritionistAndPlan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [NutritionistAuthorizeAttribute]
    public class NutritionistController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public NutritionistController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Get-All-Plans")]
        public async Task<IActionResult> GetPlans()
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.NutritionistServices.GetMyPlans(userId);

            if (result is null)
                return BadRequest("No Plans Find");

            return Ok(result);
        }
        
        [HttpGet("Get-By-Id")]
        public async Task<IActionResult> GetPlansById(int id)
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.NutritionistServices.GetPlanByIdAsync(userId , id);

            if (result is null)
                return BadRequest("No Plans Find");

            return Ok(result);

        }
        [HttpPost("Edit-Plan")]
        public async Task<IActionResult> EditPlan([FromForm] EditPlans model ,int id)
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.NutritionistServices.EditPlanAsync(userId , model, id);
            if (result == null)
                return NotFound($"No plan found with ID: {id}");

            return Ok(result);
        }

        [HttpPost("Delete-Plan")]
        public async Task<IActionResult> DeletePlan([FromForm]int id)
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.NutritionistServices.DeleteAsync(userId , id);
            if (!result)
                return NotFound($"No plan found with ID: {id}");

            return Ok($"Plan With ID: {id} Deleted");

        }
    }
}
