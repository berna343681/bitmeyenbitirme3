
using Business.Handlers.PhoneNumbers.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.PhoneNumbers.Queries.GetPhoneNumberQuery;
using Entities.Concrete;
using static Business.Handlers.PhoneNumbers.Queries.GetPhoneNumbersQuery;
using static Business.Handlers.PhoneNumbers.Commands.CreatePhoneNumberCommand;
using Business.Handlers.PhoneNumbers.Commands;
using Business.Constants;
using static Business.Handlers.PhoneNumbers.Commands.UpdatePhoneNumberCommand;
using static Business.Handlers.PhoneNumbers.Commands.DeletePhoneNumberCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class PhoneNumberHandlerTests
    {
        Mock<IPhoneNumberRepository> _phoneNumberRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _phoneNumberRepository = new Mock<IPhoneNumberRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task PhoneNumber_GetQuery_Success()
        {
            //Arrange
            var query = new GetPhoneNumberQuery();

            _phoneNumberRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<PhoneNumber, bool>>>())).ReturnsAsync(new PhoneNumber()
//propertyler buraya yazılacak
//{																		
//PhoneNumberId = 1,
//PhoneNumberName = "Test"
//}
);

            var handler = new GetPhoneNumberQueryHandler(_phoneNumberRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.PhoneNumberId.Should().Be(1);

        }

        [Test]
        public async Task PhoneNumber_GetQueries_Success()
        {
            //Arrange
            var query = new GetPhoneNumbersQuery();

            _phoneNumberRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<PhoneNumber, bool>>>()))
                        .ReturnsAsync(new List<PhoneNumber> { new PhoneNumber() { /*TODO:propertyler buraya yazılacak PhoneNumberId = 1, PhoneNumberName = "test"*/ } });

            var handler = new GetPhoneNumbersQueryHandler(_phoneNumberRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<PhoneNumber>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task PhoneNumber_CreateCommand_Success()
        {
            PhoneNumber rt = null;
            //Arrange
            var command = new CreatePhoneNumberCommand();
            //propertyler buraya yazılacak
            //command.PhoneNumberName = "deneme";

            _phoneNumberRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<PhoneNumber, bool>>>()))
                        .ReturnsAsync(rt);

            _phoneNumberRepository.Setup(x => x.Add(It.IsAny<PhoneNumber>())).Returns(new PhoneNumber());

            var handler = new CreatePhoneNumberCommandHandler(_phoneNumberRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _phoneNumberRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task PhoneNumber_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreatePhoneNumberCommand();
            //propertyler buraya yazılacak 
            //command.PhoneNumberName = "test";

            _phoneNumberRepository.Setup(x => x.Query())
                                           .Returns(new List<PhoneNumber> { new PhoneNumber() { /*TODO:propertyler buraya yazılacak PhoneNumberId = 1, PhoneNumberName = "test"*/ } }.AsQueryable());

            _phoneNumberRepository.Setup(x => x.Add(It.IsAny<PhoneNumber>())).Returns(new PhoneNumber());

            var handler = new CreatePhoneNumberCommandHandler(_phoneNumberRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task PhoneNumber_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdatePhoneNumberCommand();
            //command.PhoneNumberName = "test";

            _phoneNumberRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<PhoneNumber, bool>>>()))
                        .ReturnsAsync(new PhoneNumber() { /*TODO:propertyler buraya yazılacak PhoneNumberId = 1, PhoneNumberName = "deneme"*/ });

            _phoneNumberRepository.Setup(x => x.Update(It.IsAny<PhoneNumber>())).Returns(new PhoneNumber());

            var handler = new UpdatePhoneNumberCommandHandler(_phoneNumberRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _phoneNumberRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task PhoneNumber_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeletePhoneNumberCommand();

            _phoneNumberRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<PhoneNumber, bool>>>()))
                        .ReturnsAsync(new PhoneNumber() { /*TODO:propertyler buraya yazılacak PhoneNumberId = 1, PhoneNumberName = "deneme"*/});

            _phoneNumberRepository.Setup(x => x.Delete(It.IsAny<PhoneNumber>()));

            var handler = new DeletePhoneNumberCommandHandler(_phoneNumberRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _phoneNumberRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

