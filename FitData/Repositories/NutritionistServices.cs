using FitCore.Dto.NutritionistAndPlan;
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
    public class NutritionistServices : INutritionistServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public NutritionistServices(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<EditPlans> EditPlanAsync(string UserId, EditPlans model, int id)
        {
            var oldData = await _context.nutritionPlans
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted && b.NutritionistId == UserId);
            if (oldData is null) return null;

            oldData.Name = model.Name;
            oldData.Details = model.Details;

            await _context.SaveChangesAsync();

            return new EditPlans
                    {
                        Name = oldData.Name,
                        Details = oldData.Details
                    };
        }
        public async Task<List<ViewPlans>> GetMyPlans(string id)
        {
            var result = await _context.nutritionPlans.Where(b=>b.NutritionistId == id && !b.IsDeleted  )
                .Select(plan => new ViewPlans
                {
                    Id = plan.Id,
                    Name = plan.Name,
                    Details = plan.Details,
                    TraineeId = plan.TraineeId,
                    TraineeName = plan.Trainee != null ? plan.Trainee.FirstName + " " + plan.Trainee.LastName : ""
                })
                .ToListAsync();
            return result;
        }
        public async Task<ViewPlansById> GetPlanByIdAsync(string UserId, int id)
        {
            var plan = await _context.nutritionPlans
                 .Include(p => p.Trainee)
                 .Include(p=>p.Nutritionist)
                 .Where(b=> !b.IsDeleted)
                 .FirstOrDefaultAsync(p => p.Id == id && p.NutritionistId == UserId);

            if (plan == null) return null;

            return new ViewPlansById
            {
                Id = plan.Id,
                Name = plan.Name,
                Details = plan.Details,
                TraineeId = plan.TraineeId,
                NutritionistId = plan.NutritionistId,
                TraineeName = plan.Trainee != null ? plan.Trainee.FirstName + " " + plan.Trainee.LastName : "",
                NutritionistName = plan.Nutritionist != null ? plan.Nutritionist.FirstName + " " + plan.Nutritionist.LastName : "",
            };
        }
        public async Task<bool> DeleteAsync(string UserId, int id)
        {
            var plan = await _context.nutritionPlans.FirstOrDefaultAsync(b=>b.Id == id && b.NutritionistId == UserId);

            if (plan is null)
                return false;
            plan.IsDeleted = true;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
