﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthServices AuthServices { get; }
        ISearch Search { get; }
        INutritionistServices NutritionistServices { get; }
        IAdminService AdminService { get; }

        public int Complete();
    }
}
