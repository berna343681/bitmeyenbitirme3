
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
using Business.Handlers.PhoneNumbers.ValidationRules;

namespace Business.Handlers.PhoneNumbers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePhoneNumberCommand : IRequest<IResult>
    {

        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public int CustomerId { get; set; }


        public class CreatePhoneNumberCommandHandler : IRequestHandler<CreatePhoneNumberCommand, IResult>
        {
            private readonly IPhoneNumberRepository _phoneNumberRepository;
            private readonly IMediator _mediator;
            public CreatePhoneNumberCommandHandler(IPhoneNumberRepository phoneNumberRepository, IMediator mediator)
            {
                _phoneNumberRepository = phoneNumberRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreatePhoneNumberValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePhoneNumberCommand request, CancellationToken cancellationToken)
            {
                var isTherePhoneNumberRecord = _phoneNumberRepository.Query().Any(u => u.Id == request.Id);

                if (isTherePhoneNumberRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedPhoneNumber = new PhoneNumber
                {
                    CreatedDate = request.CreatedDate,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = request.LastUpdatedDate,
                    Status = request.Status,
                    IsDeleted = request.IsDeleted,
                    Id = request.Id,
                    Type = request.Type,
                    Number = request.Number,
                    CustomerId = request.CustomerId,

                };

                _phoneNumberRepository.Add(addedPhoneNumber);
                await _phoneNumberRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}