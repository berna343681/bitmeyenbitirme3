using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Address : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string Avenue { get; set; }
        public string StreetNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string FullAddress { get; set; }
        
    }
}
