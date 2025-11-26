using BaseBusiness.Model;

namespace BaseBusiness.base_class
{
    public interface IEntity
    {
        long Id { get; set; }
    }

    public abstract class BaseModel : IEntity
    {
        public long Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; } = 1;
    }
    public abstract class FullAuditModel : BaseModel
    {
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        public UserModel CreatedByUser { get; set; }
        public UserModel UpdatedByUser { get; set; }
    }
}
