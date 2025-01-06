
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
using Business.Handlers.Compodents.ValidationRules;

namespace Business.Handlers.Compodents.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateCompodentCommand : IRequest<IResult>
    {

        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string ComponentName { get; set; }


        public class CreateCompodentCommandHandler : IRequestHandler<CreateCompodentCommand, IResult>
        {
            private readonly ICompodentRepository _compodentRepository;
            private readonly IMediator _mediator;
            public CreateCompodentCommandHandler(ICompodentRepository compodentRepository, IMediator mediator)
            {
                _compodentRepository = compodentRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateCompodentValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateCompodentCommand request, CancellationToken cancellationToken)
            {
                var isThereCompodentRecord = _compodentRepository.Query().Any(u => u.Id == request.Id);

                if (isThereCompodentRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedCompodent = new Compodent
                {
                    CreatedDate = request.CreatedDate,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = request.LastUpdatedDate,
                    Status = request.Status,
                    IsDeleted = request.IsDeleted,
                    Id = request.Id,
                    ComponentName = request.ComponentName,

                };

                _compodentRepository.Add(addedCompodent);
                await _compodentRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}