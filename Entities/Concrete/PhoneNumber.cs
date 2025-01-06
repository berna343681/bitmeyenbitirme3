using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class PhoneNumber : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public string Type { get; set; } // e.g., Home, Work, Mobile
        public string Number { get; set; }
        public int CustomerId { get; set; }
    }
}
 