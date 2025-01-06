
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
using Business.Handlers.Compodents.ValidationRules;


namespace Business.Handlers.Compodents.Commands
{


    public class UpdateCompodentCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string ComponentName { get; set; }

        public class UpdateCompodentCommandHandler : IRequestHandler<UpdateCompodentCommand, IResult>
        {
            private readonly ICompodentRepository _compodentRepository;
            private readonly IMediator _mediator;

            public UpdateCompodentCommandHandler(ICompodentRepository compodentRepository, IMediator mediator)
            {
                _compodentRepository = compodentRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateCompodentValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateCompodentCommand request, CancellationToken cancellationToken)
            {
                var isThereCompodentRecord = await _compodentRepository.GetAsync(u => u.Id == request.Id);


                isThereCompodentRecord.CreatedDate = request.CreatedDate;
                isThereCompodentRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereCompodentRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereCompodentRecord.Status = request.Status;
                isThereCompodentRecord.IsDeleted = request.IsDeleted;
                isThereCompodentRecord.Id = request.Id;
                isThereCompodentRecord.ComponentName = request.ComponentName;


                _compodentRepository.Update(isThereCompodentRecord);
                await _compodentRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

