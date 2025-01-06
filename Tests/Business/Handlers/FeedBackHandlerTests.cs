
using Business.Handlers.FeedBacks.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.FeedBacks.Queries.GetFeedBackQuery;
using Entities.Concrete;
using static Business.Handlers.FeedBacks.Queries.GetFeedBacksQuery;
using static Business.Handlers.FeedBacks.Commands.CreateFeedBackCommand;
using Business.Handlers.FeedBacks.Commands;
using Business.Constants;
using static Business.Handlers.FeedBacks.Commands.UpdateFeedBackCommand;
using static Business.Handlers.FeedBacks.Commands.DeleteFeedBackCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class FeedBackHandlerTests
    {
        Mock<IFeedBackRepository> _feedBackRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _feedBackRepository = new Mock<IFeedBackRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task FeedBack_GetQuery_Success()
        {
            //Arrange
            var query = new GetFeedBackQuery();

            _feedBackRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<FeedBack, bool>>>())).ReturnsAsync(new FeedBack()
//propertyler buraya yazılacak
//{																		
//FeedBackId = 1,
//FeedBackName = "Test"
//}
);

            var handler = new GetFeedBackQueryHandler(_feedBackRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.FeedBackId.Should().Be(1);

        }

        [Test]
        public async Task FeedBack_GetQueries_Success()
        {
            //Arrange
            var query = new GetFeedBacksQuery();

            _feedBackRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<FeedBack, bool>>>()))
                        .ReturnsAsync(new List<FeedBack> { new FeedBack() { /*TODO:propertyler buraya yazılacak FeedBackId = 1, FeedBackName = "test"*/ } });

            var handler = new GetFeedBacksQueryHandler(_feedBackRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<FeedBack>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task FeedBack_CreateCommand_Success()
        {
            FeedBack rt = null;
            //Arrange
            var command = new CreateFeedBackCommand();
            //propertyler buraya yazılacak
            //command.FeedBackName = "deneme";

            _feedBackRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<FeedBack, bool>>>()))
                        .ReturnsAsync(rt);

            _feedBackRepository.Setup(x => x.Add(It.IsAny<FeedBack>())).Returns(new FeedBack());

            var handler = new CreateFeedBackCommandHandler(_feedBackRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _feedBackRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task FeedBack_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateFeedBackCommand();
            //propertyler buraya yazılacak 
            //command.FeedBackName = "test";

            _feedBackRepository.Setup(x => x.Query())
                                           .Returns(new List<FeedBack> { new FeedBack() { /*TODO:propertyler buraya yazılacak FeedBackId = 1, FeedBackName = "test"*/ } }.AsQueryable());

            _feedBackRepository.Setup(x => x.Add(It.IsAny<FeedBack>())).Returns(new FeedBack());

            var handler = new CreateFeedBackCommandHandler(_feedBackRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task FeedBack_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateFeedBackCommand();
            //command.FeedBackName = "test";

            _feedBackRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<FeedBack, bool>>>()))
                        .ReturnsAsync(new FeedBack() { /*TODO:propertyler buraya yazılacak FeedBackId = 1, FeedBackName = "deneme"*/ });

            _feedBackRepository.Setup(x => x.Update(It.IsAny<FeedBack>())).Returns(new FeedBack());

            var handler = new UpdateFeedBackCommandHandler(_feedBackRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _feedBackRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task FeedBack_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteFeedBackCommand();

            _feedBackRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<FeedBack, bool>>>()))
                        .ReturnsAsync(new FeedBack() { /*TODO:propertyler buraya yazılacak FeedBackId = 1, FeedBackName = "deneme"*/});

            _feedBackRepository.Setup(x => x.Delete(It.IsAny<FeedBack>()));

            var handler = new DeleteFeedBackCommandHandler(_feedBackRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _feedBackRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

