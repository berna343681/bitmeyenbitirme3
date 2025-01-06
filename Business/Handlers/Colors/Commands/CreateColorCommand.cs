
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
using Business.Handlers.Colors.ValidationRules;

namespace Business.Handlers.Colors.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateColorCommand : IRequest<IResult>
    {

        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string ColorName { get; set; }
        public string HexCode { get; set; }


        public class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, IResult>
        {
            private readonly IColorRepository _colorRepository;
            private readonly IMediator _mediator;
            public CreateColorCommandHandler(IColorRepository colorRepository, IMediator mediator)
            {
                _colorRepository = colorRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateColorValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(Core.CrossCuttingConcerns.Logging.Serilog.Loggers.Elasticsearch))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateColorCommand request, CancellationToken cancellationToken)
            {
                var isThereColorRecord = _colorRepository.Query().Any(u => u.Id == request.Id);

                if (isThereColorRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedColor = new Color
                {
                    CreatedDate = request.CreatedDate,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = request.LastUpdatedDate,
                    Status = request.Status,
                    IsDeleted = request.IsDeleted,
                    Id = request.Id,
                    ColorName = request.ColorName,
                    HexCode = request.HexCode,

                };

                _colorRepository.Add(addedColor);
                await _colorRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}