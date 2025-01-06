using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Cart : BaseEntity, IEntity
    {
        public int Id { get; set; } // Sepet ID
        public int UserId { get; set; } // Kullanıcı kimliği
    }
}
