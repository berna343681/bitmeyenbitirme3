
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.PhoneNumbers.Queries
{

    public class GetPhoneNumbersQuery : IRequest<IDataResult<IEnumerable<PhoneNumber>>>
    {
        public class GetPhoneNumbersQueryHandler : IRequestHandler<GetPhoneNumbersQuery, IDataResult<IEnumerable<PhoneNumber>>>
        {
            private readonly IPhoneNumberRepository _phoneNumberRepository;
            private readonly IMediator _mediator;

            public GetPhoneNumbersQueryHandler(IPhoneNumberRepository phoneNumberRepository, IMediator mediator)
            {
                _phoneNumberRepository = phoneNumberRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<PhoneNumber>>> Handle(GetPhoneNumbersQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<PhoneNumber>>(await _phoneNumberRepository.GetListAsync());
            }
        }
    }
}