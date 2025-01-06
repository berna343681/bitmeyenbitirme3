
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ErrorRaportRepository : EfEntityRepositoryBase<ErrorRaport, ProjectDbContext>, IErrorRaportRepository
    {
        public ErrorRaportRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
