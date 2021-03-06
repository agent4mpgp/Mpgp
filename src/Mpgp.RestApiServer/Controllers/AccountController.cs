﻿// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Mpgp.Abstract;
using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Dtos;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.Domain.Accounts.Queries;
using Mpgp.Infrastructure.Filters;
using Mpgp.RestApiServer.ApiServices;

namespace Mpgp.RestApiServer.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ValidationFilterAttribute<Account.Errors>))]
    public class AccountController : ControllerBase
    {
        private readonly ICommandFactory commandFactory;
        private readonly IQueryFactory queryFactory;

        public AccountController(ICommandFactory commandFactory, IQueryFactory queryFactory)
        {
            this.commandFactory = commandFactory;
            this.queryFactory = queryFactory;
        }

        [HttpPost]
        public async Task<AuthInfoDto> Authorize(AuthorizeAccountCommand command)
        {
            var account = await queryFactory.ResolveQuery<AccountByLoginAndPasswordQuery>()
                .Execute(command.Login, command.Password);

            return AccountApiService.CreateAuthInfo(account);
        }

        [HttpGet("{id}")]
        public async Task<AccountDto> GetInfo(int id)
        {
            var response = await queryFactory.ResolveQuery<AccountByIdQuery>().Execute(id);
            return AutoMapper.Mapper.Map<Account, AccountDto>(response);
        }

        [HttpPut]
        public async Task<AuthInfoDto> Register(RegisterAccountCommand command)
        {
            await commandFactory.Execute(command);
            var account = await queryFactory.ResolveQuery<AccountByLoginAndPasswordQuery>()
                .Execute(command.Login, command.Password);

            return AccountApiService.CreateAuthInfo(account);
        }
    }
}
