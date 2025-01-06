
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
using Business.Handlers.Orderİtems.ValidationRules;


namespace Business.Handlers.Orderİtems.Commands
{


    public class UpdateOrderİtemCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
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

        public class UpdateOrderİtemCommandHandler : IRequestHandler<UpdateOrderİtemCommand, IResult>
        {
            private readonly IOrderİtemRepository _orderİtemRepository;
            private readonly IMediator _mediator;

            public UpdateOrderİtemCommandHandler(IOrderİtemRepository orderİtemRepository, IMediator mediator)
            {
                _orderİtemRepository = orderİtemRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrderİtemValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrderİtemCommand request, CancellationToken cancellationToken)
            {
                var isThereOrderİtemRecord = await _orderİtemRepository.GetAsync(u => u.Id == request.Id);


                isThereOrderİtemRecord.CreatedDate = request.CreatedDate;
                isThereOrderİtemRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereOrderİtemRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereOrderİtemRecord.Status = request.Status;
                isThereOrderİtemRecord.IsDeleted = request.IsDeleted;
                isThereOrderİtemRecord.Id = request.Id;
                isThereOrderİtemRecord.ProductName = request.ProductName;
                isThereOrderİtemRecord.Color = request.Color;
                isThereOrderİtemRecord.Size = request.Size;
                isThereOrderİtemRecord.Quantity = request.Quantity;


                _orderİtemRepository.Update(isThereOrderİtemRecord);
                await _orderİtemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

