
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
using Business.Handlers.Carts.ValidationRules;

namespace Business.Handlers.Carts.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateCartCommand : IRequest<IResult>
    {

        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }


        public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, IResult>
        {
            private readonly ICartRepository _cartRepository;
            private readonly IMediator _mediator;
            public CreateCartCommandHandler(ICartRepository cartRepository, IMediator mediator)
            {
                _cartRepository = cartRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateCartValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateCartCommand request, CancellationToken cancellationToken)
            {
                var isThereCartRecord = _cartRepository.Query().Any(u => u.Id == request.Id);

                if (isThereCartRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedCart = new Cart
                {
                    CreatedDate = request.CreatedDate,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = request.LastUpdatedDate,
                    Status = request.Status,
                    IsDeleted = request.IsDeleted,
                    Id = request.Id,
                    UserId = request.UserId,

                };

                _cartRepository.Add(addedCart);
                await _cartRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}