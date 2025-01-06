
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Notifications.ValidationRules;


namespace Business.Handlers.Notifications.Commands
{


    public class UpdateNotificationCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public int ErrorLogId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string NotificationType { get; set; }

        public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, IResult>
        {
            private readonly INotificationRepository _notificationRepository;
            private readonly IMediator _mediator;

            public UpdateNotificationCommandHandler(INotificationRepository notificationRepository, IMediator mediator)
            {
                _notificationRepository = notificationRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateNotificationValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
            {
                var isThereNotificationRecord = await _notificationRepository.GetAsync(u => u.Id == request.Id);


                isThereNotificationRecord.CreatedDate = request.CreatedDate;
                isThereNotificationRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereNotificationRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereNotificationRecord.Status = request.Status;
                isThereNotificationRecord.IsDeleted = request.IsDeleted;
                isThereNotificationRecord.Id = request.Id;
                isThereNotificationRecord.Title = request.Title;
                isThereNotificationRecord.ErrorLogId = request.ErrorLogId;
                isThereNotificationRecord.Message = request.Message;
                isThereNotificationRecord.IsRead = request.IsRead;
                isThereNotificationRecord.NotificationType = request.NotificationType;


                _notificationRepository.Update(isThereNotificationRecord);
                await _notificationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

