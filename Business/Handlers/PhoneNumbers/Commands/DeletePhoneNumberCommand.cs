
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.PhoneNumbers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletePhoneNumberCommand : IRequest<IResult>
    {
        public string Type { get; set; }

        public class DeletePhoneNumberCommandHandler : IRequestHandler<DeletePhoneNumberCommand, IResult>
        {
            private readonly IPhoneNumberRepository _phoneNumberRepository;
            private readonly IMediator _mediator;

            public DeletePhoneNumberCommandHandler(IPhoneNumberRepository phoneNumberRepository, IMediator mediator)
            {
                _phoneNumberRepository = phoneNumberRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeletePhoneNumberCommand request, CancellationToken cancellationToken)
            {
                var phoneNumberToDelete = _phoneNumberRepository.Get(p => p.Type == request.Type);

                _phoneNumberRepository.Delete(phoneNumberToDelete);
                await _phoneNumberRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

