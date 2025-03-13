using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string VideoLink { get; set; }
        public string  Tilte { get; set; }

        // public bool IsDelete { get; set; }
        public int LevelId { get; set; }
        public Level level { get; set; }
    }
}
