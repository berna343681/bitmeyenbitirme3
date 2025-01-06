using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ErrorRaport : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } //başlık
        public string UserName { get; set; } // raporu oluşturan kullanıcı adı
        public string Severity { get; set; } // hatanın ciddiyet seviyesi ( kritik /orta /düşük)
        public string ComponentName { get; set; } // hangi comdodente ait
        public string Description { get; set; } // raorun detaylı açıklaması 
        public string ErrorTypeName { get; set; } // kullanıcı hatası / sistem hatası 
    }
}
