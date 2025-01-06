
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class WareHouseRepository : EfEntityRepositoryBase<WareHouse, ProjectDbContext>, IWareHouseRepository
    {
        public WareHouseRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
