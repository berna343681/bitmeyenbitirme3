
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class FeedBackRepository : EfEntityRepositoryBase<FeedBack, ProjectDbContext>, IFeedBackRepository
    {
        public FeedBackRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
