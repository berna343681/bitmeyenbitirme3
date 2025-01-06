using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Notification : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ErrorLogId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string NotificationType { get; set; }
    }
}
