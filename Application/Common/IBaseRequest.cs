using Domain.Responses;
using MediatR;

namespace Application.Common
{
    public interface IBaseRequest : IRequest<BaseResponse>
    {
    }

    public interface IBaseRequest<TResponse> : IRequest<BaseResponse<TResponse>>
    {
    }
}
