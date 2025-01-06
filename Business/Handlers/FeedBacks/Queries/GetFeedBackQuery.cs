
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.FeedBacks.Queries
{
    public class GetFeedBackQuery : IRequest<IDataResult<FeedBack>>
    {
        public int Id { get; set; }

        public class GetFeedBackQueryHandler : IRequestHandler<GetFeedBackQuery, IDataResult<FeedBack>>
        {
            private readonly IFeedBackRepository _feedBackRepository;
            private readonly IMediator _mediator;

            public GetFeedBackQueryHandler(IFeedBackRepository feedBackRepository, IMediator mediator)
            {
                _feedBackRepository = feedBackRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<FeedBack>> Handle(GetFeedBackQuery request, CancellationToken cancellationToken)
            {
                var feedBack = await _feedBackRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<FeedBack>(feedBack);
            }
        }
    }
}
