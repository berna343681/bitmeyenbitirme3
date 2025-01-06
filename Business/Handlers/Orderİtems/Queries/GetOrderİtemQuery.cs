
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Orderİtems.Queries
{
    public class GetOrderİtemQuery : IRequest<IDataResult<Orderİtem>>
    {
        public int Id { get; set; }

        public class GetOrderİtemQueryHandler : IRequestHandler<GetOrderİtemQuery, IDataResult<Orderİtem>>
        {
            private readonly IOrderİtemRepository _orderİtemRepository;
            private readonly IMediator _mediator;

            public GetOrderİtemQueryHandler(IOrderİtemRepository orderİtemRepository, IMediator mediator)
            {
                _orderİtemRepository = orderİtemRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Orderİtem>> Handle(GetOrderİtemQuery request, CancellationToken cancellationToken)
            {
                var orderİtem = await _orderİtemRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Orderİtem>(orderİtem);
            }
        }
    }
}
