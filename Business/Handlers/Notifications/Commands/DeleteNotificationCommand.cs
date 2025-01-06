
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


namespace Business.Handlers.Notifications.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteNotificationCommand : IRequest<IResult>
    {
        public string Title { get; set; }

        public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, IResult>
        {
            private readonly INotificationRepository _notificationRepository;
            private readonly IMediator _mediator;

            public DeleteNotificationCommandHandler(INotificationRepository notificationRepository, IMediator mediator)
            {
                _notificationRepository = notificationRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
            {
                var notificationToDelete = _notificationRepository.Get(p => p.Title == request.Title);

                _notificationRepository.Delete(notificationToDelete);
                await _notificationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

