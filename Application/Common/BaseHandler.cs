using Application.Services.Interfaces;
using AutoMapper;
using Domain.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence.Data;

namespace Application.Common
{
    public abstract class BaseHandler<TRequest> :
        BaseHandler, IRequestHandler<TRequest, BaseResponse> where TRequest : IBaseRequest
    {
        protected readonly ILogger<TRequest> _logger;

        protected BaseHandler(CookbookDbContext dbContext, IMapper mapper, IUserService userService, ILogger<TRequest> logger) :
            base(dbContext, mapper, userService)
        {
            _logger = logger;
        }

        public abstract Task<BaseResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class BaseHandler<TRequest, TResponse> :
        BaseHandler, IRequestHandler<TRequest, BaseResponse<TResponse>> where TRequest : IBaseRequest<TResponse>
    {
        protected readonly ILogger<TRequest> _logger;

        protected BaseHandler(CookbookDbContext dbContext, IMapper mapper, IUserService userService, ILogger<TRequest> logger) :
            base(dbContext, mapper, userService)
        {
            _logger = logger;
        }

        public abstract Task<BaseResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public class BaseHandler
    {
        public readonly CookbookDbContext _dbContext;
        public readonly IMapper _mapper;
        public readonly IUserService _user;

        public BaseHandler(CookbookDbContext dbContext, IMapper mapper, IUserService userService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _user = userService;
        }
    }
}
