
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

namespace Business.Handlers.Cartİtems.Queries
{

    public class GetCartİtemsQuery : IRequest<IDataResult<IEnumerable<Cartİtem>>>
    {
        public class GetCartİtemsQueryHandler : IRequestHandler<GetCartİtemsQuery, IDataResult<IEnumerable<Cartİtem>>>
        {
            private readonly ICartİtemRepository _cartİtemRepository;
            private readonly IMediator _mediator;

            public GetCartİtemsQueryHandler(ICartİtemRepository cartİtemRepository, IMediator mediator)
            {
                _cartİtemRepository = cartİtemRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Cartİtem>>> Handle(GetCartİtemsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Cartİtem>>(await _cartİtemRepository.GetListAsync());
            }
        }
    }
}