using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FitCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public int? Gender { get; set; }
        public int? ExperienceYears { get; set; }

        // One To Many With Levels
        public int? LevelId { get; set; }
        [JsonIgnore]
        public Level? level { get; set; }

        // Nutritionplan one trainee has one plan
        public NutritionPlans NutritionPlan { get; set; }

        // Nutritionist can Create Many Plan
        public ICollection<NutritionPlans> CreatedNutritionPlans { get; set; }
    }
}
