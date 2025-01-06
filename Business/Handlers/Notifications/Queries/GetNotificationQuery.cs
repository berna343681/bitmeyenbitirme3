
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Notifications.Queries
{
    public class GetNotificationQuery : IRequest<IDataResult<Notification>>
    {
        public int Id { get; set; }

        public class GetNotificationQueryHandler : IRequestHandler<GetNotificationQuery, IDataResult<Notification>>
        {
            private readonly INotificationRepository _notificationRepository;
            private readonly IMediator _mediator;

            public GetNotificationQueryHandler(INotificationRepository notificationRepository, IMediator mediator)
            {
                _notificationRepository = notificationRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Notification>> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
            {
                var notification = await _notificationRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Notification>(notification);
            }
        }
    }
}
