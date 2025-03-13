﻿using FitCore.Models.Nutritionist;
using FitCore.Models.NutritionistAndPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.IRepositories
{
    public interface INutritionistServices
    {
        Task<List<ViewPlans>> GetMyPlans(string id);
        Task<EditPlans> EditPlanAsync(string UserId, EditPlans model , int id);
        Task<ViewPlansById> GetPlanByIdAsync(string UserId, int id);
        Task<bool> DeleteAsync(string UserId, int id);
    }
}
