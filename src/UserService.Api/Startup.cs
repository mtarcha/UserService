using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Swashbuckle.AspNetCore.Swagger;
using UserService.Api.Services;
using UserService.Application.Commands;
using UserService.Application.Queries;
using UserService.Domain.Repositories;
using UserService.Infrastructure.EventSourcing;
using UserService.Infrastructure.Mongo;
using UserService.Infrastructure.Sql;
using UserService.Infrastructure.Sql.Handlers;

namespace UserService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddScoped<IStorageSeeder, StorageSeeder>();
            services.AddScoped<IEncryptionKeysProvider<Guid>, UserEncryptionKeyProvider>();
            services.AddScoped<IEncryptionService<Guid>, EncryptionService<Guid>>();
            
            var connectionString = Configuration.GetConnectionString("UserSqlDbConnectionString");
            services.AddDbContext<UserServiceDbContext>(cfg =>
            {
                cfg.UseSqlServer(connectionString);
            });

            services.AddMongoDbEventStore(Configuration);

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddMediatR(typeof(CreateUserCommand), typeof(UserCreatedEventHandler));

            services.AddControllers();

            services.AddSwaggerGen();
        }
    

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Service API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
