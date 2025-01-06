
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
using Business.Handlers.FeedBacks.ValidationRules;


namespace Business.Handlers.FeedBacks.Commands
{


    public class UpdateFeedBackCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FeedbackMessage { get; set; }

        public class UpdateFeedBackCommandHandler : IRequestHandler<UpdateFeedBackCommand, IResult>
        {
            private readonly IFeedBackRepository _feedBackRepository;
            private readonly IMediator _mediator;

            public UpdateFeedBackCommandHandler(IFeedBackRepository feedBackRepository, IMediator mediator)
            {
                _feedBackRepository = feedBackRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateFeedBackValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateFeedBackCommand request, CancellationToken cancellationToken)
            {
                var isThereFeedBackRecord = await _feedBackRepository.GetAsync(u => u.Id == request.Id);


                isThereFeedBackRecord.CreatedDate = request.CreatedDate;
                isThereFeedBackRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereFeedBackRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereFeedBackRecord.Status = request.Status;
                isThereFeedBackRecord.IsDeleted = request.IsDeleted;
                isThereFeedBackRecord.Id = request.Id;
                isThereFeedBackRecord.UserId = request.UserId;
                isThereFeedBackRecord.FeedbackMessage = request.FeedbackMessage;


                _feedBackRepository.Update(isThereFeedBackRecord);
                await _feedBackRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

