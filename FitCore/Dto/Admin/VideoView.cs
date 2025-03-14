using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Dto.Admin
{
    public class VideoView
    {
        public int Id { get; set; }
        public string VideoLink { get; set; }
        public string Tilte { get; set; }
        public int Level { get; set; }
        public string LevelText =>
            Level == 1 ? "Beginner" :
            Level == 2 ? "Intermediate" :
            Level == 3 ? "Advanced" :
            "Unknown";
    }
}
