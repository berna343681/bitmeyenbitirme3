
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Compodents.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteCompodentCommand : IRequest<IResult>
    {
        public string CompodentName { get; set; }

        public class DeleteCompodentCommandHandler : IRequestHandler<DeleteCompodentCommand, IResult>
        {
            private readonly ICompodentRepository _compodentRepository;
            private readonly IMediator _mediator;

            public DeleteCompodentCommandHandler(ICompodentRepository compodentRepository, IMediator mediator)
            {
                _compodentRepository = compodentRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteCompodentCommand request, CancellationToken cancellationToken)
            {
                var compodentToDelete = _compodentRepository.Get(p => p.ComponentName == request.CompodentName);

                _compodentRepository.Delete(compodentToDelete);
                await _compodentRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

