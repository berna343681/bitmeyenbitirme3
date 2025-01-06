
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.ErrorRaports.Queries
{

    public class GetErrorRaportsQuery : IRequest<IDataResult<IEnumerable<ErrorRaport>>>
    {
        public class GetErrorRaportsQueryHandler : IRequestHandler<GetErrorRaportsQuery, IDataResult<IEnumerable<ErrorRaport>>>
        {
            private readonly IErrorRaportRepository _errorRaportRepository;
            private readonly IMediator _mediator;

            public GetErrorRaportsQueryHandler(IErrorRaportRepository errorRaportRepository, IMediator mediator)
            {
                _errorRaportRepository = errorRaportRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ErrorRaport>>> Handle(GetErrorRaportsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ErrorRaport>>(await _errorRaportRepository.GetListAsync());
            }
        }
    }
}