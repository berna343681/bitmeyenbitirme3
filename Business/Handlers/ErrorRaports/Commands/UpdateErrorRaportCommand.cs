
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
using Business.Handlers.ErrorRaports.ValidationRules;


namespace Business.Handlers.ErrorRaports.Commands
{


    public class UpdateErrorRaportCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string Severity { get; set; }
        public string ComponentName { get; set; }
        public string Description { get; set; }
        public string ErrorTypeName { get; set; }

        public class UpdateErrorRaportCommandHandler : IRequestHandler<UpdateErrorRaportCommand, IResult>
        {
            private readonly IErrorRaportRepository _errorRaportRepository;
            private readonly IMediator _mediator;

            public UpdateErrorRaportCommandHandler(IErrorRaportRepository errorRaportRepository, IMediator mediator)
            {
                _errorRaportRepository = errorRaportRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateErrorRaportValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateErrorRaportCommand request, CancellationToken cancellationToken)
            {
                var isThereErrorRaportRecord = await _errorRaportRepository.GetAsync(u => u.Id == request.Id);


                isThereErrorRaportRecord.CreatedDate = request.CreatedDate;
                isThereErrorRaportRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereErrorRaportRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereErrorRaportRecord.Status = request.Status;
                isThereErrorRaportRecord.IsDeleted = request.IsDeleted;
                isThereErrorRaportRecord.Id = request.Id;
                isThereErrorRaportRecord.Title = request.Title;
                isThereErrorRaportRecord.UserName = request.UserName;
                isThereErrorRaportRecord.Severity = request.Severity;
                isThereErrorRaportRecord.ComponentName = request.ComponentName;
                isThereErrorRaportRecord.Description = request.Description;
                isThereErrorRaportRecord.ErrorTypeName = request.ErrorTypeName;


                _errorRaportRepository.Update(isThereErrorRaportRecord);
                await _errorRaportRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

