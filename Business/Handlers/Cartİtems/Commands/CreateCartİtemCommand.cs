
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
using Business.Handlers.Cartİtems.ValidationRules;

namespace Business.Handlers.Cartİtems.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateCartİtemCommand : IRequest<IResult>
    {

        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public Core.Enums.SizeEnum Size { get; set; }
        public int Quantity { get; set; }


        public class CreateCartİtemCommandHandler : IRequestHandler<CreateCartİtemCommand, IResult>
        {
            private readonly ICartİtemRepository _cartİtemRepository;
            private readonly IMediator _mediator;
            public CreateCartİtemCommandHandler(ICartİtemRepository cartİtemRepository, IMediator mediator)
            {
                _cartİtemRepository = cartİtemRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateCartİtemValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateCartİtemCommand request, CancellationToken cancellationToken)
            {
                var isThereCartİtemRecord = _cartİtemRepository.Query().Any(u => u.Id == request.Id);

                if (isThereCartİtemRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedCartİtem = new Cartİtem
                {
                    CreatedDate = request.CreatedDate,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = request.LastUpdatedDate,
                    Status = request.Status,
                    IsDeleted = request.IsDeleted,
                    Id = request.Id,
                    ProductId = request.ProductId,
                    ProductName = request.ProductName,
                    Color = request.Color,
                    Size = request.Size,
                    Quantity = request.Quantity,

                };

                _cartİtemRepository.Add(addedCartİtem);
                await _cartİtemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}