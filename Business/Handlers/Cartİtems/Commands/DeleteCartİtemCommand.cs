
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


namespace Business.Handlers.Cartİtems.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteCartİtemCommand : IRequest<IResult>
    {
        public string ProductName { get; set; }

        public class DeleteCartİtemCommandHandler : IRequestHandler<DeleteCartİtemCommand, IResult>
        {
            private readonly ICartİtemRepository _cartİtemRepository;
            private readonly IMediator _mediator;

            public DeleteCartİtemCommandHandler(ICartİtemRepository cartİtemRepository, IMediator mediator)
            {
                _cartİtemRepository = cartİtemRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteCartİtemCommand request, CancellationToken cancellationToken)
            {
                var cartİtemToDelete = _cartİtemRepository.Get(p => p.ProductName == request.ProductName);

                _cartİtemRepository.Delete(cartİtemToDelete);
                await _cartİtemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

