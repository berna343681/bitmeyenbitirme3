
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Cartİtems.ValidationRules;


namespace Business.Handlers.Cartİtems.Commands
{


    public class UpdateCartİtemCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
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

        public class UpdateCartİtemCommandHandler : IRequestHandler<UpdateCartİtemCommand, IResult>
        {
            private readonly ICartİtemRepository _cartİtemRepository;
            private readonly IMediator _mediator;

            public UpdateCartİtemCommandHandler(ICartİtemRepository cartİtemRepository, IMediator mediator)
            {
                _cartİtemRepository = cartİtemRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateCartİtemValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateCartİtemCommand request, CancellationToken cancellationToken)
            {
                var isThereCartİtemRecord = await _cartİtemRepository.GetAsync(u => u.Id == request.Id);


                isThereCartİtemRecord.CreatedDate = request.CreatedDate;
                isThereCartİtemRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereCartİtemRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereCartİtemRecord.Status = request.Status;
                isThereCartİtemRecord.IsDeleted = request.IsDeleted;
                isThereCartİtemRecord.Id = request.Id;
                isThereCartİtemRecord.ProductId = request.ProductId;
                isThereCartİtemRecord.ProductName = request.ProductName;
                isThereCartİtemRecord.Color = request.Color;
                isThereCartİtemRecord.Size = request.Size;
                isThereCartİtemRecord.Quantity = request.Quantity;


                _cartİtemRepository.Update(isThereCartİtemRecord);
                await _cartİtemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

