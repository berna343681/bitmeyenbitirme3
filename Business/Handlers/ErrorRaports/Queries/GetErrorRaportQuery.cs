
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ErrorRaports.Queries
{
    public class GetErrorRaportQuery : IRequest<IDataResult<ErrorRaport>>
    {
        public int Id { get; set; }

        public class GetErrorRaportQueryHandler : IRequestHandler<GetErrorRaportQuery, IDataResult<ErrorRaport>>
        {
            private readonly IErrorRaportRepository _errorRaportRepository;
            private readonly IMediator _mediator;

            public GetErrorRaportQueryHandler(IErrorRaportRepository errorRaportRepository, IMediator mediator)
            {
                _errorRaportRepository = errorRaportRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ErrorRaport>> Handle(GetErrorRaportQuery request, CancellationToken cancellationToken)
            {
                var errorRaport = await _errorRaportRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<ErrorRaport>(errorRaport);
            }
        }
    }
}
