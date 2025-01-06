
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.FeedBacks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteFeedBackCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public class DeleteFeedBackCommandHandler : IRequestHandler<DeleteFeedBackCommand, IResult>
        {
            private readonly IFeedBackRepository _feedBackRepository;
            private readonly IMediator _mediator;

            public DeleteFeedBackCommandHandler(IFeedBackRepository feedBackRepository, IMediator mediator)
            {
                _feedBackRepository = feedBackRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteFeedBackCommand request, CancellationToken cancellationToken)
            {
                var feedBackToDelete = _feedBackRepository.Get(p => p.Id == request.Id && p.UserId == request.UserId);

                _feedBackRepository.Delete(feedBackToDelete);
                await _feedBackRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

