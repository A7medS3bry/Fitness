using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Models
{
    public class VideoReview
    {
        public int Id { get; set; }
        public int Review { get; set; }
        public string Description { get; set; }

        public int VideoId { get; set; }
        public Video Video { get; set; }
        public string TraineeId { get; set; }
        public ApplicationUser Trainee { get; set; }
    }
}
