
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
using Business.Handlers.ErrorRaports.ValidationRules;

namespace Business.Handlers.ErrorRaports.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateErrorRaportCommand : IRequest<IResult>
    {

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


        public class CreateErrorRaportCommandHandler : IRequestHandler<CreateErrorRaportCommand, IResult>
        {
            private readonly IErrorRaportRepository _errorRaportRepository;
            private readonly IMediator _mediator;
            public CreateErrorRaportCommandHandler(IErrorRaportRepository errorRaportRepository, IMediator mediator)
            {
                _errorRaportRepository = errorRaportRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateErrorRaportValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateErrorRaportCommand request, CancellationToken cancellationToken)
            {
                var isThereErrorRaportRecord = _errorRaportRepository.Query().Any(u => u.Id == request.Id);

                if (isThereErrorRaportRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedErrorRaport = new ErrorRaport
                {
                    CreatedDate = request.CreatedDate,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = request.LastUpdatedDate,
                    Status = request.Status,
                    IsDeleted = request.IsDeleted,
                    Id = request.Id,
                    Title = request.Title,
                    UserName = request.UserName,
                    Severity = request.Severity,
                    ComponentName = request.ComponentName,
                    Description = request.Description,
                    ErrorTypeName = request.ErrorTypeName,

                };

                _errorRaportRepository.Add(addedErrorRaport);
                await _errorRaportRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}