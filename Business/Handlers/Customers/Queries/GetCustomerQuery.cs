
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Customers.Queries
{
    public class GetCustomerQuery : IRequest<IDataResult<Customer>>
    {
        public int CreatedUserId { get; set; }

        public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, IDataResult<Customer>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetCustomerQueryHandler(ICustomerRepository customerRepository, IMediator mediator)
            {
                _customerRepository = customerRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Customer>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
            {
                var customer = await _customerRepository.GetAsync(p => p.CreatedUserId == request.CreatedUserId);
                return new SuccessDataResult<Customer>(customer);
            }
        }
    }
}
