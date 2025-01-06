
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


namespace Business.Handlers.Orderİtems.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrderİtemCommand : IRequest<IResult>
    {
        public string ProductName { get; set; }
        public int Id { get; set; }

        public class DeleteOrderİtemCommandHandler : IRequestHandler<DeleteOrderİtemCommand, IResult>
        {
            private readonly IOrderİtemRepository _orderİtemRepository;
            private readonly IMediator _mediator;

            public DeleteOrderİtemCommandHandler(IOrderİtemRepository orderİtemRepository, IMediator mediator)
            {
                _orderİtemRepository = orderİtemRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrderİtemCommand request, CancellationToken cancellationToken)
            {
                var orderİtemToDelete = _orderİtemRepository.Get(p => p.ProductName == request.ProductName && p.Id == request.Id);

                _orderİtemRepository.Delete(orderİtemToDelete);
                await _orderİtemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

