
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class CompodentRepository : EfEntityRepositoryBase<Compodent, ProjectDbContext>, ICompodentRepository
    {
        public CompodentRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
