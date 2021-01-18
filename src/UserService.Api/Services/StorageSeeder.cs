﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Application.Commands;
using UserService.Infrastructure.Sql;

namespace UserService.Api.Services
{
    public class StorageSeeder : IStorageSeeder
    {
        private readonly IMediator _mediator;
        private readonly UserServiceDbContext _sqlDbContext;

        public StorageSeeder(IMediator mediator, UserServiceDbContext sqlDbContext)
        {
            _mediator = mediator;
            _sqlDbContext = sqlDbContext;
        }

        public async Task SeedAsync(CancellationToken token)
        {
            await _sqlDbContext.Database.EnsureCreatedAsync(token);
        }
    }
}