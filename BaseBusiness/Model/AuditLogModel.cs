using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBusiness.Model
{
    public class AuditLog
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Action { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserModel User { get; set; }
    }
}
