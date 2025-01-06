
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Compodents.Queries
{
    public class GetCompodentQuery : IRequest<IDataResult<Compodent>>
    {
        public int Id { get; set; }

        public class GetCompodentQueryHandler : IRequestHandler<GetCompodentQuery, IDataResult<Compodent>>
        {
            private readonly ICompodentRepository _compodentRepository;
            private readonly IMediator _mediator;

            public GetCompodentQueryHandler(ICompodentRepository compodentRepository, IMediator mediator)
            {
                _compodentRepository = compodentRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Compodent>> Handle(GetCompodentQuery request, CancellationToken cancellationToken)
            {
                var compodent = await _compodentRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Compodent>(compodent);
            }
        }
    }
}
