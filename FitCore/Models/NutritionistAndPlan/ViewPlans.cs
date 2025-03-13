using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Models.Nutritionist
{
    public class ViewPlans
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string TraineeId { get; set; }
        public string? TraineeName { get; set; }
    }
}
