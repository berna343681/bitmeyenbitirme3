
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
using Business.Handlers.FeedBacks.ValidationRules;

namespace Business.Handlers.FeedBacks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateFeedBackCommand : IRequest<IResult>
    {

        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FeedbackMessage { get; set; }


        public class CreateFeedBackCommandHandler : IRequestHandler<CreateFeedBackCommand, IResult>
        {
            private readonly IFeedBackRepository _feedBackRepository;
            private readonly IMediator _mediator;
            public CreateFeedBackCommandHandler(IFeedBackRepository feedBackRepository, IMediator mediator)
            {
                _feedBackRepository = feedBackRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateFeedBackValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateFeedBackCommand request, CancellationToken cancellationToken)
            {
                var isThereFeedBackRecord = _feedBackRepository.Query().Any(u => u.Id == request.Id);

                if (isThereFeedBackRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedFeedBack = new FeedBack
                {
                    CreatedDate = request.CreatedDate,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = request.LastUpdatedDate,
                    Status = request.Status,
                    IsDeleted = request.IsDeleted,
                    Id = request.Id,
                    UserId = request.UserId,
                    FeedbackMessage = request.FeedbackMessage,

                };

                _feedBackRepository.Add(addedFeedBack);
                await _feedBackRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}