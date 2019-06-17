using System.Collections.Generic;
using System.Threading.Tasks;
using Fox.Category.Models.ViewModels.Menu;

namespace Fox.Category.Infrastructure.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuResponse>> GetAll();
    }
}
