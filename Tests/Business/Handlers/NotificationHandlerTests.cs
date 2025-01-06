
using Business.Handlers.Notifications.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Notifications.Queries.GetNotificationQuery;
using Entities.Concrete;
using static Business.Handlers.Notifications.Queries.GetNotificationsQuery;
using static Business.Handlers.Notifications.Commands.CreateNotificationCommand;
using Business.Handlers.Notifications.Commands;
using Business.Constants;
using static Business.Handlers.Notifications.Commands.UpdateNotificationCommand;
using static Business.Handlers.Notifications.Commands.DeleteNotificationCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class NotificationHandlerTests
    {
        Mock<INotificationRepository> _notificationRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _notificationRepository = new Mock<INotificationRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Notification_GetQuery_Success()
        {
            //Arrange
            var query = new GetNotificationQuery();

            _notificationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Notification, bool>>>())).ReturnsAsync(new Notification()
//propertyler buraya yazılacak
//{																		
//NotificationId = 1,
//NotificationName = "Test"
//}
);

            var handler = new GetNotificationQueryHandler(_notificationRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.NotificationId.Should().Be(1);

        }

        [Test]
        public async Task Notification_GetQueries_Success()
        {
            //Arrange
            var query = new GetNotificationsQuery();

            _notificationRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Notification, bool>>>()))
                        .ReturnsAsync(new List<Notification> { new Notification() { /*TODO:propertyler buraya yazılacak NotificationId = 1, NotificationName = "test"*/ } });

            var handler = new GetNotificationsQueryHandler(_notificationRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Notification>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Notification_CreateCommand_Success()
        {
            Notification rt = null;
            //Arrange
            var command = new CreateNotificationCommand();
            //propertyler buraya yazılacak
            //command.NotificationName = "deneme";

            _notificationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Notification, bool>>>()))
                        .ReturnsAsync(rt);

            _notificationRepository.Setup(x => x.Add(It.IsAny<Notification>())).Returns(new Notification());

            var handler = new CreateNotificationCommandHandler(_notificationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _notificationRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Notification_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateNotificationCommand();
            //propertyler buraya yazılacak 
            //command.NotificationName = "test";

            _notificationRepository.Setup(x => x.Query())
                                           .Returns(new List<Notification> { new Notification() { /*TODO:propertyler buraya yazılacak NotificationId = 1, NotificationName = "test"*/ } }.AsQueryable());

            _notificationRepository.Setup(x => x.Add(It.IsAny<Notification>())).Returns(new Notification());

            var handler = new CreateNotificationCommandHandler(_notificationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Notification_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateNotificationCommand();
            //command.NotificationName = "test";

            _notificationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Notification, bool>>>()))
                        .ReturnsAsync(new Notification() { /*TODO:propertyler buraya yazılacak NotificationId = 1, NotificationName = "deneme"*/ });

            _notificationRepository.Setup(x => x.Update(It.IsAny<Notification>())).Returns(new Notification());

            var handler = new UpdateNotificationCommandHandler(_notificationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _notificationRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Notification_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteNotificationCommand();

            _notificationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Notification, bool>>>()))
                        .ReturnsAsync(new Notification() { /*TODO:propertyler buraya yazılacak NotificationId = 1, NotificationName = "deneme"*/});

            _notificationRepository.Setup(x => x.Delete(It.IsAny<Notification>()));

            var handler = new DeleteNotificationCommandHandler(_notificationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _notificationRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

