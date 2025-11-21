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
        List<SizeModel> GetAll();
        SizeModel GetById(long id);
        bool Create(SizeModel model);
        bool Update(SizeModel model);
        bool Delete(long id);
    }
}
