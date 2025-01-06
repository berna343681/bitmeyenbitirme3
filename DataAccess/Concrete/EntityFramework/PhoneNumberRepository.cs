
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class PhoneNumberRepository : EfEntityRepositoryBase<PhoneNumber, ProjectDbContext>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
