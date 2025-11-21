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
        List<ColorModel> GetAll();
        ColorModel GetById(long id);
        bool Create(ColorModel model);
        bool Update(ColorModel model);
        bool Delete(long id);
    }
}
