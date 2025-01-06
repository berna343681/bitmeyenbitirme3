
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

namespace Business.Handlers.Orderİtems.Queries
{

    public class GetOrderİtemsQuery : IRequest<IDataResult<IEnumerable<Orderİtem>>>
    {
        public class GetOrderİtemsQueryHandler : IRequestHandler<GetOrderİtemsQuery, IDataResult<IEnumerable<Orderİtem>>>
        {
            private readonly IOrderİtemRepository _orderİtemRepository;
            private readonly IMediator _mediator;

            public GetOrderİtemsQueryHandler(IOrderİtemRepository orderİtemRepository, IMediator mediator)
            {
                _orderİtemRepository = orderİtemRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Orderİtem>>> Handle(GetOrderİtemsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Orderİtem>>(await _orderİtemRepository.GetListAsync());
            }
        }
    }
}