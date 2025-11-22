using BaseBusiness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Services.Interfaces
{
    public interface IBrandService
    {
        List<BrandModel> GetAll();
        BrandModel GetById(long id);
        bool Create(BrandModel model);
        bool Update(BrandModel model);
        bool Delete(long id);
    }
}
