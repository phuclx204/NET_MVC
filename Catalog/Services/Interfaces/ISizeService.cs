using BaseBusiness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Services.Interfaces
{
    public interface ISizeService
    {
        Task<List<SizeModel>> GetAll();
        Task<SizeModel> GetById(long id);
        Task<bool> Create(SizeModel model);
        Task<bool> Update(SizeModel model);
        Task<bool> Delete(long id);
    }
}
