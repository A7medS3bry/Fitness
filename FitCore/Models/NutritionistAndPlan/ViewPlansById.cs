﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Models.NutritionistAndPlan
{
    public class ViewPlansById
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string TraineeId { get; set; }
        public string? TraineeName { get; set; }
        public string NutritionistId { get; set; }
        public string? NutritionistName { get; set; }
    }
}
