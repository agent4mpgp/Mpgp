// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Mpgp.Abstract;
using Mpgp.DataAccess;
using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Dtos;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.Domain.Accounts.Handlers;
using Mpgp.Domain.Accounts.Queries;
using Mpgp.RestApiServer.Controllers;
using NUnit.Framework;

namespace Mpgp.UnitTests.Mpgp.RestApiServer.Controllers
{
    public class AccountControllerTest
    {
        private ILogger<AccountController> logger;
        private AppUnitOfWork uow;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<Infrastructure.AutoMapperProfile>());
            logger = new Mock<ILogger<AccountController>>().Object;
        }

        [SetUp]
        public void Setup()
        {
            var dbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options);

            uow = new AppUnitOfWork(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            uow.Dispose();
        }

        [Test]
        public async Task Authorize_ExpectSuccessResponse()
        {
            // Arrange
            await uow.AccountRepository.AddAsync(new Account()
            {
                Login = "admin2018",
                Nickname = "AlexAnder",
                Password = Shared.Utils.HashString("12345678asdf")
            });
            await uow.SaveChangesAsync();

            var command = new AuthorizeAccountCommand()
            {
                Login = "admin2018",
                Password = "12345678asdf"
            };
            var mockCommandFactory = new Mock<ICommandFactory>();
            mockCommandFactory.Setup(repo => repo.Execute(command))
                .Returns(async () =>
                {
                    var handler = new AuthorizeAccountCommandHandler(uow);
                    await handler.Execute(command);
                    handler.Dispose();
                });

            // Act
            var controller = new AccountController(mockCommandFactory.Object, logger, null);
            var okObjectResult = await controller.Authorize(command) as OkObjectResult;

            // Assert
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as AuthDataDto;
            Assert.NotNull(model);
        }

        [Test]
        public async Task GetInfo_ExpectSuccessResponse()
        {
            // Arrange
            await uow.AccountRepository.AddAsync(new Account()
            {
                Avatar = "29.jpg",
                Login = "admin2018",
                Nickname = "AlexAnder",
                Password = Shared.Utils.HashString("12345678asdf")
            });
            await uow.SaveChangesAsync();

            var mockQueryFactory = new Mock<IQueryFactory>();
            mockQueryFactory.Setup(repo => repo.ResolveQuery<AccountByIdQuery>())
                .Returns(() => new AccountByIdQuery(uow));

            // Act
            var account = await uow.AccountRepository.GetByLogin("admin2018");
            var controller = new AccountController(null, logger, mockQueryFactory.Object);
            var okObjectResult = await controller.GetInfo(account.AccountId) as OkObjectResult;

            // Assert
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as AccountDto;
            Assert.NotNull(model);
            Assert.AreEqual("AlexAnder", model.Nickname);
            Assert.AreEqual("29.jpg", model.Avatar);
        }

        [Test]
        public async Task Register_ExpectSuccessResponse()
        {
            // Arrange
            var command = new RegisterAccountCommand()
            {
                Login = "admin2018",
                Nickname = "AlexAnder",
                Password = "12345678asdf",
                PasswordRepeat = "12345678asdf"
            };

            var mockCommandFactory = new Mock<ICommandFactory>();
            mockCommandFactory.Setup(repo => repo.Execute(command))
                .Returns(async () =>
                {
                    var handler = new RegisterAccountCommandHandler(uow);
                    await handler.Execute(command);
                    handler.Dispose();
                });

            // Act
            var controller = new AccountController(mockCommandFactory.Object, logger, null);
            var objectResult = await controller.Register(command) as ObjectResult;

            // Assert
            Assert.NotNull(objectResult);

            var model = objectResult.Value as AuthDataDto;
            Assert.NotNull(model);
        }

        [Test]
        public async Task ValidateToken_ExpectSuccessResponse()
        {
            // Arrange
            await uow.AccountRepository.AddAsync(new Account()
            {
                AuthToken = "392c2a901720d24e26be260ec331632f",
                Login = "admin2018",
                Nickname = "AlexAnder",
                Password = Shared.Utils.HashString("12345678asdf")
            });
            await uow.SaveChangesAsync();

            var command = new ValidateTokenCommand()
            {
                AuthToken = "392c2a901720d24e26be260ec331632f"
            };
            var mockCommandFactory = new Mock<ICommandFactory>();
            mockCommandFactory.Setup(repo => repo.Execute(command))
                .Returns(async () =>
                {
                    var handler = new ValidateTokenCommandHandler(uow);
                    await handler.Execute(command);
                    handler.Dispose();
                });

            // Act
            var controller = new AccountController(mockCommandFactory.Object, logger, null);
            var okResult = await controller.ValidateToken(command) as OkResult;

            // Assert
            Assert.NotNull(okResult);
        }
    }
}