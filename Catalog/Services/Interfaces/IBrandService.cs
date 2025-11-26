using BaseBusiness.Model;

namespace Catalog.Services.Interfaces
{
    public interface IBrandService
    {
        Task<bool> Create(BrandModel model);
        Task<bool> Update(BrandModel model);
        Task<bool> Delete(long id);
        Task<List<BrandModel>> GetAll();
        Task<BrandModel> GetById(long id);
    }
}
