
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

namespace Business.Handlers.Compodents.Queries
{

    public class GetCompodentsQuery : IRequest<IDataResult<IEnumerable<Compodent>>>
    {
        public class GetCompodentsQueryHandler : IRequestHandler<GetCompodentsQuery, IDataResult<IEnumerable<Compodent>>>
        {
            private readonly ICompodentRepository _compodentRepository;
            private readonly IMediator _mediator;

            public GetCompodentsQueryHandler(ICompodentRepository compodentRepository, IMediator mediator)
            {
                _compodentRepository = compodentRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Compodent>>> Handle(GetCompodentsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Compodent>>(await _compodentRepository.GetListAsync());
            }
        }
    }
}