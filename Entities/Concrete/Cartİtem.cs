using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Cartİtem : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } // Ürün adı
        public string Color { get; set; } // Ürün rengi
        public SizeEnum Size { get; set; }
        public int Quantity { get; set; }

    }
}
