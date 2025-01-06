
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
using Business.Handlers.Addresses.ValidationRules;


namespace Business.Handlers.Addresses.Commands
{


    public class UpdateAddressCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string Header { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string Avenue { get; set; }
        public string StreetNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string FullAddress { get; set; }

        public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, IResult>
        {
            private readonly IAddressRepository _addressRepository;
            private readonly IMediator _mediator;

            public UpdateAddressCommandHandler(IAddressRepository addressRepository, IMediator mediator)
            {
                _addressRepository = addressRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateAddressValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
            {
                var isThereAddressRecord = await _addressRepository.GetAsync(u => u.Id == request.Id);


                isThereAddressRecord.CreatedDate = request.CreatedDate;
                isThereAddressRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereAddressRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereAddressRecord.Status = request.Status;
                isThereAddressRecord.IsDeleted = request.IsDeleted;
                isThereAddressRecord.Id = request.Id;
                isThereAddressRecord.Header = request.Header;
                isThereAddressRecord.Street = request.Street;
                isThereAddressRecord.Neighborhood = request.Neighborhood;
                isThereAddressRecord.Avenue = request.Avenue;
                isThereAddressRecord.StreetNumber = request.StreetNumber;
                isThereAddressRecord.ApartmentNumber = request.ApartmentNumber;
                isThereAddressRecord.City = request.City;
                isThereAddressRecord.Country = request.Country;
                isThereAddressRecord.FullAddress = request.FullAddress;


                _addressRepository.Update(isThereAddressRecord);
                await _addressRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

