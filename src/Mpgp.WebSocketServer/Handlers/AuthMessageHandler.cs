﻿// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Mpgp.Abstract;
using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Dtos;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.Domain.Accounts.Queries;
using Mpgp.Shared.Exceptions;
using Mpgp.WebSocketServer.Core;
using Mpgp.WebSocketServer.Messages;

namespace Mpgp.WebSocketServer.Handlers
{
    public class AuthMessageHandler : MessageHandler<Messages.Client.AuthMessage>
    {
        private readonly ICommandFactory commandFactory;

        private readonly ILogger<AuthMessageHandler> logger;

        private readonly IQueryFactory queryFactory;

        public AuthMessageHandler(ICommandFactory commandFactory, ILogger<AuthMessageHandler> logger, IQueryFactory queryFactory)
        {
            this.commandFactory = commandFactory;
            this.logger = logger;
            this.queryFactory = queryFactory;
        }

        public override Task CheckAuth()
        {
            return Task.CompletedTask;
        }

        public override async Task Handle()
        {
            try
            {
                var account = await GetAccountByToken(Context.Message.Payload.AuthToken);
                var response = new Messages.Server.AuthMessage()
                {
                    UsersList = Context.ConnectionManager.GetOnlineUsers()
                };
                Context.ConnectionManager.AddSocket(account, Context.Socket);
                await Context.ConnectionManager.SendMessageAsync(Context.Socket, response);

                var connectionMessage = new Messages.Server.UserConnectionMessage()
                {
                    Account = account,
                    Status = Status.Connect
                };
                await Context.ConnectionManager.SendMessageToAllExcludeOneAsync(Context.Socket, connectionMessage);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Unhandled exception in AuthMessageHandler in method 'Handle'.");
            }
        }

        private async Task<AccountDto> GetAccountByToken(string token)
        {
            try
            {
                await commandFactory.Execute(Context.Message.Payload as ValidateTokenCommand);

                var model = await queryFactory.ResolveQuery<AccountByAuthTokenQuery>().Execute(token);
                if (Context.ConnectionManager.IsConnected(model.AccountId))
                {
                    throw new ConflictException("ALREADY_CONNECTED");
                }

                return AutoMapper.Mapper.Map<Account, AccountDto>(model);
            }
            catch (NotFoundException ex)
            {
                await Disconnect(404, ex.Message);
                throw;
            }
            catch (ConflictException ex)
            {
                await Disconnect(409, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Unhandled exception in AuthMessageHandler in method 'GetAccountByToken'.");
                throw;
            }
        }
    }
}
