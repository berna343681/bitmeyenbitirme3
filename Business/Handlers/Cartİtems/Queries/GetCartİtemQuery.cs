
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Cartİtems.Queries
{
    public class GetCartİtemQuery : IRequest<IDataResult<Cartİtem>>
    {
        public int Id { get; set; }

        public class GetCartİtemQueryHandler : IRequestHandler<GetCartİtemQuery, IDataResult<Cartİtem>>
        {
            private readonly ICartİtemRepository _cartİtemRepository;
            private readonly IMediator _mediator;

            public GetCartİtemQueryHandler(ICartİtemRepository cartİtemRepository, IMediator mediator)
            {
                _cartİtemRepository = cartİtemRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Cartİtem>> Handle(GetCartİtemQuery request, CancellationToken cancellationToken)
            {
                var cartİtem = await _cartİtemRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Cartİtem>(cartİtem);
            }
        }
    }
}
