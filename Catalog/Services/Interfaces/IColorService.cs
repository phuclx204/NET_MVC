using BaseBusiness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Services.Interfaces
{
    public interface IColorService
    {
        Task<List<ColorModel>> GetAll();
        Task<ColorModel> GetById(long id);
        Task<bool> Create(ColorModel model);
        Task<bool> Update(ColorModel model);
        Task<bool> Delete(long id);
    }
}
