using FitCore.Models.Authentication;

namespace Fit.Authorization
{
    public class NutritionistAuthorizeAttribute : RoleAuthorizeAttribute
    {
        public NutritionistAuthorizeAttribute() : base(ApplicationRoles.NutritionistsRole)
        {

        }
    }
}
