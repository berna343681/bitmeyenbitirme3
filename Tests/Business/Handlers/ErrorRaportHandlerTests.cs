
using Business.Handlers.ErrorRaports.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ErrorRaports.Queries.GetErrorRaportQuery;
using Entities.Concrete;
using static Business.Handlers.ErrorRaports.Queries.GetErrorRaportsQuery;
using static Business.Handlers.ErrorRaports.Commands.CreateErrorRaportCommand;
using Business.Handlers.ErrorRaports.Commands;
using Business.Constants;
using static Business.Handlers.ErrorRaports.Commands.UpdateErrorRaportCommand;
using static Business.Handlers.ErrorRaports.Commands.DeleteErrorRaportCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ErrorRaportHandlerTests
    {
        Mock<IErrorRaportRepository> _errorRaportRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _errorRaportRepository = new Mock<IErrorRaportRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ErrorRaport_GetQuery_Success()
        {
            //Arrange
            var query = new GetErrorRaportQuery();

            _errorRaportRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ErrorRaport, bool>>>())).ReturnsAsync(new ErrorRaport()
//propertyler buraya yazılacak
//{																		
//ErrorRaportId = 1,
//ErrorRaportName = "Test"
//}
);

            var handler = new GetErrorRaportQueryHandler(_errorRaportRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ErrorRaportId.Should().Be(1);

        }

        [Test]
        public async Task ErrorRaport_GetQueries_Success()
        {
            //Arrange
            var query = new GetErrorRaportsQuery();

            _errorRaportRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ErrorRaport, bool>>>()))
                        .ReturnsAsync(new List<ErrorRaport> { new ErrorRaport() { /*TODO:propertyler buraya yazılacak ErrorRaportId = 1, ErrorRaportName = "test"*/ } });

            var handler = new GetErrorRaportsQueryHandler(_errorRaportRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ErrorRaport>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ErrorRaport_CreateCommand_Success()
        {
            ErrorRaport rt = null;
            //Arrange
            var command = new CreateErrorRaportCommand();
            //propertyler buraya yazılacak
            //command.ErrorRaportName = "deneme";

            _errorRaportRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ErrorRaport, bool>>>()))
                        .ReturnsAsync(rt);

            _errorRaportRepository.Setup(x => x.Add(It.IsAny<ErrorRaport>())).Returns(new ErrorRaport());

            var handler = new CreateErrorRaportCommandHandler(_errorRaportRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _errorRaportRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ErrorRaport_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateErrorRaportCommand();
            //propertyler buraya yazılacak 
            //command.ErrorRaportName = "test";

            _errorRaportRepository.Setup(x => x.Query())
                                           .Returns(new List<ErrorRaport> { new ErrorRaport() { /*TODO:propertyler buraya yazılacak ErrorRaportId = 1, ErrorRaportName = "test"*/ } }.AsQueryable());

            _errorRaportRepository.Setup(x => x.Add(It.IsAny<ErrorRaport>())).Returns(new ErrorRaport());

            var handler = new CreateErrorRaportCommandHandler(_errorRaportRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ErrorRaport_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateErrorRaportCommand();
            //command.ErrorRaportName = "test";

            _errorRaportRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ErrorRaport, bool>>>()))
                        .ReturnsAsync(new ErrorRaport() { /*TODO:propertyler buraya yazılacak ErrorRaportId = 1, ErrorRaportName = "deneme"*/ });

            _errorRaportRepository.Setup(x => x.Update(It.IsAny<ErrorRaport>())).Returns(new ErrorRaport());

            var handler = new UpdateErrorRaportCommandHandler(_errorRaportRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _errorRaportRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ErrorRaport_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteErrorRaportCommand();

            _errorRaportRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ErrorRaport, bool>>>()))
                        .ReturnsAsync(new ErrorRaport() { /*TODO:propertyler buraya yazılacak ErrorRaportId = 1, ErrorRaportName = "deneme"*/});

            _errorRaportRepository.Setup(x => x.Delete(It.IsAny<ErrorRaport>()));

            var handler = new DeleteErrorRaportCommandHandler(_errorRaportRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _errorRaportRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

