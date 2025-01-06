﻿
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Notifications.ValidationRules;

namespace Business.Handlers.Notifications.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateNotificationCommand : IRequest<IResult>
    {

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


        public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, IResult>
        {
            private readonly INotificationRepository _notificationRepository;
            private readonly IMediator _mediator;
            public CreateNotificationCommandHandler(INotificationRepository notificationRepository, IMediator mediator)
            {
                _notificationRepository = notificationRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateNotificationValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
            {
                var isThereNotificationRecord = _notificationRepository.Query().Any(u => u.Id == request.Id);

                if (isThereNotificationRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedNotification = new Notification
                {
                    CreatedDate = request.CreatedDate,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = request.LastUpdatedDate,
                    Status = request.Status,
                    IsDeleted = request.IsDeleted,
                    Id = request.Id,
                    Title = request.Title,
                    ErrorLogId = request.ErrorLogId,
                    Message = request.Message,
                    IsRead = request.IsRead,
                    NotificationType = request.NotificationType,

                };

                _notificationRepository.Add(addedNotification);
                await _notificationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}