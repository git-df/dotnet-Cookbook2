using Domain.Requests;
using Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Helpers
{
    public static class PaginationHelper
    {
        private static readonly int DefaultPageIndex = 1;
        private static readonly int DefaultPageSize = 10;

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> source, PaginationRequest paginationRequest)
        {
            paginationRequest = GetValidPaginationOptions(paginationRequest);

            return source
                .Skip(paginationRequest.PageSize * (paginationRequest.PageIndex - 1))
                .Take(paginationRequest.PageSize);
        }

        public static async Task<PaginationResponse> GetPaginationInfo<T>(
            this IQueryable<T> source, PaginationRequest paginationRequest, CancellationToken cancellationToken = default)
        {
            paginationRequest = GetValidPaginationOptions(paginationRequest);
            var count = await source.CountAsync(cancellationToken);

            return new PaginationResponse(
                IndexCount: count,
                PageCount: (int)Math.Ceiling((double)count / paginationRequest.PageSize),
                FirstIndex: count < 1 ? 0 : paginationRequest.PageSize * (paginationRequest.PageIndex - 1) + 1,
                LastIndex: count < 1 ? 0 : Math.Min(paginationRequest.PageSize * paginationRequest.PageIndex, count));
        }

        private static PaginationRequest GetValidPaginationOptions(PaginationRequest paginationRequest)
            => new(
                PageIndex: paginationRequest.PageIndex <= 0 ? DefaultPageIndex : paginationRequest.PageIndex,
                PageSize: paginationRequest.PageSize <= 0 ? DefaultPageSize : paginationRequest.PageSize);
    }
}
