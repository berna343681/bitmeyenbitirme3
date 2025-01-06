
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
using Business.Handlers.PhoneNumbers.ValidationRules;


namespace Business.Handlers.PhoneNumbers.Commands
{


    public class UpdatePhoneNumberCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public int CustomerId { get; set; }

        public class UpdatePhoneNumberCommandHandler : IRequestHandler<UpdatePhoneNumberCommand, IResult>
        {
            private readonly IPhoneNumberRepository _phoneNumberRepository;
            private readonly IMediator _mediator;

            public UpdatePhoneNumberCommandHandler(IPhoneNumberRepository phoneNumberRepository, IMediator mediator)
            {
                _phoneNumberRepository = phoneNumberRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdatePhoneNumberValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdatePhoneNumberCommand request, CancellationToken cancellationToken)
            {
                var isTherePhoneNumberRecord = await _phoneNumberRepository.GetAsync(u => u.Id == request.Id);


                isTherePhoneNumberRecord.CreatedDate = request.CreatedDate;
                isTherePhoneNumberRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isTherePhoneNumberRecord.LastUpdatedDate = request.LastUpdatedDate;
                isTherePhoneNumberRecord.Status = request.Status;
                isTherePhoneNumberRecord.IsDeleted = request.IsDeleted;
                isTherePhoneNumberRecord.Id = request.Id;
                isTherePhoneNumberRecord.Type = request.Type;
                isTherePhoneNumberRecord.Number = request.Number;
                isTherePhoneNumberRecord.CustomerId = request.CustomerId;


                _phoneNumberRepository.Update(isTherePhoneNumberRecord);
                await _phoneNumberRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

