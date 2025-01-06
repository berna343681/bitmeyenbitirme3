
using Business.Handlers.Cartİtems.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Cartİtems.Queries.GetCartİtemQuery;
using Entities.Concrete;
using static Business.Handlers.Cartİtems.Queries.GetCartİtemsQuery;
using static Business.Handlers.Cartİtems.Commands.CreateCartİtemCommand;
using Business.Handlers.Cartİtems.Commands;
using Business.Constants;
using static Business.Handlers.Cartİtems.Commands.UpdateCartİtemCommand;
using static Business.Handlers.Cartİtems.Commands.DeleteCartİtemCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class CartİtemHandlerTests
    {
        Mock<ICartİtemRepository> _cartİtemRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _cartİtemRepository = new Mock<ICartİtemRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Cartİtem_GetQuery_Success()
        {
            //Arrange
            var query = new GetCartİtemQuery();

            _cartİtemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Cartİtem, bool>>>())).ReturnsAsync(new Cartİtem()
//propertyler buraya yazılacak
//{																		
//CartİtemId = 1,
//CartİtemName = "Test"
//}
);

            var handler = new GetCartİtemQueryHandler(_cartİtemRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.CartİtemId.Should().Be(1);

        }

        [Test]
        public async Task Cartİtem_GetQueries_Success()
        {
            //Arrange
            var query = new GetCartİtemsQuery();

            _cartİtemRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Cartİtem, bool>>>()))
                        .ReturnsAsync(new List<Cartİtem> { new Cartİtem() { /*TODO:propertyler buraya yazılacak CartİtemId = 1, CartİtemName = "test"*/ } });

            var handler = new GetCartİtemsQueryHandler(_cartİtemRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Cartİtem>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Cartİtem_CreateCommand_Success()
        {
            Cartİtem rt = null;
            //Arrange
            var command = new CreateCartİtemCommand();
            //propertyler buraya yazılacak
            //command.CartİtemName = "deneme";

            _cartİtemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Cartİtem, bool>>>()))
                        .ReturnsAsync(rt);

            _cartİtemRepository.Setup(x => x.Add(It.IsAny<Cartİtem>())).Returns(new Cartİtem());

            var handler = new CreateCartİtemCommandHandler(_cartİtemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _cartİtemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Cartİtem_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateCartİtemCommand();
            //propertyler buraya yazılacak 
            //command.CartİtemName = "test";

            _cartİtemRepository.Setup(x => x.Query())
                                           .Returns(new List<Cartİtem> { new Cartİtem() { /*TODO:propertyler buraya yazılacak CartİtemId = 1, CartİtemName = "test"*/ } }.AsQueryable());

            _cartİtemRepository.Setup(x => x.Add(It.IsAny<Cartİtem>())).Returns(new Cartİtem());

            var handler = new CreateCartİtemCommandHandler(_cartİtemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Cartİtem_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateCartİtemCommand();
            //command.CartİtemName = "test";

            _cartİtemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Cartİtem, bool>>>()))
                        .ReturnsAsync(new Cartİtem() { /*TODO:propertyler buraya yazılacak CartİtemId = 1, CartİtemName = "deneme"*/ });

            _cartİtemRepository.Setup(x => x.Update(It.IsAny<Cartİtem>())).Returns(new Cartİtem());

            var handler = new UpdateCartİtemCommandHandler(_cartİtemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _cartİtemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Cartİtem_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteCartİtemCommand();

            _cartİtemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Cartİtem, bool>>>()))
                        .ReturnsAsync(new Cartİtem() { /*TODO:propertyler buraya yazılacak CartİtemId = 1, CartİtemName = "deneme"*/});

            _cartİtemRepository.Setup(x => x.Delete(It.IsAny<Cartİtem>()));

            var handler = new DeleteCartİtemCommandHandler(_cartİtemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _cartİtemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

