
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


namespace Business.Handlers.ErrorRaports.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteErrorRaportCommand : IRequest<IResult>
    {
        public string Title { get; set; }

        public class DeleteErrorRaportCommandHandler : IRequestHandler<DeleteErrorRaportCommand, IResult>
        {
            private readonly IErrorRaportRepository _errorRaportRepository;
            private readonly IMediator _mediator;

            public DeleteErrorRaportCommandHandler(IErrorRaportRepository errorRaportRepository, IMediator mediator)
            {
                _errorRaportRepository = errorRaportRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteErrorRaportCommand request, CancellationToken cancellationToken)
            {
                var errorRaportToDelete = _errorRaportRepository.Get(p => p.Title == request.Title);

                _errorRaportRepository.Delete(errorRaportToDelete);
                await _errorRaportRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

