﻿
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
using Business.Handlers.Carts.ValidationRules;


namespace Business.Handlers.Carts.Commands
{


    public class UpdateCartCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }

        public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, IResult>
        {
            private readonly ICartRepository _cartRepository;
            private readonly IMediator _mediator;

            public UpdateCartCommandHandler(ICartRepository cartRepository, IMediator mediator)
            {
                _cartRepository = cartRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateCartValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
            {
                var isThereCartRecord = await _cartRepository.GetAsync(u => u.Id == request.Id);


                isThereCartRecord.CreatedDate = request.CreatedDate;
                isThereCartRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereCartRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereCartRecord.Status = request.Status;
                isThereCartRecord.IsDeleted = request.IsDeleted;
                isThereCartRecord.Id = request.Id;
                isThereCartRecord.UserId = request.UserId;


                _cartRepository.Update(isThereCartRecord);
                await _cartRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

