
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Orderİtems.ValidationRules;

namespace Business.Handlers.Orderİtems.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrderİtemCommand : IRequest<IResult>
    {

        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public Core.Enums.SizeEnum Size { get; set; }
        public int Quantity { get; set; }


        public class CreateOrderİtemCommandHandler : IRequestHandler<CreateOrderİtemCommand, IResult>
        {
            private readonly IOrderİtemRepository _orderİtemRepository;
            private readonly IMediator _mediator;
            public CreateOrderİtemCommandHandler(IOrderİtemRepository orderİtemRepository, IMediator mediator)
            {
                _orderİtemRepository = orderİtemRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrderİtemValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrderİtemCommand request, CancellationToken cancellationToken)
            {
                var isThereOrderİtemRecord = _orderİtemRepository.Query().Any(u => u.Id == request.Id);

                if (isThereOrderİtemRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrderİtem = new Orderİtem
                {
                    CreatedDate = request.CreatedDate,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = request.LastUpdatedDate,
                    Status = request.Status,
                    IsDeleted = request.IsDeleted,
                    Id = request.Id,
                    ProductName = request.ProductName,
                    Color = request.Color,
                    Size = request.Size,
                    Quantity = request.Quantity,

                };

                _orderİtemRepository.Add(addedOrderİtem);
                await _orderİtemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}