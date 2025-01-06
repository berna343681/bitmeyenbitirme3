
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrderİtemRepository : EfEntityRepositoryBase<Orderİtem, ProjectDbContext>, IOrderİtemRepository
    {
        public OrderİtemRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
