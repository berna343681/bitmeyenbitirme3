
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.FeedBacks.Queries
{

    public class GetFeedBacksQuery : IRequest<IDataResult<IEnumerable<FeedBack>>>
    {
        public class GetFeedBacksQueryHandler : IRequestHandler<GetFeedBacksQuery, IDataResult<IEnumerable<FeedBack>>>
        {
            private readonly IFeedBackRepository _feedBackRepository;
            private readonly IMediator _mediator;

            public GetFeedBacksQueryHandler(IFeedBackRepository feedBackRepository, IMediator mediator)
            {
                _feedBackRepository = feedBackRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<FeedBack>>> Handle(GetFeedBacksQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<FeedBack>>(await _feedBackRepository.GetListAsync());
            }
        }
    }
}