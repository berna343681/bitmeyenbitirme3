
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
using Business.Handlers.WareHouses.ValidationRules;


namespace Business.Handlers.WareHouses.Commands
{


    public class UpdateWareHouseCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        public Core.Enums.SizeEnum Size { get; set; }
        public int Quantity { get; set; }

        public class UpdateWareHouseCommandHandler : IRequestHandler<UpdateWareHouseCommand, IResult>
        {
            private readonly IWareHouseRepository _wareHouseRepository;
            private readonly IMediator _mediator;

            public UpdateWareHouseCommandHandler(IWareHouseRepository wareHouseRepository, IMediator mediator)
            {
                _wareHouseRepository = wareHouseRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateWareHouseValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateWareHouseCommand request, CancellationToken cancellationToken)
            {
                var isThereWareHouseRecord = await _wareHouseRepository.GetAsync(u => u.Id == request.Id);


                isThereWareHouseRecord.CreatedDate = request.CreatedDate;
                isThereWareHouseRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereWareHouseRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereWareHouseRecord.Status = request.Status;
                isThereWareHouseRecord.IsDeleted = request.IsDeleted;
                isThereWareHouseRecord.Id = request.Id;
                isThereWareHouseRecord.Category = request.Category;
                isThereWareHouseRecord.ProductName = request.ProductName;
                isThereWareHouseRecord.ColorName = request.ColorName;
                isThereWareHouseRecord.Size = request.Size;
                isThereWareHouseRecord.Quantity = request.Quantity;


                _wareHouseRepository.Update(isThereWareHouseRecord);
                await _wareHouseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

