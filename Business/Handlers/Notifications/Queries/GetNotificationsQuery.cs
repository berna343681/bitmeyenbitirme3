
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

namespace Business.Handlers.Notifications.Queries
{

    public class GetNotificationsQuery : IRequest<IDataResult<IEnumerable<Notification>>>
    {
        public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, IDataResult<IEnumerable<Notification>>>
        {
            private readonly INotificationRepository _notificationRepository;
            private readonly IMediator _mediator;

            public GetNotificationsQueryHandler(INotificationRepository notificationRepository, IMediator mediator)
            {
                _notificationRepository = notificationRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Notification>>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Notification>>(await _notificationRepository.GetListAsync());
            }
        }
    }
}