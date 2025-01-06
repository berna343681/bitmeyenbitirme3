
using Business.Handlers.Compodents.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Compodents.Queries.GetCompodentQuery;
using Entities.Concrete;
using static Business.Handlers.Compodents.Queries.GetCompodentsQuery;
using static Business.Handlers.Compodents.Commands.CreateCompodentCommand;
using Business.Handlers.Compodents.Commands;
using Business.Constants;
using static Business.Handlers.Compodents.Commands.UpdateCompodentCommand;
using static Business.Handlers.Compodents.Commands.DeleteCompodentCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class CompodentHandlerTests
    {
        Mock<ICompodentRepository> _compodentRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _compodentRepository = new Mock<ICompodentRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Compodent_GetQuery_Success()
        {
            //Arrange
            var query = new GetCompodentQuery();

            _compodentRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Compodent, bool>>>())).ReturnsAsync(new Compodent()
//propertyler buraya yazılacak
//{																		
//CompodentId = 1,
//CompodentName = "Test"
//}
);

            var handler = new GetCompodentQueryHandler(_compodentRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.CompodentId.Should().Be(1);

        }

        [Test]
        public async Task Compodent_GetQueries_Success()
        {
            //Arrange
            var query = new GetCompodentsQuery();

            _compodentRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Compodent, bool>>>()))
                        .ReturnsAsync(new List<Compodent> { new Compodent() { /*TODO:propertyler buraya yazılacak CompodentId = 1, CompodentName = "test"*/ } });

            var handler = new GetCompodentsQueryHandler(_compodentRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Compodent>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Compodent_CreateCommand_Success()
        {
            Compodent rt = null;
            //Arrange
            var command = new CreateCompodentCommand();
            //propertyler buraya yazılacak
            //command.CompodentName = "deneme";

            _compodentRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Compodent, bool>>>()))
                        .ReturnsAsync(rt);

            _compodentRepository.Setup(x => x.Add(It.IsAny<Compodent>())).Returns(new Compodent());

            var handler = new CreateCompodentCommandHandler(_compodentRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _compodentRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Compodent_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateCompodentCommand();
            //propertyler buraya yazılacak 
            //command.CompodentName = "test";

            _compodentRepository.Setup(x => x.Query())
                                           .Returns(new List<Compodent> { new Compodent() { /*TODO:propertyler buraya yazılacak CompodentId = 1, CompodentName = "test"*/ } }.AsQueryable());

            _compodentRepository.Setup(x => x.Add(It.IsAny<Compodent>())).Returns(new Compodent());

            var handler = new CreateCompodentCommandHandler(_compodentRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Compodent_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateCompodentCommand();
            //command.CompodentName = "test";

            _compodentRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Compodent, bool>>>()))
                        .ReturnsAsync(new Compodent() { /*TODO:propertyler buraya yazılacak CompodentId = 1, CompodentName = "deneme"*/ });

            _compodentRepository.Setup(x => x.Update(It.IsAny<Compodent>())).Returns(new Compodent());

            var handler = new UpdateCompodentCommandHandler(_compodentRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _compodentRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Compodent_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteCompodentCommand();

            _compodentRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Compodent, bool>>>()))
                        .ReturnsAsync(new Compodent() { /*TODO:propertyler buraya yazılacak CompodentId = 1, CompodentName = "deneme"*/});

            _compodentRepository.Setup(x => x.Delete(It.IsAny<Compodent>()));

            var handler = new DeleteCompodentCommandHandler(_compodentRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _compodentRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

