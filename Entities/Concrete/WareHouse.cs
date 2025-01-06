using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class WareHouse : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        public SizeEnum Size { get; set; }
        public int Quantity { get; set; }
    }
}
