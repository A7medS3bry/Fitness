using FitCore.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.IRepositories
{
    public interface IAdminService
    {
        Task<List<VideoView>> GetAllVideoAsync();
        Task<VideoView> GetByIdAsync(int id);
        Task<VideoView> AddVideo(AddAndEditVideo model);
        Task<AddAndEditVideo> EditVideo(AddAndEditVideo model , int id);
        Task<bool> DeleteVideoAsync(int id);
    }
}
