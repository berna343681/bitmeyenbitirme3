
using Business.Handlers.Orderİtems.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Orderİtems.Queries.GetOrderİtemQuery;
using Entities.Concrete;
using static Business.Handlers.Orderİtems.Queries.GetOrderİtemsQuery;
using static Business.Handlers.Orderİtems.Commands.CreateOrderİtemCommand;
using Business.Handlers.Orderİtems.Commands;
using Business.Constants;
using static Business.Handlers.Orderİtems.Commands.UpdateOrderİtemCommand;
using static Business.Handlers.Orderİtems.Commands.DeleteOrderİtemCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrderİtemHandlerTests
    {
        Mock<IOrderİtemRepository> _orderİtemRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orderİtemRepository = new Mock<IOrderİtemRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Orderİtem_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrderİtemQuery();

            _orderİtemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Orderİtem, bool>>>())).ReturnsAsync(new Orderİtem()
//propertyler buraya yazılacak
//{																		
//OrderİtemId = 1,
//OrderİtemName = "Test"
//}
);

            var handler = new GetOrderİtemQueryHandler(_orderİtemRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrderİtemId.Should().Be(1);

        }

        [Test]
        public async Task Orderİtem_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrderİtemsQuery();

            _orderİtemRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Orderİtem, bool>>>()))
                        .ReturnsAsync(new List<Orderİtem> { new Orderİtem() { /*TODO:propertyler buraya yazılacak OrderİtemId = 1, OrderİtemName = "test"*/ } });

            var handler = new GetOrderİtemsQueryHandler(_orderİtemRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Orderİtem>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Orderİtem_CreateCommand_Success()
        {
            Orderİtem rt = null;
            //Arrange
            var command = new CreateOrderİtemCommand();
            //propertyler buraya yazılacak
            //command.OrderİtemName = "deneme";

            _orderİtemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Orderİtem, bool>>>()))
                        .ReturnsAsync(rt);

            _orderİtemRepository.Setup(x => x.Add(It.IsAny<Orderİtem>())).Returns(new Orderİtem());

            var handler = new CreateOrderİtemCommandHandler(_orderİtemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orderİtemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Orderİtem_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrderİtemCommand();
            //propertyler buraya yazılacak 
            //command.OrderİtemName = "test";

            _orderİtemRepository.Setup(x => x.Query())
                                           .Returns(new List<Orderİtem> { new Orderİtem() { /*TODO:propertyler buraya yazılacak OrderİtemId = 1, OrderİtemName = "test"*/ } }.AsQueryable());

            _orderİtemRepository.Setup(x => x.Add(It.IsAny<Orderİtem>())).Returns(new Orderİtem());

            var handler = new CreateOrderİtemCommandHandler(_orderİtemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Orderİtem_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrderİtemCommand();
            //command.OrderİtemName = "test";

            _orderİtemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Orderİtem, bool>>>()))
                        .ReturnsAsync(new Orderİtem() { /*TODO:propertyler buraya yazılacak OrderİtemId = 1, OrderİtemName = "deneme"*/ });

            _orderİtemRepository.Setup(x => x.Update(It.IsAny<Orderİtem>())).Returns(new Orderİtem());

            var handler = new UpdateOrderİtemCommandHandler(_orderİtemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orderİtemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Orderİtem_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrderİtemCommand();

            _orderİtemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Orderİtem, bool>>>()))
                        .ReturnsAsync(new Orderİtem() { /*TODO:propertyler buraya yazılacak OrderİtemId = 1, OrderİtemName = "deneme"*/});

            _orderİtemRepository.Setup(x => x.Delete(It.IsAny<Orderİtem>()));

            var handler = new DeleteOrderİtemCommandHandler(_orderİtemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orderİtemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

