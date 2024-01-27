using Application.Common;
using Application.Funcions.User.Commands.SignUp;
using Application.Helpers;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            var mapperConfig = MapperHelper.CreateMappingConfiguration();
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IValidator<SignUpCommand>, SignUpCommandValidator>();

            services.AddHttpContextAccessor();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
