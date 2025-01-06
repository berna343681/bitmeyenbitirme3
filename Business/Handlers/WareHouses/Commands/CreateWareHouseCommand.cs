
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
using Business.Handlers.WareHouses.ValidationRules;

namespace Business.Handlers.WareHouses.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateWareHouseCommand : IRequest<IResult>
    {

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


        public class CreateWareHouseCommandHandler : IRequestHandler<CreateWareHouseCommand, IResult>
        {
            private readonly IWareHouseRepository _wareHouseRepository;
            private readonly IMediator _mediator;
            public CreateWareHouseCommandHandler(IWareHouseRepository wareHouseRepository, IMediator mediator)
            {
                _wareHouseRepository = wareHouseRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateWareHouseValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateWareHouseCommand request, CancellationToken cancellationToken)
            {
                var isThereWareHouseRecord = _wareHouseRepository.Query().Any(u => u.Id == request.Id);

                if (isThereWareHouseRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedWareHouse = new WareHouse
                {
                    CreatedDate = request.CreatedDate,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = request.LastUpdatedDate,
                    Status = request.Status,
                    IsDeleted = request.IsDeleted,
                    Id = request.Id,
                    Category = request.Category,
                    ProductName = request.ProductName,
                    ColorName = request.ColorName,
                    Size = request.Size,
                    Quantity = request.Quantity,

                };

                _wareHouseRepository.Add(addedWareHouse);
                await _wareHouseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}